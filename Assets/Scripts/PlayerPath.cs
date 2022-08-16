using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPath : MonoBehaviour
{
    public Transform targetPlayer;
    private NavMeshAgent navMeshAgent;
    private LineRenderer linePath;
    public Transform player;
    public Transform anchor;
    private float yOffset;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        linePath = GetComponent<LineRenderer>();
        anchor = GetComponent<Transform>();
        yOffset = anchor.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(targetPlayer.position);
        navMeshAgent.isStopped = true;
        anchor.position = new Vector3(player.position.x, yOffset, player.position.z);
        navMeshAgent.isStopped = true;
        renderPath();
    }
    void renderPath()
    {
        linePath.positionCount = navMeshAgent.path.corners.Length;
        linePath.SetPosition(0, transform.position);
        if (navMeshAgent.path.corners.Length >= 2)
        {
            for (int i = 0; i < navMeshAgent.path.corners.Length; i++)
            {
                linePath.SetPosition(i, navMeshAgent.path.corners[i]);
            }
        }


    }
}
