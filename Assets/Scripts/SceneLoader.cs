using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool playerIsHere=false;
    public int sceneIndex;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsHere = true;
        }
    }
}
