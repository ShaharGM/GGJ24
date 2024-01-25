using UnityEngine;


[System.Serializable]
public class EnemyBase : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private Color originalColor = Color.red;
    private Color hitColor = Color.blue;
    private float hitDuration = 0.5f;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = originalColor;
    }

    public virtual void OnHit()
    {
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
