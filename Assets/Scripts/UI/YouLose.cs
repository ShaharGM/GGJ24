using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouLose : MonoBehaviour
{
    public GameObject loseCanvas;

    public void ShowLoseCanvas()
    {
        loseCanvas.SetActive(true);
    }

    public static void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void ExitGame()
    {
        Application.Quit();
    }
}
