
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust this speed to your liking
    public float jumpForce = 10f;  // Adjust the jump force
    public LayerMask groundLayer;  // Assign the ground layer in the Inspector

    private Rigidbody2D rb;
    private bool isGrounded;
    private float raycastOffset;
    private Animator animator;
    void Start()
    {
        // Get the Rigidbody2D component attached to the player GameObject
        rb = GetComponent<Rigidbody2D>();
        raycastOffset = GetComponent<Collider2D>().bounds.extents.y + 0.01f; // Adding a small offset for reliability
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.Raycast(GetRaycastOrigin(), Vector2.down, 0.2f, groundLayer);

        // Call the function to handle player movement
        HandleMovement();
        animator.SetBool("isJumping", !isGrounded);

        // Check for jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void HandleMovement()
    {
        
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");

        animator.SetBool("isRunning", horizontalInput != 0);

        // Rotate the player based on the direction they are moving
        if (horizontalInput > 0)
        {
            // Moving right, set rotation to 0 (facing right)
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            // Moving left, set rotation to 180 (facing left)
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, 0f);

        // Apply the movement to the player Rigidbody2D
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        // Add upward force to make the player jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
    }

    Vector2 GetRaycastOrigin()
    {
        // Calculate the starting point of the raycast from the bottom of the player
        return new Vector2(transform.position.x, transform.position.y - raycastOffset);
    }

    void OnDrawGizmos()
    {
        // Visualize the ground check ray
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawLine(GetRaycastOrigin(), GetRaycastOrigin() + Vector2.down * 0.2f);
    }
}