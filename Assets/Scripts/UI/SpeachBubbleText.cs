using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeachBubbleText : MonoBehaviour
{
    public Transform character; // The character that the image will follow
    public Vector3 offset; // Offset from the character's position

    void Update()
    {
        if (character != null)
        {
            // Update the position of the image to be above the character
            transform.position = character.position + offset;
        }
    }
}
