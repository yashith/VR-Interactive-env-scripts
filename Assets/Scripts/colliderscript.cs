using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class colliderscript : MonoBehaviour
{

    string player = "Player";
    private string url;
    public string category;
    public string category_item_name;
    public int category_item_id;
    public TextAsset urlFile;
    string device_id;
    DateTime startTime;
    DateTime endTime;



    // Start is called before the first frame update
    void Start()
    {
        url = urlFile.text;
        device_id = "1";

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == player)
        {
            print("Object Entered the trigger:"+ DateTime.Now.ToString());
            startTime = DateTime.Now;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
  
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == player)
        {
           print("Object Exited the trigger" + DateTime.Now.ToString());

            endTime = DateTime.Now;
            var result= StartCoroutine(sendStatData());

        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator sendStatData()
    {
        var stats = new Stat() { device_id = device_id, session_id = SessionIdGen.sessionID, category = category, category_item_id=category_item_id, category_item_name=category_item_name
        , start_time=startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), end_time =endTime.ToString("yyyy-MM-dd HH:mm:ss.fff")};
        using(var postRequest = CreateRequest(url, RequestType.POST, stats))
        {
           yield return postRequest.SendWebRequest();
        }
        
    }


    private UnityWebRequest CreateRequest(string path, RequestType type = RequestType.GET, object data = null)
    {
        var request = new UnityWebRequest(path, type.ToString());

        if (data != null)
        {
            var bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;
    }

    private void AttachHeader(UnityWebRequest request, string key, string value)
    {
        request.SetRequestHeader(key, value);
    }

    public void sendVideoData(DateTime startTime,DateTime endTime,int cat_item_id)
    {
        var stats = new Stat()
        {
            device_id = device_id,
            session_id = SessionIdGen.sessionID,
            category = "Video",
            category_item_id = cat_item_id,
            category_item_name = category_item_name + "_" + cat_item_id.ToString(),
            start_time = startTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
            end_time = endTime.ToString("yyyy-MM-dd HH:mm:ss.fff")
        };
        StartCoroutine(sendVideoDataEnum(stats));
    }
    private IEnumerator sendVideoDataEnum(Stat stat)
    {
        using (var postRequest = CreateRequest(url, RequestType.POST, stat))
        {
            yield return postRequest.SendWebRequest();
        }

    }

}

public enum RequestType
{
    GET = 0,
    POST = 1,
    PUT = 2
}



[Serializable]
public class Stat
{
    public string session_id;
    public string device_id;
    public string category;
    public string category_item_name;
    public int category_item_id;
    public String start_time;
    public String end_time;
    public string query_type = "time";
}

public class PostResult
{
    public string success { get; set; }
}


