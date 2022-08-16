using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;
public class GuideAuditorium : MonoBehaviour
{
    private Transform guide;
    public Transform player;
    public float minDiff;
    private int state=0;
    private AudioSource audioSource;
    public AudioClip[] audioclips;
    public VideoPlayer videoPlayer;
    private Animator animator;
    public GameObject teleporter;
    public Transform playerNavAnchorTarget;
    public StaticPlayer staticPlayer;
    void Start()
    {
        guide = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float PGDiff = Mathf.Abs(Vector3.Distance(guide.position, player.position));
        if (PGDiff<minDiff)
        {
            if (state == 0 && !audioSource.isPlaying)
            {
                audioSource.clip = audioclips[0];
                audioSource.Play();
                animator.SetBool("isTalking", true);
                state = 1;
            }
            else if(state==1 && !audioSource.isPlaying)
            {
                state = 2;
                animator.SetBool("isTalking", false);
            }
        }
        if (state == 0)
        {
            videoPlayer.Stop();
        }
        else if (state == 2)
        {
            staticPlayer.setPlayerState(1);
            videoPlayer.Play();
            //yield return new WaitForSeconds(1);
            state = 3;
        }
        else if(state == 3 && videoPlayer.isPlaying)
        {
            state = 4;
        }
        else if(state == 4 && !videoPlayer.isPlaying && !audioSource.isPlaying)
        {
            animator.SetBool("isTalking", true);
            audioSource.clip = audioclips[1];
            audioSource.Play();
            state = 5;
            teleporter.SetActive(true);
            playerNavAnchorTarget.position = teleporter.transform.position;
        }
        print(state + " " + videoPlayer.isPlaying);
    }
}
