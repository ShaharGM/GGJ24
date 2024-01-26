using UnityEngine;
using TMPro;

[System.Serializable]
public class Princess : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro Text object
    public Animator animator; // Reference to the Animator component

    public int converstaionStep = -1;
    public string[] dialog;
    public string[] animationState;

     void OnEnable()
    {
        EventManager.onTalk.AddListener(Talk); // Subscribe to the onPlayerHit event
    }
    
    void OnDisable()
    {
        EventManager.onTalk.RemoveListener(Talk); // Unsubscribe to the onPlayerHit event
    }


    // This method is called when the "onTalk" event is invoked.
    public void Talk()
    {
       converstaionStep = (converstaionStep + 1) % dialog.Length;
       SetDialogueAndAnimation(dialog[converstaionStep], animationState[converstaionStep]);
    }

    public void SetDialogueAndAnimation(string dialogue, string animationState)
    {
        // Change the text of the TextMeshPro Text object
        dialogueText.text = dialogue;

        // Transition to a different animation state in the animator
        animator.Play(animationState);
    }
}