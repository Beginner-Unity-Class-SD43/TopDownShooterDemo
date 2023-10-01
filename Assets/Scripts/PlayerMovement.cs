using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5; // Player's move speed

    Rigidbody2D rb; // Player's rigidbody2D component

    Vector2 movement; // Player's movement vector

    bool facingRight = true; // Boolean to determine if the player should flip direction

    Animator anim; // Animator component

    Vector2 mousePos; // Mouse position vector;

    public GameObject gun; // Gun object

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize(); // Normalize the movement vector (if both the x input and the y input are being pressed at the same time, this will make the vector = 1, just like up and down.

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // Animator
        anim.SetFloat("mouseX", mousePos.x);
        anim.SetFloat("mouseY", mousePos.y);

        // Flips the player sprite based on the mouse position
        if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Moves the player

        // Rotate the gun
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    void Flip()
    {
        // Flips the sprite

        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
