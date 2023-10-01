using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for health bar

public class Player : MonoBehaviour
{
    public Image healthBar;

    public float maxHealth = 50;
    public float health;

    SpriteRenderer spriteRenderer;

    bool isInvincible;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component for flicker
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvincible) // If the player collides with the enemy and the player isn't in the invincibility state, take damage
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // Get enemy's class

            TakeDamage(enemy.damage); // Take damage based on enemy's damage value
        }
    }

    private void TakeDamage(float damage) // Take damage based on a certain amount
    {

        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        StartCoroutine(Invincibility()); // Starts the invincibility state coroutine
    }

    IEnumerator Invincibility()
    {
        isInvincible = true; // Set invincible to true

        for (int i = 0; i < 5; i++) // Run the flicker loop 5 times
        {
            // This code flickers the player sprite by turning the sprite renderer on and off
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds
            spriteRenderer.enabled = false; // Disables sprite renderer
            yield return new WaitForSeconds(0.1f); // Wait another 0.1 seconds
            spriteRenderer.enabled = true; // Re-enables sprite renderer
        }

        isInvincible = false;
    }
}
