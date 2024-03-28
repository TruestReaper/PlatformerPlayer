using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float horizontalInput;

    private Animator animator;

    // audio clip to hold jump sound
    public AudioClip jumpSound;

    // audio source to play our sounds
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        // check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Apply upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    void FixedUpdate()
    {
        // move the player using Rigidbody2D in FixedUpdate
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // set animator parameter xVelocityAbs to absolute of x velocity
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));

        // set animator parameter yVelocity to y velocity
        animator.SetFloat("yVelocity", rb.velocity.y);

        // check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // set animator parameter onGround to isGrounder
        animator.SetBool("onGround", isGrounded);

        // Ensure the player is facing the direction of movement
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // facing right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // facing left
        }
    }
}
