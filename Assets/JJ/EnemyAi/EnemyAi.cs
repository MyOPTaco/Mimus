using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    
    

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public bool objectInWay;
    

    ////Attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
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
        }
        else
        {
            Patroling();
        }

        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
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
        if(lastHit.transform != player)
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
}
