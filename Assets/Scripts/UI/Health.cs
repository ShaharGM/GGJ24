using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events; // Namespace for Unity Events

[System.Serializable]
public class Health : MonoBehaviour
{
    public Image targetImage; // The image component you want to change
    public static int health = 3; // Current index in the array

    public Sprite[] newSprites; // Array of new sprites
    
    public UnityEvent onDeath; // Event to be raised after the last sprite

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
        int currentIndex = newSprites.Length - health;
        if(targetImage != null && newSprites != null && newSprites.Length > 0 && currentIndex < newSprites.Length)
        {
            // Change the image to the next sprite in the array
            targetImage.sprite = newSprites[currentIndex];
            currentIndex++; // Move to the next sprite

            // Check if we've reached the end of the array
            if (currentIndex >= newSprites.Length)
            {
                onDeath.Invoke(); // Raise the 'death' event
                currentIndex = 0; // Optionally reset the index or take other action
            }
        }
        else
        {
            Debug.LogError("Target image or new sprites are not properly assigned, or array is empty.");
        }
    }
}
