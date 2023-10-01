using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Press mouse button to fire
        {
            Shoot(); // Shoot function
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Instantiates a bullet at the firePoint position

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Gets the bullet's rigidbody component
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // Adds force to the bullet
    }

}
