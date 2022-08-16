using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class DoorMovingScript : MonoBehaviour
{

    //private float maximumOpening = 10f;
    //private float minimumOpening = 0f;

    private bool playerIsHere;

    private float moveSpeed = 0.1f;

    public Vector3 door1endpos;
    public Vector3 door1startpos;

    public Vector3 door2endpos;
    public Vector3 door2startpos;

    public Transform door1;
    public Transform door2;
    private Collider doorCollider;
    private AudioSource audioSource;
    public AudioSource cityAudioSource;


    //public float speed = 0.2f;

    //private bool opening = true;
    //private bool moving = false;

    //private float delay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
        //startpos = transform.position;
        doorCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            door1.transform.Translate(door1endpos * moveSpeed);
            door2.transform.Translate(door2endpos * moveSpeed);


        }
        else
        {
            //transform.position = Vector3.Lerp(transform.localPosition, startpos, moveSpeed);
        }
        //if (playerIsHere)
        //{
        //    MoveDoor(endpos);

        //}
        //else
        //{
        //    MoveDoor(startpos);
        //}
    }

    void MoveDoor(Vector3 goalpos)
    {
        //float dist = Vector3.Distance(transform.position, goalpos);
        //if (dist > .1)
        //{
        //    transform.position = Vector3.Lerp(transform.position, goalpos, moveSpeed);
        //}
        //else
        //{
        //    playerIsHere = false;
        //}
    }

    //public bool Moving
    //{
    //    //get { return moving; }
    //    //set { moving = value; }
    //}

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            playerIsHere = true;
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsHere = false;
            if (cityAudioSource != null)
            {
                if (cityAudioSource.volume == 1f)
                {
                    cityAudioSource.volume = 0.3f;
                }
                else
                {
                    cityAudioSource.volume = 1f;
                }
            }
        }

    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
