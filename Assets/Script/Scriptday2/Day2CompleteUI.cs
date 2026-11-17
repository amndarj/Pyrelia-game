using UnityEngine;
using UnityEngine.SceneManagement;

public class Day2CompleteUI : MonoBehaviour
{
    public void MainLagi()
    {
        if (Day2MissionManager.instance != null)
        {
            Day2MissionManager.instance.ResetProgres();
        }
        else
        {
            Debug.LogError("Day2MissionManager tidak ditemukan!");
            return;
        }

        PlayerPrefs.Save();

        // Muat ulang Day 2
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void LanjutDay3()
    {
        SceneManager.LoadScene("Opening_Day3");

        Debug.Log("Melanjutkan ke Opening Day 3!");
    }
}
