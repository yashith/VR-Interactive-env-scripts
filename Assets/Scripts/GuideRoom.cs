using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GuideRoom : MonoBehaviour
{
    public Transform player;
    private int stage = 0;
    private int showTeleporter = 0;
    private Animator animator;
    private Transform guide;
    public float minDistanceDifference;
    private AudioSource audioPlayer;
    public AudioClip[] guideAudios;
    public Transform target;
    private NavMeshAgent navMeshAgent;
    public RoomVideoCounter videoCounter;
    public Transform playerNavAnchor;
    public Vector3[] targetPlayerNavAnchorPositions;
    public Vector3 finalPosition;
    public GameObject finalCanvas;
    public VideoPlayer outSideVideoPlayer;
    //public Vector3 guideTargetPosition;
    void Start()
    {
        animator = GetComponent<Animator>();
        guide = GetComponent<Transform>();
        audioPlayer = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        finalCanvas.SetActive(false);
    }

    void Update()
    {
        //print(stage);

        float GPDifference = Mathf.Abs(Vector3.Distance(player.position, guide.position));

        if (GPDifference < minDistanceDifference)
        {
            animator.SetBool("isTalking", true);

            if (stage == 0 && !audioPlayer.isPlaying)
            {

                StartCoroutine(AudioPlay(guideAudios[0], audioPlayer));
                stage = 1;

            }
            else if (stage == 1 && !audioPlayer.isPlaying)
            {
                playerNavAnchor.position = targetPlayerNavAnchorPositions[0];
                //StartCoroutine(AudioPlay(guideAudios[1], audioPlayer));

                stage = 2;
                audioPlayer.clip=guideAudios[1];
            }
        }

        if (stage == 2)
        {

            if (!audioPlayer.isPlaying)
            {
                animator.SetBool("isWalking", true);

                navMeshAgent.SetDestination(target.position);

                stage = 3;
            }

        }

        else if (stage == 3 )
        {
            if (navMeshAgent.remainingDistance == 0)
            {
                animator.SetBool("isWalking", false);
                stage = 4;

            }
            
        }
        else if(stage==4 && audioPlayer.isPlaying)
        {
            animator.SetBool("isTalking", true);
            playerNavAnchor.position = targetPlayerNavAnchorPositions[1];
           
        }
        else if(stage ==5 && (GPDifference < minDistanceDifference))
        {
            guide.LookAt(player);
            audioPlayer.clip = guideAudios[2];
            audioPlayer.Play();
            stage = 6;
        }
        else if(stage == 6 && audioPlayer.isPlaying)
        {
            playerNavAnchor.position = targetPlayerNavAnchorPositions[2];
            stage = 7;
        }
        else if(stage ==7 && !audioPlayer.isPlaying)
        {
            animator.SetBool("isTalking", false);
            navMeshAgent.SetDestination(finalPosition);
            animator.SetBool("isWalking", true);
            finalCanvas.SetActive(true);
            stage = 8;
        }
        else if(stage == 8 && navMeshAgent.remainingDistance == 0)
        {
            animator.SetBool("isWalking", false);
            guide.LookAt(player);
        }
        else if(stage ==9 && !audioPlayer.isPlaying)
        {
            SceneManager.LoadScene(0);
        }
        /*if (stage == 4 && (GPDifference < minDistanceDifference))
        {
            guide.LookAt(player);
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = guideAudios[1];
                audioPlayer.Play();
                stage = 5;
            }
        }
        else if(stage==5 && !audioPlayer.isPlaying)
        {
            target.position = finalPosition;
            StartCoroutine(GuideWalk(guide, target));
        }*/

        /*if (stage == 4)
        {
            if (!audioPlayer.isPlaying && true && GPDifference < minDistanceDifference && !animator.GetBool("isWalking"))
            {
                guide.LookAt(player);

                StartCoroutine(BeforePollAudio(guideAudios[2], audioPlayer));
                stage = 5;
            }
        }
        else if (stage == 5 && !audioPlayer.isPlaying)
        {
            //target.position = finalPosition;
            finalCanvas.SetActive(true);
            navMeshAgent.SetDestination(finalPosition);
            animator.SetBool("isWalking", true);
            stage = 6;
        }
        else if(stage==6 & navMeshAgent.remainingDistance == 0)
        {
            animator.SetBool("isWalking", false);
            guide.LookAt(player);
        }
        else if(stage==7 && !audioPlayer.isPlaying)
        {
            SceneManager.LoadScene(0);
        }*/
        
    }

    IEnumerator BeforePollAudio(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;

        audioSource.Play();

        yield return new WaitUntil(() => audioSource.isPlaying);

        animator.SetBool("isTalking", false);

        stage = 5;
    }

    IEnumerator AudioPlay(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;

        audioSource.Play();

        yield return new WaitUntil(() => audioSource.isPlaying);

        animator.SetBool("isTalking", false);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator GuideWalk(Transform guide, Transform target)
    {
        animator.SetBool("isTalking", false);

        

        if (Vector3.Distance(guide.position, target.position) < 1f)
        {
            animator.SetBool("isWalking", false);

            stage = stage++;

            yield return null;
        }
    }
    public void playFinalLine()
    {
        guide.LookAt(player);
        animator.SetBool("isTalking", true);
        animator.SetBool("isWalking", false);
        audioPlayer.clip = guideAudios[3];
        audioPlayer.Play();
        stage = 9;
    }
    public void setDestination(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }
    public void setStage(int stage)
    {
        this.stage = stage;
    }
    public void lookAtPlayer()
    {
        guide.LookAt(player);
    }
}
