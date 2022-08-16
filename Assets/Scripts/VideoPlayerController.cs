using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;


public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Transform plane;
    private Vector3 planeSize;
    private Vector3 planePosition;
    public Vector3 playedPosition;
    public Vector3 playedScale;
    public colliderscript colliderscript;
    public int videoId;
    //public Animator videoPlayerAnimator;
    private bool videoPlayed=false;
    DateTime startTime;
    DateTime endTime;
    private int playerState;
    void Start()
    {
        plane = GetComponent<Transform>();
        planeSize = plane.localScale;
        planePosition = plane.position;
        //videoPlayer = GetComponentInParent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == 1 && !videoPlayer.isPlaying)
        {
            endTime = DateTime.Now;
            colliderscript.sendVideoData(startTime, endTime, videoId);
            playerState = 0;
        }
    }

    public void startStopVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            endTime = DateTime.Now;
            colliderscript.sendVideoData(startTime, endTime, videoId);
            plane.localScale = planeSize;
            plane.position = planePosition;

        }
        else
        {
            print("clicked");
            videoPlayer.Play();
            startTime = DateTime.Now;
            plane.localScale = playedScale;
            plane.position = playedPosition;
            if (!videoPlayed)
            {
                videoPlayed = true;
            }
        }

    }
    /*public void resizePlayer()
    {
        if (videoPlayerAnimator.GetBool("isSmall"))
        {
            videoPlayerAnimator.SetBool("isSmall", false);
        }
        else
        {
            videoPlayerAnimator.SetBool("isSmall", true);
        }

    }*/
    public bool isVideoPlayed()
    {
        return (videoPlayed);
    }
    public VideoPlayer getVideoPlayer()
    {
        return videoPlayer;
    }
}
