using UnityEngine;

namespace Weapons
{
    public class LongSword : SwordBase
    {
        public float rotationSpeed = 360f; // Degrees per second
        private Transform parentTransform;
        private float currentAngle = 0f;

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

        protected override void PerformBasicSlash()
        {
            Debug.Log("LongSword Attack!");
            // Start the attack rotation
            currentAngle = 180f; // Start from behind the player (180 degrees)
            base.is_attacking = true;
        }

        protected override void Move()
        {
            if (base.is_attacking && parentTransform != null)
            {
                // Calculate rotation for this frame
                float rotationThisFrame = rotationSpeed * Time.deltaTime;
                
                // Rotate the sword around the parent
                transform.RotateAround(parentTransform.position, Vector3.forward, rotationThisFrame);
                
                // Update the current angle
                currentAngle -= rotationThisFrame;

                // Check if rotation is complete
                if (currentAngle <= 0f)
                {
                    base.is_attacking = false;
                    currentAngle = 0f;
                    Debug.Log("Attack completed");
                    // You might want to reset the sword's position relative to the parent here
                }
            }
        }
    }
}
