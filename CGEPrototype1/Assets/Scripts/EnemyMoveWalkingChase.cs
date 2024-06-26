using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// require a RigidBody2D and an Animator on the enemy
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMoveWalkingChase : MonoBehaviour
{
    public float chaseRange = 4f;

    public float enemyMovementSpeed = 2f;

    // private references

    // transform of the player
    private Transform playerTransform;

    private Rigidbody2D rb;

    private Animator anim;

    // sprite renderer of the enemy
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        /*if (playerTransform == null)
        {
            Debug.LogError("Player transform not found. Ensure player has 'Player' tag.");
        }*/
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 from the enemy to the player
        Vector2 playerDirection = playerTransform.position - transform.position;


        // distance the enemy from player
        float distanceToPlayer = playerDirection.magnitude;

        Debug.Log("Distance to Player: " + distanceToPlayer);

        if (distanceToPlayer <= chaseRange)
        
        {
            // we need the direction to move towards the player
            // and we're not moving up or down with this enemy

            // Normalizing the vector
            // this gives us the direction without the magnitude or length
            playerDirection.Normalize();

            // set the y axis to 0 so enemy doesn't move up or down
            playerDirection.y = 0f;

            // Rotate the enemy to face the player
            FacePlayer(playerDirection);

            // if there is ground ahead of the enemy
            if (IsGroundAhead())
            {
                MoveTowardsPlayer(playerDirection);
            }
            else
            {
                // stop moving if there is no ground ahead
                StopMoving();
                Debug.Log("No ground ahead");
            }
        }
        else
        {
            // stop moving if player is not within chase range
            StopMoving();
        }
       
    }

    // bool function to check if there is ground ahead of the enemy
    bool IsGroundAhead()
    {
        // ground check variables

        // Distance to check for ground
        float groundCheckDistance = 2.0f;

        // LayerMask for the ground
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        Debug.Log("Ground layer: " + groundLayer);

        // determine which direction the enemy is facing
        Vector2 enemyFacingDirection = (sr.flipX == false) ? Vector2.left : Vector2.right;

        // Raycast to check for ground ahead of the enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);

        // draw a line to visualize the raycast
        Debug.DrawRay(transform.position, Vector2.down + enemyFacingDirection, Color.red);

        Debug.Log("IsGroundAhead : " + ((hit.collider != null)));

        // Return true if ground is detected
        return hit.collider != null;
    }

    private void FacePlayer (Vector2 playerDirection)
    {
        if (playerDirection.x < 0)
        {
            // face right
            // transform.rotation = Quaternion.Euler(0, 0, 0);
            sr.flipX = false;
        }
        else
        {
            // face left
            // transform.rotation = Quaternion.Euler(0, 180, 0); 
            sr.flipX = true;
        }
        Debug.Log("Facing player: " + (sr.flipX ? "left" : "right"));
    }
    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        Debug.Log("move towards start");
        Debug.DrawRay(transform.position, playerDirection, Color.green);

        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);

        anim.SetBool("isMoving", true);
        Debug.Log("move towards finish");

    }

    private void StopMoving()
    {
        Debug.Log("stop moving start");

        rb.velocity = new Vector2(0, rb.velocity.y);

        anim.SetBool("isMoving", false);

        Debug.Log("stop moving finish");

    }
}
