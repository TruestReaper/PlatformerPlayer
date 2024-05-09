using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveFlyingChase : MonoBehaviour
{
    // an array of waypoints the enemy will move between
    public GameObject[] patrolPoints;

    // public variables for movement
    public float speed = 2f;
    public float chaseRange = 3f;

    // enemy state enum
    public enum EnemyState
    {
        PATROLLING,
        CHASING
    }

    // current enemy state
    public EnemyState currentState = EnemyState.PATROLLING;

    // variables for targetting the where the enemy will move
    public GameObject target;
    private GameObject player;

    // Rigidbody2D component for the enemy
    private Rigidbody2D rb;

    // SpriteRenderer component for the enemy
    private SpriteRenderer sr;

    // current patrol point index
    private int currentPatrolPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // find the player object
        player = GameObject.FindWithTag("Player");

        // get the Rigidbody2D component of the enemy
        rb = GetComponent<Rigidbody2D>();

        // Ger the SpriteRenderer component of the enemy
        sr = GetComponent<SpriteRenderer>();

        // check if patrol points are assigned
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned!");
        }

        // Set the initial target to first patrol point
        target = patrolPoints[currentPatrolPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        // update the state based on player and target distance
        UpdateState();

        // move and face based on current state
        switch (currentState)
        {
            case EnemyState.PATROLLING:
                Patrol();
                break;
            case EnemyState.CHASING:
                ChasePlayer();
                break;
        }

        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }

    void UpdateState()
    {
        if (IsPlayerInChaseRange() && currentState == EnemyState.PATROLLING)
        {
            currentState = EnemyState.CHASING;
        }
        else if (!IsPlayerInChaseRange() && currentState == EnemyState.CHASING)
        {
            currentState = EnemyState.PATROLLING;
        }
    }

    bool IsPlayerInChaseRange()
    {
        if (player == null)
        {
            Debug.LogError("Player not found");
            return false;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= chaseRange;
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= 0.5f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        target = patrolPoints[currentPatrolPointIndex];

        MoveTowardsTarget();
    }

    void ChasePlayer()
    {
        target = player;
        MoveTowardsTarget();
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = target.transform.position - transform.position;

        // normalize direction
        direction.Normalize();

        // move towards target with rb
        rb.velocity = direction * speed;

        // Face forward
        FaceForward(direction);
    }

    private void FaceForward(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sr.flipX = false;
        }
        else if (direction.x > 0)
        {
            sr.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (GameObject point in patrolPoints)
            {
                Gizmos.DrawWireSphere(point.transform.position, 0.5f);
            }
        }
    }
}