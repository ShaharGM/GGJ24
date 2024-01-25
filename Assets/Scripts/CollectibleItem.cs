using UnityEngine;

[System.Serializable]
public class CollectibleItem : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPosition;
    private Vector3 tempPosition;

    void Start()
    {
        startPosition = transform.position;
        tempPosition = startPosition;
    }

    void Update()
    {
        // Spin
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Float
        tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;

        transform.position = tempPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the effect on the player
            // For example, increase the player's score, health, etc.
            // You might need a reference to the player's script to call a method like other.GetComponent<PlayerScript>().IncreaseScore();
            Debug.Log("Collectible item collected!");

            // Hide or deactivate the collectible item
            gameObject.SetActive(false);

            // Alternatively, if you want to destroy the object completely, you can use:
            // Destroy(gameObject);
        }
    }
}
