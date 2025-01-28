using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab
    public Transform firePoint;     // The point where bullets are fired
    public float bulletSpeed = 20f; // Speed of the bullet
    private AudioSource shootingAudio;


    private void Start()
    {
        shootingAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // Change "Fire1" if using a different input
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!shootingAudio.isPlaying)
        {
            shootingAudio.Play();
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direaction = mousePos - transform.position;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direaction.y, direaction.x) * Mathf.Rad2Deg));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Use Rigidbody2D if 2D, or Rigidbody for 3D
        rb.velocity = direaction * bulletSpeed; // Adjust based on the forward direction

    }
}
