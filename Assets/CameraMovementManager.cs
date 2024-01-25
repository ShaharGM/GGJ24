using UnityEngine;

public class CameraMovementManager : MonoBehaviour
{
    public Transform target;  // Reference to the player's Transform
    public float smoothTime = 0.3f;  // Adjust this for smoothness of camera movement
    public Vector3 offset;  // Adjust this to set the desired offset between player and camera

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // Check if the target (player) is not null
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);

            // Smoothly interpolate between the current camera position and the desired position using SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}