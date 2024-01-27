using UnityEngine;


[System.Serializable]
public class EnemyBase : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    public float health = 10f;
    private Color originalColor = Color.red;
    private Color hitColor = Color.blue;
    private float hitDuration = 0.5f;
    public float SwordSizeDecrease = 0.2f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 startPosition;
    private Vector3 tempPosition;
    private AudioSource audio;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Float
        tempPosition = startPosition;
        tempPosition.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;

        transform.position = tempPosition;
    }

    public virtual void OnHit(float damage, GameObject sword)
    {
        audio.Play();
        health -= damage;
        if (health <= 0){
            Destroy(this.gameObject, audio.clip.length);
            Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
            Vector3 newScale = sword.transform.localScale;
            newScale.y -= SwordSizeDecrease;
            rb.mass -= SwordSizeDecrease;
            sword.GetComponent<SwordMovement>().power -= SwordSizeDecrease * 10;
            sword.transform.localScale = newScale;
            
        }
        StopCoroutine("HandleHit"); // Stop the coroutine if it's already running
        StartCoroutine("HandleHit");
    }

    private System.Collections.IEnumerator HandleHit()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        spriteRenderer.color = originalColor;
    }
}
