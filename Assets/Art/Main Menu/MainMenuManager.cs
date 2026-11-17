
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void MulaiGame()
    {
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.NewGame();
        }
        else
        {
            Debug.LogError("GameSaveManager tidak ditemukan!");
        }
    }

    public void LanjutkanGame()
    {
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.ContinueGame();
        }
        else
        {
            Debug.LogError("GameSaveManager tidak ditemukan!");
        }
    }

    public void BukaPengaturan()
    {
        SceneManager.LoadScene("Setting Panel");
    }

    public void Kredit()
    {
        SceneManager.LoadScene("Credit Panel");
    }

    public void KeluarGame()
    {
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveOnExit();
        }

        Debug.Log("Game ditutup.");
        Application.Quit();
    }
}
