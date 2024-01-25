using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this speed to your liking

    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component attached to the player GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Call the function to handle player movement
        HandleMovement();
    }

    void HandleMovement()
    {
        
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, 0f);

        // Apply the movement to the player Rigidbody2D
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}