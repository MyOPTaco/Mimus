using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy_AIRecode : MonoBehaviour
{
    #region assets
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;


    //Audio / Scream
    public int randomizedNum;
    public AudioSource playerrrrrr;


    //Patroling
    public Vector3[] office, officeB, officeC, officeD, officeE, officeF;
    public Vector3[] startPoints;
    private int roomStorage;
    private bool currentlyPatrolling;
    public int currentPointIndex;
    public int randomizedRoom;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public bool objectInWay;
    public int waitTime;

    //Noise Detection
    public Transform noiseSource;
    public bool noiseDetection;
    public int detectionRange;

    ////Attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    #endregion
  
    
    private void Awake()
    {
        InvokeRepeating(nameof(MakeNoise), randomizedNum, randomizedNum);
        randomizedNum = Random.Range(180, 360);
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    
   
   
    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if (objectInWay) Patroling();

        RaycastToPlayer();

        if (playerInSightRange && !objectInWay)
        {
            ChasePlayer();
        }if (noiseDetection == true)
        {
            PatrolNoise();
        }else 
        {
            Patroling();
        }
        //else
        //{
        //    randomizedRoom = Random.Range(1, 7);
            
        //}
        

        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    

    #region noisePatrol
    void MakeNoise()
    {
        randomizedNum = Random.Range(180, 360);
        playerrrrrr.Play(0);
        noiseSource = player;
        noiseDetection = true;
    }
    public void PatrolNoise()
    {
        agent.SetDestination(noiseSource.position);
        if (Vector3.Distance(this.gameObject.transform.position, noiseSource.position) < 2.5f)
        {
            
            noiseDetection = false;
            
            //will expand upon this and add a condensed patrol once that point is reached, and will have it move out of the room after a set period of time
        }
    }
    IEnumerator Listening()
    {
        yield return new WaitForSeconds(waitTime);
       sightRange = sightRange + 7f;
        currentPointIndex = 0;
        currentlyPatrolling = false;
    }
    #endregion

    #region patrol
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 0.1f)
        {
             currentPointIndex++;
             walkPointSet = false;
        }
            
    }
    private void SearchWalkPoint()
    {
        currentlyPatrolling = true;
        if(randomizedRoom == 1)
        {
            roomStorage = 1;
            if(currentPointIndex + 1 < office.Length)
            {
                walkPoint = office[currentPointIndex];
                walkPointSet = true;
            }
            else
            {
                StartCoroutine(Listening());
               
            }
            
        }
        if (randomizedRoom == 2)
        {
            roomStorage = 2;
            if (currentPointIndex + 1 < officeB.Length)
            {
                walkPoint = officeB[currentPointIndex];
                walkPointSet = true;
            }
            else
            {
                StartCoroutine(Listening());

            }
        }
        if (randomizedRoom == 3)
        {
            roomStorage = 3;
        }
        if (randomizedRoom == 4 )
        {
            roomStorage = 4;
        }
        if (randomizedRoom == 5)
        {
            roomStorage = 5;
        }

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void roomSet()
    {
        randomizedRoom = Random.Range(1, 7);
        if (randomizedRoom != roomStorage)
        {
            agent.SetDestination(startPoints[randomizedRoom]);
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    #endregion


    #region misc
    //private void AttackPlayer()
    //{
    //    //Make sure enemy dosent move
    //    agent.SetDestination(transform.position);

    //    transform.LookAt(player);

    //    if (!alreadyAttacked)
    //    {
    //        ///Attack code here
    //        Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
    //        ///

    //        alreadyAttacked = true;
    //        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    //    }
    //}
    //private void ResetAttack()
    //{
    //    alreadyAttacked = false;
    //}

    private void RaycastToPlayer()
    {
        var ray = new Ray(origin: this.transform.position, direction: player.position - this.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance: 15))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point;
        }
        if (lastHit.transform != player)
        {
            objectInWay = true;
        }
        else if (lastHit.transform == player)
        {
            objectInWay = false;
        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawLine(this.transform.position, player.position);
    }
    #endregion
}
