using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVideoCounter : MonoBehaviour
{
    [SerializeField]
    private VideoPlayerController[] videoPlayerControllers;

    private bool isFullyWatched = false;
    
    void Start()
    {
        
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
        }
        if(counter.ToString() == videoPlayerControllers.Length.ToString())
        {
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

