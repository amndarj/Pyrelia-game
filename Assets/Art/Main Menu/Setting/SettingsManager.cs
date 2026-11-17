using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public void KembaliKeMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}