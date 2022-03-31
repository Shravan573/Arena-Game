using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisground, whatisplayer,targetMask, obstructionMask;

    //patrolling
    public Vector3 walkpoint;
    bool walkpointset;
    public float walkpointrange;
   
    [Range(0,360)]
    public float angle;

    //in range
    public float sightrange;
    public bool playerinsight ;
    public GameObject gameover;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }
 

    private void Update()
    {
        playerinsight = Physics.CheckSphere(transform.position, sightrange, whatisplayer);

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightrange, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < sightrange / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    playerinsight = true;
                else
                    playerinsight = false;
            }
            else
                playerinsight = false;
        }
        else if (playerinsight)
            playerinsight = false;
    
        if (!playerinsight )
        {
            Patrolling();
        }
        if (playerinsight)
        {
            Chase();
        }
       

    }

    private void Patrolling()
    {
        if (!walkpointset) SearchWalkPoint();
        if (walkpointset)
        {
            agent.SetDestination(walkpoint);

            Vector3 distancetothewalkpoint = transform.position - walkpoint;
            if (distancetothewalkpoint.magnitude < 1f)
            {
                Debug.Log("walkpoint not set");
                walkpointset = false;
            }
        }
    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);
        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatisground))
        {
            walkpointset = true;
        }
    }
    private void Chase()
    {
        if (playerinsight)
        {
            agent.SetDestination(player.position);
            gameover.SetActive(true);
            Time.timeScale = 0f;
        }else
        {
            gameover.SetActive(false);
            
        }
        
        
    }


   
    //private void OnDrawGizmosSelected()
    //{
       
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, sightrange);
    //}


}

