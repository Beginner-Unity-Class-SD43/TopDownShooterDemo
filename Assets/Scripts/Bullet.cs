using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect; // Hit effect game object

    public float damage = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); // Instantiate the hit effect at the bullet's position
        Destroy(effect, 0.2f); // Destroy the hit effect after 0.2 seconds
        Destroy(gameObject); // Destroy the bullet
    }
}
