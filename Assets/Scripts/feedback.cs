using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using System.Text;

public class feedback : MonoBehaviour

{
   // public Button SubmitBtn;
    List<string> Questions = new List<string>();
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI Final_msg;
    string selectedAnswer = "";
    int selectedQuestion = 0;
    Dictionary<string, string> QuestionResults = new Dictionary<string, string>();
    public GameObject form;
    public GameObject buttons;
    string startTime;
    string endTime;
    public string pollName;
    public string category;
    public string category_item_name;
    public int category_item_id;
    string device_id;
    public TextAsset url;
    public TextAsset pollQuestions;
    public static string URL;
    public Transform PlayerNavTarget;
    public Vector3 targetPosition;
    public GameObject finalCollider;
    public GameObject[] buttonGroupList;
    public Transform guide;
    public GuideRoom guideRoom;

    // Start is called before the first frame update
    void Start()
    {
        URL = url.text;
        device_id = "1";
        string pollRawQuestions = pollQuestions.text;
        Questions = new List<string>(pollRawQuestions.Split("#"));
        QuestionText.text = Questions[selectedQuestion];
        buttonGroupList[0].SetActive(true);
        buttonGroupList[1].SetActive(false);
        buttonGroupList[2].SetActive(false);
    }

    private void OnEnable()
    {
        startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }

    public void buttonCliked(string b)
    {
        if (startTime == null)
        {
            startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        selectedAnswer = b;
        QuestionResults.Add(selectedQuestion.ToString(), selectedAnswer);
        selectedAnswer = "";

        if (selectedQuestion == Questions.Count - 1)
        {
            buttons.SetActive(false);
            buttonGroupList[2].SetActive(false);
            Final_msg.text = "Thank you for helping shape\nthe Virtusa Brand!";
            // Move guide to the door
            if(guide != null)
            {
                guide.position = targetPosition;
                guideRoom.setDestination(targetPosition);
                guideRoom.setStage(5);
                guideRoom.lookAtPlayer();
            }
            PlayerNavTarget.position = targetPosition;
            if (finalCollider != null)
            {
                finalCollider.SetActive(false);
            }
            endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            System.Random rd = new System.Random();
            int rand_num = rd.Next(100, 200);

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < Questions.Count; j++)
            {
                sb.Append(j.ToString() + "," + QuestionResults[j.ToString()]);
                if(j!= Questions.Count - 1)
                {
                    sb.Append("#");
                }
                    
            }
            PollResults pollAnswers = new PollResults { session_id = SessionIdGen.sessionID, device_id = device_id, poll_name = pollName, start_time = startTime, end_time = endTime, answers= sb.ToString()
            , category_item_id= category_item_id, category_item_name=category_item_name, category= category};

            StartCoroutine(sendFeedback(pollAnswers));
            StartCoroutine(waitBeforeClose());

        }

        else
        {
            buttonGroupList[selectedQuestion].SetActive(false);
            selectedQuestion++;
            buttonGroupList[selectedQuestion].SetActive(true);
            QuestionText.text = Questions[selectedQuestion];

        }

    }

    IEnumerator waitBeforeClose()
    {

        yield return new WaitForSeconds(5);
        form.SetActive(false);
    }

    private IEnumerator sendFeedback(PollResults pollResults)
    {
        using(var postRequest = CreateRequest(URL, RequestType.POST, pollResults))
        {
            yield return postRequest.SendWebRequest();
        }
    
    }

    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var t = JsonUtility.ToJson(data);
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }


}
[Serializable]
public class PollResults
{
    public string session_id;
    public string device_id;
    public string poll_name;
    public string answers;
    public string start_time;
    public string end_time;
    public string category;
    public string category_item_name;
    public int category_item_id;
    public string query_type="poll";


}


