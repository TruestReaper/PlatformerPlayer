using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // variable to store player's health
    public int health = 100;

    // A reference to the health bar
    // must be set to inspector
    public DisplayBar healthBar;

    // adds knockback ability
    private Rigidbody2D rb;

    // knockback force when the player collides with an enemy
    public float knockbackForce = 5f;

    // a prefab to spawn when the player dies
    // this must be set in the inspector
    public GameObject playerDeathEffect;

    // static bool to keep track if the player has been hit recently
    public static bool hitRecently = false;

    // time in seconds to recover from being hit
    public float hitRecoveryTime = 0.2f;

    // variables to handle sound effects
    private AudioSource playerAudio;
    public AudioClip playerHitSound;
    public AudioClip playerDeathSound;

    // a reference to the player's animator
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // set animator reference
        animator = GetComponent<Animator>();

        // set the AudioSource reference
        playerAudio = GetComponent<AudioSource>();

        // set rigidbody reference
        rb = GetComponent<Rigidbody2D>();

        // check if rb is null
        if (rb == null)
        {
            // if rb is null, log an error message
            Debug.LogError("Rigidbody2D component not found on player");
        }

        // set the max value of the health bar to the player's health
        healthBar.SetMaxValue(health);

        // initialize the hitRecently bool to false
        hitRecently = false;
    }

    // a function to knock back the player when hit
    public void Knockback(Vector3 enemyPosition)
    {
        // if the player has been hit recently
        if (hitRecently)
        {
            // return out of the function
            return;
        }

        // set the hitRecently bool to true
        hitRecently = true;

        if (gameObject.activeSelf)
        {
            // start a coroutine to recover from being hit
            StartCoroutine(RecoverFromHit());
        }

        // calculate the direction of knockback
        Vector2 direction = transform.position - enemyPosition;

        // Normalize direction vector
        // This gives a consistent knockback force regardless of distance
        // between player and enemy
        direction.Normalize();

        // Add upward force to the knockback
        direction.y = direction.y * 0.5f + 0.5f;

        // Add force to the player in the direction of knockback
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    // Coroutine to reset the hitRecently bool after a delay of hitRecovery seconds
    IEnumerator RecoverFromHit()
    {
        // Wait for hitRecoveryTime (0.2) seconds
        yield return new WaitForSeconds(hitRecoveryTime);

        // Set hitRecently to false
        hitRecently = false;

        // set hit animation to false
        animator.SetBool("hit", false);
    }

    // A function to take damage
    public void TakeDamage(int damage)
    {
        // Subtract the damage from the health
        health -= damage;

        // Update the health bar
        healthBar.SetValue(health);

        // TODO: Play a sound effect when player takes damage
        // TODO: Play an animation for player taking damage

        // If the health is less than or equal to 0
        if (health <= 0)
        {
            // call Die method
            Die();
        }
        else
        {
            playerAudio.PlayOneShot(playerHitSound);

            // play the player hit animation
            animator.SetBool("hit", true);
        }
    }

        // function to die
        public void Die()
        {
            // set gameover to true
            ScoreManager.gameOver = true;

            // TODO: Play sound effect when player dies
            
            // Instantiate player death effect when player dies
             Instantiate(playerDeathEffect, transform.position, Quaternion.identity);

            // Disable player object
            gameObject.SetActive(false);
        }
    }