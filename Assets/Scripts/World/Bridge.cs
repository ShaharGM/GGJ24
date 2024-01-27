using UnityEngine;
using UnityEngine.Events;

public class BridgeController : MonoBehaviour
{
    public float breakingPoint = 10f;      // Configurable breaking point value
    public HingeJoint2D hinge;             // Reference to the Hinge Joint 2D
    public Collider2D bridgeCollider;      // Reference to the bridge's Collider2D
    public Rigidbody2D playerRigidbody;    // Reference to the player's Rigidbody2D
    public GameObject sword;               // Reference to the sword GameObject
    public UnityEvent BridgeBroke;         // Event to be invoked when the bridge breaks

    private bool isBroken = false;         // Flag to track whether the bridge is broken

    private void Update()
    {
        // Check if the player's Rigidbody2D is in contact with the bridge's Collider2D
        if (IsPlayerOnBridge() && IsSwordScaleGreater())
        {
            // Disable the Hinge Joint 2D to break the bridge
            if (!isBroken)
            {
                TurnOffHingeJoint(hinge);
                isBroken = true;

                // Invoke the BridgeBroke event
                if (BridgeBroke != null)
                {
                    BridgeBroke.Invoke();
                }
            }
        }
    }

    private bool IsPlayerOnBridge()
    {
        if (playerRigidbody != null && bridgeCollider != null)
        {
            // Check if the player's Rigidbody2D is touching the bridge's Collider2D
            return playerRigidbody.IsTouching(bridgeCollider);
        }

        return false;
    }

    private bool IsSwordScaleGreater()
    {
        // Check if the Y scale of the sword is greater than the breaking point
        if (sword != null)
        {
            Vector3 swordScale = sword.transform.localScale;
            return swordScale.y > breakingPoint;
        }

        return false;
    }

    private void TurnOffHingeJoint(HingeJoint2D hingeJoint)
    {
        // Disable the Hinge Joint 2D to break the bridge
        if (hingeJoint != null)
        {
            hingeJoint.enabled = false;
        }
    }
}
