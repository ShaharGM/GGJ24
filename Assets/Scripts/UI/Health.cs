using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; // Namespace for Unity Events

[System.Serializable]
public class Health : MonoBehaviour
{
    public Image targetImage; // The image component you want to change
    public int health = 2; // Current index in the array

    public Sprite[] newSprites; // Array of new sprites

    void OnEnable()
    {
        EventManager.onPlayerHit.AddListener(ReduceHealth); // Subscribe to the onPlayerHit event
    }
    
    void OnDisable()
    {
        EventManager.onPlayerHit.RemoveListener(ReduceHealth); // Unsubscribe to the onPlayerHit event
    }

    public void ReduceHealth()
    {
        health--;
        if (health <= 0)
        {
            EventManager.OnDeath.Invoke(); // Raise the 'death' event
            Debug.Log("Called Death Event!!!");
            return;
        }
        int currentIndex = newSprites.Length - health;
        targetImage.sprite = newSprites[currentIndex];
    }
}
