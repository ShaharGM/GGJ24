using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class State
{
    public string dialogue;
    public string animation;
}

public class Princess : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshPro Text object
    public Animator animator; // Reference to the Animator component

    public State[] start;
    public State[] win;
    public State[] lose;

    public float swordSizeThreshold = 1;
    public float sleepLength = 2f; // Sleep length in seconds

    private float swordVal = 0f;
    private bool isStartSceneRunning = false; // Flag to control the execution of the second Coroutine

    void OnEnable()
    {
        EventManager.onTalk.AddListener(Talk); // Subscribe to the onTalk event
    }

    void OnDisable()
    {
        EventManager.onTalk.RemoveListener(Talk); // Unsubscribe from the onTalk event
    }

    // This method is called when the "onTalk" event is invoked.
    public void Talk()
    {
        swordVal = GetSwordSize();
        StartCoroutine(RunStartScene());
    }

    public IEnumerator RunStartScene()
    {
        // Set the flag to true to indicate that the start scene is running
        isStartSceneRunning = true;

        foreach (var state in start)
        {
            SetState(state);
            yield return new WaitForSeconds(sleepLength);
        }

        // Start scene has ended, set the flag to false
        isStartSceneRunning = false;

        State[] endState = swordVal > swordSizeThreshold ? win : lose;

        // Start the second Coroutine only if the start scene has ended
        if (!isStartSceneRunning)
        {
            StartCoroutine(RunScene(endState));
        }
    }

    public void SetState(State state)
    {
        dialogueText.text = state.dialogue;
        animator.Play(state.animation);
    }

    public IEnumerator RunScene(State[] states)
    {
        for (int i = 0; i < states.Length; ++i)
        {
            SetState(states[i]);
            yield return new WaitForSeconds(sleepLength); // Sleep for the specified duration
        }
    }

    // Helper method to get the sword size from a GameObject with the "Sword" tag
    private float GetSwordSize()
    {
        GameObject sword = GameObject.FindGameObjectWithTag("Sword");
        if (sword != null)
        {
            return sword.transform.localScale.y;
        }
        else
        {
            Debug.LogWarning("Sword object not found");
            return 0f; // Return a default value if the sword is not found
        }
    }
}
