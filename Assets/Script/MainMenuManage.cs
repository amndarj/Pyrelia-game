
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void MulaiGame()
    {
        SceneManager.LoadScene("Opening_Day1");
    }

    public void KeluarGame()
    {
        Debug.Log("Game ditutup.");
        Application.Quit();
    }
}
