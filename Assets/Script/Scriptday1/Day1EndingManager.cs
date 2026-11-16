using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1EndingManager : MonoBehaviour
{
    public void UlangiDay1()
    {
        // Hapus seluruh progres Day 1.
        PlayerPrefs.DeleteKey("MisiPemadamanSelesai");
        PlayerPrefs.DeleteKey("MisiEvakuasiGokilSelesai");
        PlayerPrefs.DeleteKey("MisiPertolonganSelesai");

        PlayerPrefs.Save();

        // Muat ulang Day 1.
        SceneManager.LoadScene("Day 1");

        Debug.Log("Day 1 dimulai kembali!");
    }

    public void LanjutDay2()
    {
        SceneManager.LoadScene("Opening_Day2");

        Debug.Log("Melanjutkan ke Opening Day 2!");
    }
}

