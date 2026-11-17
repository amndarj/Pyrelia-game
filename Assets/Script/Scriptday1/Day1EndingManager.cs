using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1EndingManager : MonoBehaviour
{
    public void UlangiDay1()
    {
        // Hapus seluruh progres Day 1.
        string[] day1Keys =
        {
            "Day1_TeleponDamkarSelesai",
            "Day1_KeputusanDamkar",

            "Day1_FireCount",
            "Day1_FireMiniGameStarted",
            "Day1_FireTimer",
            "Day1_FireTimerStarted",
            "Day1_Fire_1_Extinguished",
            "Day1_Fire_2_Extinguished",
            "Day1_Fire_3_Extinguished",
            "Day1_Fire_4_Extinguished",

            "MisiPemadamanSelesai",
            "MisiEvakuasiGokilSelesai",
            "MisiPertolonganSelesai",

            // Posisi Player.
            "Day1_PlayerX",
            "Day1_PlayerY",
            "Day1_PlayerPositionSaved",

            // Progres Gokil.
            "Day1_GokilProgress",
            "Day1_GokilX",
            "Day1_GokilY",
            "Day1_GokilFollowing",
            "Day1_GokilDropped",

            // Dialog perjalanan Gokil.
            "Day1_GokilDialogPoint",
            "Day1_GokilDialogLine",
            "Day1_GokilDialogActive"
        };

        foreach (string key in day1Keys)
        {
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.Save();

        // Simpan checkpoint baru.
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveScene("Day 1");
        }

        SceneManager.LoadScene("Day 1");

        Debug.Log("Day 1 dimulai kembali!");
    }

    public void LanjutDay2()
    {
        // Simpan checkpoint Opening Day 2.
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveScene("Opening_Day2");
        }

        SceneManager.LoadScene("Opening_Day2");

        Debug.Log("Melanjutkan ke Opening Day 2!");
    }
}
