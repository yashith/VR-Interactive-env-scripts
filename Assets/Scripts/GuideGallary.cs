using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;

public class GuideGallary : MonoBehaviour
{
    public Transform player;
    private int greeting = 0;
    private int showTeleporter = 0;
    private Animator animator;
    private Transform guide;
    public float minDistanceDifference;
    private AudioSource audioPlayer;
    public AudioClip[] guideAudios;
    public Transform target;
    private NavMeshAgent navMeshAgent;
    public VideoCounter videoCounter;
    public Transform PlayerNavTarget;
    public Transform teleporter;
    public Vector3 tvPosition;
    void Start()
    {
        animator = GetComponent<Animator>();
        guide = GetComponent<Transform>();
        audioPlayer = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        PlayerNavTarget.position = new Vector3(7.76000023f, 0f, -50.0379982f);
    }

    void Update()
    {
        float GPDifference = Mathf.Abs(Vector3.Distance(player.position, guide.position));

        if (GPDifference < minDistanceDifference)
        {
            if (greeting == 0)
            {
                animator.SetBool("isTalking", true);
                audioPlayer.clip = guideAudios[0];
                audioPlayer.Play();
                greeting = 1;
            }
            
        }
        if (greeting == 1 && !audioPlayer.isPlaying)
        {
            PlayerNavTarget.position = tvPosition;
            animator.SetBool("isTalking", false);
            greeting = 3;
        }
        if (greeting == 3)
        {
            if (videoCounter.isAllWatched() && showTeleporter == 0)
            {
                navMeshAgent.SetDestination(target.position);
                animator.SetBool("isWalking", true);
                showTeleporter = 1;
            }
            else if(videoCounter.isAllWatched() && navMeshAgent.remainingDistance==0 && showTeleporter==1)
            {
                audioPlayer.clip = guideAudios[1];
                animator.SetBool("isWalking", false);
                animator.SetBool("isTalking", true);
                guide.LookAt(player);
                if (!isOnePlaying())
                {
                    audioPlayer.Play();
                    showTeleporter = 2;
                }
                
            }
            else if(showTeleporter==2 && !audioPlayer.isPlaying)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isTalking", false);
                PlayerNavTarget.position = teleporter.position;
            }
        }
    }
    private bool isOnePlaying()
    {
        bool playing=false;
        foreach(VideoPlayerController videoController in videoCounter.getVideoControllers())
        {
            playing = playing || videoController.getVideoPlayer().isPlaying;
            print(playing);
        }
        return playing;
    }
}
