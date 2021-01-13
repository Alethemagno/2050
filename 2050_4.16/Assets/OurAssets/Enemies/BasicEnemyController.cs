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

    public AudioClip gunShot;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public GameObject[] walkPoints;
    public GameObject playerGO;
    public int walkPointCounter;

    //Attacking
    public float attackDelay;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange, visionConeAngle;
    public bool playerInSightRange, playerInAttackRange;

    private RaycastHit lastHit;
    private float sinceLastAction;

    private void Awake()
    {
        playerGO = GameObject.Find("Player");
        player = playerGO.transform;
        agent = GetComponent<NavMeshAgent>();
        myLight.color = Color.blue;
        sinceLastAction = 0;
        myLight.intensity = 0.0f;
    }

    private void Update()
    {
        //Check for sight and attack range
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsVisible);
        playerInSightRange = checkSight();
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && playerInAttackRange) ChasePlayer();
        if (!playerInSightRange && playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private bool checkSight() {
        RaycastHit hit;
        Vector3 startingDirection = Quaternion.AngleAxis(-sightAngle, Vector3.up) * transform.forward;        
        Vector3 finalRayDirection = startingDirection;
        for (int i = 0; i < 7; i++) {
            if (Physics.Raycast(transform.position, finalRayDirection, out hit, sightRange, whatIsVisible)) {
                Debug.Log("I see " + hit.transform.tag);
                if(hit.transform.tag == "Player") {
                    return true;
                }   
            }
            finalRayDirection = (Quaternion.AngleAxis(angleOffset, Vector3.up)) * finalRayDirection;
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
        sinceLastAction = 0;
        walkPointSet = false;
        myLight.color = Color.blue;
        if (!walkPointSet) {
            walkPoint = walkPoints[walkPointCounter].transform.position;
            walkPointSet = true;
            Debug.Log("Walking to Walkpoint " + walkPointCounter);
        }

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.0f) {
            walkPointCounter++;
            float c = 0;
            c += Time.deltaTime;
            if (c >= 5) {
                walkPointSet = false;
            }
            if (walkPointCounter == walkPoints.Length - 1) {
                walkPointCounter = 0;  
            }
        }
    }
    

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        myLight.color = Color.white;
        transform.LookAt(player.position);
        //Debug.Log("Chasing the player");
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        myLight.color = Color.red;
        if (sinceLastAction == 0) {
            AudioSource.PlayClipAtPoint(gunShot, transform.position, 1.0f);
        }
        if (sinceLastAction < attackDelay) {
            sinceLastAction += Time.deltaTime;
            //Debug.Log("timer = " + sinceLastAction);
        } 
        if (sinceLastAction >= attackDelay) {
            myLight.color = Color.red;
            killPlayer();
            Debug.Log("I ran killPlayer");
            sinceLastAction = 0;
        }
    }
    private void killPlayer() {
        //Debug.Log("killPlayer ran successfully");
        player.SendMessage("playerDeath");
        myLight.color = Color.blue;
        Debug.Log("I sent you to " + playerGO.GetComponent<PlayerMovement>().lastCheckpoint);
        player.transform.position = playerGO.GetComponent<PlayerMovement>().lastCheckpoint;

    }

    public void ARGlassesActivated() {
        myLight.intensity = 15.0f;
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
        //Gizmos.DrawLine(transform.position, (transform.TransformDirection(Vector3.forward)*5));
        Gizmos.DrawLine(transform.position, transform.forward*5);
        Gizmos.color = Color.yellow;
        Vector3 startingDirection = Quaternion.AngleAxis(-sightAngle, Vector3.up) * transform.forward;
        //Vector3 startingRayDirection = Quaternion.AngleAxis(sightAngle, Vector3.up) * transform.forward;
        Vector3 finalRayDirection = startingDirection;
        //Vector3 finalDirection = startingRayDirection;
        Gizmos.DrawLine(transform.position, transform.position + startingDirection);
        Gizmos.color = Color.yellow;
        for (int i = 0; i < 7; i++) {
            Gizmos.DrawLine(transform.position, transform.position + sightRange*(finalRayDirection));
            finalRayDirection = Quaternion.AngleAxis(angleOffset, Vector3.up) * finalRayDirection;
        }
    }
}
