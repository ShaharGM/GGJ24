using UnityEngine;
using System.Collections;

[System.Serializable]
public class CollectibleItem : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    public float SwordSizeIncrease = 0.2f;

    public AudioSource audioSource;
    public ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;

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
            audioSource.Play();
            particleSystem.Play();
            StartCoroutine(DisableComponentsRoutine());

            // Hide or deactivate the collectible item
            // gameObject.SetActive(false);

            // Alternatively, if you want to destroy the object completely, you can use:
            // Destroy(gameObject);

            // Find the GameObject with the "Sword" tag
            GameObject sword = GameObject.FindGameObjectWithTag("Sword");
            Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
            if (sword != null)
            {
                // Increase the Y scale of the sword by scaleYIncrease
                Vector3 newScale = sword.transform.localScale;
                newScale.y += SwordSizeIncrease;
                rb.mass += SwordSizeIncrease;
                sword.transform.localScale = newScale;
                sword.GetComponent<SwordMovement>().power += SwordSizeIncrease * 10;
            }
            else
            {
                Debug.LogWarning("Sword object not found");
            }
        }
    }

    private IEnumerator DisableComponentsRoutine()
    {
        // Disable the SpriteRenderer and Collider immediately
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;

        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Disable the entire GameObject
        gameObject.SetActive(false);
    }
}
