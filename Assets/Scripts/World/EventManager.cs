using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public static UnityEvent onPlayerHit = new UnityEvent();
    public static UnityEvent onTalk = new UnityEvent();
    public static UnityEvent StartCutsceneEvent = new UnityEvent();
    public static UnityEvent OnDeath = new UnityEvent();
    public static UnityEvent SwordTooBigEvent = new UnityEvent();

    [SerializeField] private GameObject loseCanvas; // Serialized loseCanvas reference
    [SerializeField] private TextMeshProUGUI loseTextToChange; // Serialized loseCanvas reference

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
        SwordTooBigEvent.AddListener(PreapreTooBigSword);
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
        PauseGame(true);
        // Call the ShowLoseCanvas function of the loseCanvas script (replace 'loseCanvas' with your actual reference)
        loseCanvas.GetComponent<YouLose>().ShowLoseCanvas();
    }

    public void PreapreTooBigSword()
    {
         Debug.Log("Sword too bit Detected!");
         loseTextToChange.text = "Game Over!\nThat Bridge couldn't handle your mighty sword!\nPerhaps a different approach is needed...";
         ShowLoseCanvas();
    }
}
