using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for health bar

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb; // Enemy's rigidbody component
    PlayerMovement player; // PlayerMovement class to find the player in the scene
    public float moveSpeed = 2f; // Enemy's movement speed
    Vector2 directionToPlayer; // Vector to get the direction to the player

    public Animator anim; // Animator component

    public GameObject sprite; // Separate sprite child object, for rotating the sprite

    public float maxHealth = 10; // Max enemy health
    public float health; // Enemy health

    bool facingLeft = true; // Boolean to determine if the enemy should flip directions or not

    public GameObject deathEffect; // Death effect

    public Image healthBar;

    public float damage = 5f; // Enemy damage

    public Material defaultMaterial;
    public Material hitMaterial;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the enemy's rigidbody component
        player = FindObjectOfType<PlayerMovement>(); // This finds the first object with the class PlayerMovement in the scene
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0) // Destroy this object and trigger death effect if health is 0 or lower
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.33f); // Destroy the death effect after delay
            Destroy(gameObject); // Destroy the enemy
        }

        // Determine movement direction to flip
        if(directionToPlayer.x >= 0 && facingLeft)
        {
            Flip();
        }
        else if(directionToPlayer.x < 0 && !facingLeft)
        {
            Flip();
        }

        // Set animation based on direction to player
        anim.SetFloat("dirX", directionToPlayer.x);
        anim.SetFloat("dirY", directionToPlayer.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        directionToPlayer = (player.transform.position - transform.position).normalized; // This calculates the vector to the player
        rb.MovePosition(rb.position + directionToPlayer * moveSpeed * Time.fixedDeltaTime); // This moves the enemy towards the player
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // If the collision is with a bullet
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>(); // Get the Bullet class from the object collision
            TakeDamage(bullet.damage);
        }
    }

    void TakeDamage(float damage) // Function to take damage
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        StartCoroutine(HitSprite()); // Starts a coroutine for the hit effect on the enemy
    }

    void Flip()
    {
        // Flips the sprite

        facingLeft = !facingLeft;
        sprite.transform.Rotate(0f, 180f, 0f);
    }

    IEnumerator HitSprite() // Coroutine for the hit effect
    {
        SpriteRenderer renderer = sprite.GetComponent<SpriteRenderer>(); // Gets the sprite renderer from the enemy sprite child object
        renderer.material = hitMaterial; // Changes the material of the sprite to the hit material
        yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds
        renderer.material = defaultMaterial;
    }
}
