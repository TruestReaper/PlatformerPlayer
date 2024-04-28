using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Rigidbody component of the projectile
    private Rigidbody2D rb;

    // Speed of the projectile with a default value of 20
    public float speed = 20f;

    // Damage of the projectile with a default value of 20
    public int damage = 20;

    // impact effect prefab for the projectile
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // get the enemy component of the object that was hit
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        // if the object that was hit has an enemy component
        if (enemy != null)
        { 
            // call the TakeDamage function of the Enemy component
            enemy.TakeDamage(damage); 
        }

        // if the object that was hit is not the player
        if (hitInfo.gameObject.tag != "Player")
        {
            // instantiate the impact effect
            Instantiate(impactEffect, transform.position, Quaternion.identity);

            // destroy the projectile
            Destroy(gameObject);
        }
       
    }
}
