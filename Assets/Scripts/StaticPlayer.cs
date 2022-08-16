using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.Audio;

public class StaticPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject teleporter;
    private bool isPlayed= false;
    public Transform playerAnchorTarget;
    private DateTime startTime;
    private DateTime endTime;
    public colliderscript colliderscript;
    public int videoId;
    private int playerState = 0;
    public GameObject canvas;
    bool played = false;
    public GameObject guide;
    private GameObject player;
    public AudioSource guideAudio;
    void Start()
    {
        player = GameObject.Find("XR Origin");
        guide = GameObject.Find("Guide Variant");
        if (teleporter != null)
        {
            teleporter.SetActive(false);
        }
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    void Update()
    {
        
        if (videoPlayer.isPlaying)
        {
            played = true;

        }
        if (isPlayed && teleporter != null)
        {
            teleporter.SetActive(true);
            playerAnchorTarget.position = teleporter.transform.position;

        }
        
        if(playerState==1 && !videoPlayer.isPlaying && played)
        {
            
            if (canvas != null)
            {
                canvas.SetActive(true);
            }
            else if(guideAudio != null)
            {
                guide.transform.LookAt(player.transform);
                guideAudio.Play();
            }
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
            if(canvas != null)
            {
                canvas.SetActive(true);


            }
            else if (guideAudio != null)
            {
                guide.transform.LookAt(player.transform);
                guideAudio.Play();
            }
            playerState = 2;
            endTime = DateTime.Now;
            colliderscript.sendVideoData(startTime, endTime, videoId);
        }
        else
        {
            
            videoPlayer.Play();
            playerState = 1;
            startTime = DateTime.Now;
            if (!isPlayed)
            {
                isPlayed = true;
            }
        }

    }
    public void setPlayerState(int state)
    {
        startTime = DateTime.Now;
        this.playerState = state;
    }

}
