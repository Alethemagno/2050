using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsVisible, whatIsPlayer;
    public float health, sightAngle, angleOffset;
    public Light myLight;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public GameObject[] walkPoints;
    public int walkPointCounter;

    //Attacking
    public float attackDelay;
    bool alreadyAttacked;
    public GameObject projectile;
    RaycastHit lastSeen;

    //States
    public float sightRange, attackRange, visionConeAngle;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        myLight.color = Color.blue;
    }

    private void Update()
    {
        //Check for sight and attack range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsVisible);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = checkSight();

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private bool checkSight() {
        Vector3 vectorToPlayer = player.position-transform.position;
        if (vectorToPlayer.magnitude <= attackRange) {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle) {
                RaycastHit hit;
                Vector3 startingDirection = Quaternion.AngleAxis(-sightAngle, Vector3.up) * this.transform.forward;
                Vector3 finalRayDirection = startingDirection;
                for (int i = 0; i < 7; i++) {
                    if (Physics.Raycast(transform.position, finalRayDirection, out hit, sightRange, whatIsVisible)) {
                        lastSeen = hit;
                        return true;
                    }
                    finalRayDirection = (Quaternion.AngleAxis(angleOffset, Vector3.up)) * finalRayDirection;
                }
                
            }
            //goThere(lastSeen);
        }
        return false;
    }

    private void goThere(Vector3 loc)
    {
        walkPoint = loc;
        agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
            Patroling();
    }

    private void Patroling()
    {
        walkPointSet = false;
        myLight.color = Color.blue;
        if (!walkPointSet) {
            walkPoint = walkPoints[walkPointCounter].transform.position;
            walkPointSet = true;
        }

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointCounter++;
            walkPointSet = false;
        }
    }
    

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        myLight.color = Color.blue;
        //Debug.Log("Chasing the player");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);
        transform.LookAt(player);
        myLight.color = Color.white;
        float sinceLastAction = 0;
        while (sinceLastAction < attackDelay) {
            sinceLastAction += Time.deltaTime;
        } 
        if (sinceLastAction >= attackDelay) {
            myLight.color = Color.red;
            Invoke("killPlayer", 0.5f);
        }
    }
    private void killPlayer()
    {
        lastSeen.collider.SendMessage("playerDeath");
        myLight.color = Color.blue;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (transform.TransformDirection(Vector3.forward)*5));
        Vector3 startingDirection = Quaternion.AngleAxis(-sightAngle, Vector3.up) * transform.forward;
        Vector3 finalRayDirection = startingDirection;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < 7; i++) {
            Gizmos.DrawLine(transform.position, finalRayDirection);
            finalRayDirection = Quaternion.AngleAxis(angleOffset, Vector3.up) * finalRayDirection;
        }
    }
}
