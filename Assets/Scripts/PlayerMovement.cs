using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded = true; // Add more robust ground checking logic as needed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    void OnJump(InputValue jumpValue)
    {
        if (isGrounded && jumpValue.isPressed)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // This should be set to true by your ground checking logic
        }
    }

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    // Implement a proper collision detection with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
