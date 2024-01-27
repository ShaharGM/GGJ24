using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public float power = 10.0f;
    public float circleCastRadius = 0.5f;
    public float attackRange = 1.0f;
    public float rotationSpeed = 720f; // Degrees per second
    private bool is_attacking = false;
    private bool swingDirection = false;
    private float currentAngle = 0f;
    private Transform parentTransform;
    public LayerMask enemyLayer;
    private bool rayLeftHit = false;
    private bool rayRightHit = false;
    public LayerMask groundLayer;

    private void Awake()
    {
        // Get the parent's transform
        parentTransform = transform.parent;

        // Optional: Check if the parentTransform is not null
        if (parentTransform == null)
        {
            Debug.LogError("LongSword does not have a parent GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rayRightHit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 5f, groundLayer);
            swingDirection = rayRightHit;
            Attack();
        }
        if(is_attacking)
        {
            Move();
        }
        
    }

    private void Attack()
    {
        // Check if the sword has a parent and use its transform, otherwise use the sword's own transform
        Debug.Log($"Sword attacking with power {power}");
        PerformBasicSlash();
    }

    private void PerformBasicSlash()
    {
        // Start the attack rotation
        currentAngle = 500f; // Start from behind the player (180 degrees)
        is_attacking = true;
    }

    private void Move()
    {
        if (is_attacking && parentTransform != null)
        {
            // Calculate rotation for this frame
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            
            // Rotate the sword around the parent
            
            transform.RotateAround(parentTransform.position, swingDirection ? Vector3.forward : Vector3.back, rotationThisFrame);
            
            // Update the current angle
            currentAngle -= rotationThisFrame;

            // Check if rotation is complete
            if (currentAngle <= 0f)
            {
                is_attacking = false;
                currentAngle = 0f;
                // You might want to reset the sword's position relative to the parent here
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object that should stop the animation (you might want to refine this condition)
        if (collision.gameObject.CompareTag("Enemy") && is_attacking)
        {
            // Call the OnHit function of the enemy
            Debug.Log("Enemy Hit!");
            collision.gameObject.GetComponent<EnemyBase>().OnHit(power, this.gameObject);
            is_attacking = false;
        }
        else if (collision.gameObject.layer == 6)
        {
            is_attacking = false;
            
        }

        


    }

    void OnDrawGizmos()
    {
        // Visualize the ground check ray
        Gizmos.color = rayLeftHit ? Color.green : Color.red;
        // Gizmos.DrawLine(center, center + Vector2.up * 5f);
        Gizmos.color = rayRightHit ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector2.right) * 5f);
    }

}
