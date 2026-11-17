using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void MulaiGame()
    {
        SceneManager.LoadScene("Opening_Day1");
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
        Debug.Log("Game ditutup.");
        Application.Quit();
    }
}
