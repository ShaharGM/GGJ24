using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public static UnityEvent onPlayerHit = new UnityEvent();
    public static UnityEvent onTalk = new UnityEvent();
    public static UnityEvent StartCutsceneEvent = new UnityEvent();
    public static UnityEvent OnDeath = new UnityEvent();

    [SerializeField] private GameObject loseCanvas; // Serialized loseCanvas reference

    private bool isGamePaused = false;

    void Awake()
    {
        ListenToGameEvents();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void ListenToGameEvents()
    {
        OnDeath.AddListener(ShowLoseCanvas);
    }

    public void KeyboardDebugEvent()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            onPlayerHit.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            onTalk.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCutsceneEvent.Invoke();
        }
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        isGamePaused = pause;
    }

    public void ShowLoseCanvas()
    {
        Debug.Log("Death detected!");
        if (!isGamePaused)
        {
            PauseGame(true);
        }

        // Call the ShowLoseCanvas function of the loseCanvas script (replace 'loseCanvas' with your actual reference)
        loseCanvas.GetComponent<YouLose>().ShowLoseCanvas();
    }
}
