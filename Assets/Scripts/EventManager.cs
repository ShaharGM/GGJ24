using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public static UnityEvent onPlayerHit = new UnityEvent();
    public static UnityEvent onTalk = new UnityEvent();
    public static UnityEvent StartCutsceneEvent = new UnityEvent();
    
    void Awake()
    {
        // Ensure there's only one instance of the EventManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep it across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        KeyboardDebugEvent();
    }

    public void KeyboardDebugEvent()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            onPlayerHit.Invoke(); // Trigger the event
        }
                if (Input.GetKeyDown(KeyCode.T))
        {
            onTalk.Invoke(); // Trigger the event
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCutsceneEvent.Invoke();
        }
    }
}
