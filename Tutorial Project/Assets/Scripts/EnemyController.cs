using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    // Creating 3 status
    
    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack
    public float timeBetweenAttack;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject projectilePos;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //Patrol State
        if (!playerInSightRange && !playerInAttackRange) Patrol();
        //Chase State
        if (playerInSightRange && !playerInAttackRange) Chase();
        //Attack state
        if (playerInSightRange && playerInAttackRange) Attack();

    }
    //Patrol
    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        
        if (walkPointSet)
            agent.SetDestination(walkPoint);

            Vector3 distanceWalkPoint = transform.position - walkPoint; 
        
        //WalkPoint reached
        if (distanceWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }
    private void SearchWalkPoint()
    {
        //Calculating new point
        float ranXVal = Random.Range(-walkPointRange,walkPointRange);
        float ranZVal = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + ranXVal, transform.position.y, transform.position.z + ranZVal);

        if(Physics.Raycast(walkPoint,-transform.up,2f, groundLayer))
        {
            walkPointSet = true;
        }
    }
    //Chase
    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked) 
        {
            Rigidbody bulletrb = Instantiate(projectile, projectile.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bulletrb.AddForce(transform.forward * 16f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttack);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    
    






}
