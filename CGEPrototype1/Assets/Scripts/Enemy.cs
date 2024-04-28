using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource EnemyDeathSound;
    public AudioSource EnemyDamageSound;

    // the enemy's health
    public int health = 100;

    // a prefab to spawn when the enemy dies
    public GameObject deathEffect;

    // a reference to the health bar
    private DisplayBar healthBar;

    // The damage the enemy deals to player
    public int damage = 10;

    private void Start()
    {
        // find the the health bar in the children of the enemy
        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null )
        {
            // if health bar is not found, log an error
            Debug.LogError("HealthBar (Display script) was not found");
            return;
        }
        // Set max value of the health bar to the enemy's health
        healthBar.SetMaxValue(health);
    }

    // A function to take damage
    public void TakeDamage(int damage)
    {
        EnemyDamageSound.Play();

        // subtract the damage from health
        health -= damage;

        // update the health bar
        healthBar.SetValue(health);

        // if health is less than or equal to 0
       if (health <= 0)
        {
            // call Die method
            Die();
        }
    }

    private void Die()
    {
        // Instantiate the death effect at the enemy's position
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        // Destroy the enemy
        Destroy(gameObject);

        EnemyDeathSound.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player health script from the player object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // check the player health script is null
            if (playerHealth == null) 
            {
                // Log an error if the player health scriot is null
                Debug.LogError("PlayerHealth script not found on player");
                return;
            }

            // Damage the player
            playerHealth.TakeDamage(damage);

            // Knockback the player
            playerHealth.Knockback(transform.position);
        }
    }
}
