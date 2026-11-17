
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobilePauseManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BukaPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LanjutkanGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void KembaliKeMainMenu()
    {
        Time.timeScale = 1f;

        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.SaveCurrentScene();

        SceneManager.LoadScene("Main Menu");
    }

    public void KeluarGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
