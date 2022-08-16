using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VideoCounter : MonoBehaviour
{
    [SerializeField]
    private VideoPlayerController[] videoPlayerControllers;
    private TextMeshProUGUI counterText;
    public GameObject teleporter;
    private bool isFullyWatched = false;
    
    void Start()
    {
        counterText = GetComponentInChildren<TextMeshProUGUI>();
        teleporter.SetActive(false);
        
    }

    void Update()
    {
        int counter = 0;
        if (videoPlayerControllers.Length != 0)
        {
            foreach(VideoPlayerController videoController in videoPlayerControllers)
            {
                if (videoController.isVideoPlayed())
                {
                    counter++;
                }
            }
            counterText.text = counter.ToString() +" out of "+ videoPlayerControllers.Length.ToString()+" Items Completed";
        }
        if(counter.ToString() == videoPlayerControllers.Length.ToString())
        {
            teleporter.SetActive(true);
            this.isFullyWatched = true;
        }
        
    }
    public bool isAllWatched()
    {
        return this.isFullyWatched;
    }
    public VideoPlayerController[] getVideoControllers()
    {
        return this.videoPlayerControllers;
    }
}

