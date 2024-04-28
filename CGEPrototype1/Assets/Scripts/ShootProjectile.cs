using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public AudioSource LaserShot;

    // The projectile prefab to be spawned
    public GameObject projectilePrefab;

    // A reference to the firepoint transform
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        // If the player presses the fire button
        if (Input.GetButtonDown("Fire1"))
        {
            // call the shoot function
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Shot Fired");

        // Instantiate the projectile at the firepoint's position and rotation
        GameObject firedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Debug.Log(firedProjectile.gameObject.name);

        LaserShot.Play();

        // destroy the project after 3 seconds
        // Destroy(firedProjectile, 3f);
    }
}
