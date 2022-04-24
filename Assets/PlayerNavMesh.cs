using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNavMesh : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform movePositionTransform;
    LineRenderer linePath;
    public float diff;
    public GameObject player;
    public GameObject guide;
    public Collider playerCollider;
    public Collider guideCollider;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        linePath = GetComponent<LineRenderer>();
        linePath.startWidth = 0.15f;
        linePath.endWidth = 0.15f;
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position,guide.transform.position);
        navMeshAgent.SetDestination(movePositionTransform.position);
        RaycastHit hitInfo;
        Vector3 direction = playerCollider.transform.position - guideCollider.transform.position;
        bool hit = Physics.BoxCast(guideCollider.bounds.center, transform.localScale, direction,out hitInfo);
        if (hit && hitInfo.collider.name == "Player" && distance < diff)
        {
            print(hitInfo.collider.name);
            print(hit);
            navMeshAgent.isStopped = false;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
        /*if (distance > diff)
        {
            navMeshAgent.isStopped = true;
            
        }
        else
        {
            navMeshAgent.isStopped = false;
        }*/
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
