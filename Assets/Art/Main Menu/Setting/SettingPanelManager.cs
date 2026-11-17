using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanelManager : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
