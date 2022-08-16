using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditDoorScript : MonoBehaviour
{
    public Animator animator;
	void Start()
	{
        
	}

	void Update()
	{
        
    }

    private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag== "Player")
        {
            animator.SetBool("openDoor",true);
        }
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.tag == "Player")
        {
            animator.SetBool("openDoor",false);

        }

    }
}
