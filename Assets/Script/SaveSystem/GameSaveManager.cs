using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance;

    private const string SAVE_EXISTS = "Game_SaveExists";
    private const string LAST_SCENE = "Game_LastScene";
    private const string PROGRESS_REGISTRY = "Game_ProgressRegistry";

    private const string DAY1_SCENE = "Day 1";
    private const string FIRE_MINIGAME = "MiniGame_PemadamanApi";

    [System.Serializable]
    private class KeyList
    {
        public List<string> keys = new List<string>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // =================================
    // CHECKPOINT
    // =================================

    public bool HasSavedGame()
    {
        return PlayerPrefs.GetInt(SAVE_EXISTS, 0) == 1;
    }

    public void SaveScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
            return;

        PlayerPrefs.SetString(LAST_SCENE, sceneName);
        PlayerPrefs.SetInt(SAVE_EXISTS, 1);
        PlayerPrefs.Save();

        Debug.Log("Checkpoint tersimpan: " + sceneName);
    }

    public void SaveCurrentScene()
    {
        string currentScene =
            SceneManager.GetActiveScene().name;

        // Jangan menimpa checkpoint ketika berada di menu.
        if (currentScene == "Main Menu" ||
            currentScene == "Setting Panel" ||
            currentScene == "Credit Panel")
        {
            return;
        }

        // Minigame pemadaman yang belum selesai
        // harus kembali ke Day 1.
        if (currentScene == FIRE_MINIGAME)
        {
            SaveScene(DAY1_SCENE);
            return;
        }

        SaveScene(currentScene);
    }

    public void ContinueGame()
    {
        if (!HasSavedGame())
        {
            Debug.LogWarning("Belum ada permainan tersimpan!");
            return;
        }

        string lastScene =
            PlayerPrefs.GetString(LAST_SCENE, "");

        if (string.IsNullOrEmpty(lastScene))
        {
            Debug.LogError("Checkpoint scene tidak ditemukan!");
            return;
        }

        Debug.Log("Melanjutkan permainan dari: " + lastScene);

        SceneManager.LoadScene(lastScene);
    }

    // =================================
    // REGISTRASI KEY PROGRES
    // =================================

    public void RegisterProgressKey(string key)
    {
        if (string.IsNullOrEmpty(key))
            return;

        KeyList data = LoadRegisteredKeys();

        if (!data.keys.Contains(key))
        {
            data.keys.Add(key);

            PlayerPrefs.SetString(
                PROGRESS_REGISTRY,
                JsonUtility.ToJson(data)
            );

            PlayerPrefs.Save();
        }
    }

    private KeyList LoadRegisteredKeys()
    {
        string json = PlayerPrefs.GetString(
            PROGRESS_REGISTRY,
            ""
        );

        if (string.IsNullOrEmpty(json))
            return new KeyList();

        KeyList data = JsonUtility.FromJson<KeyList>(json);

        return data ?? new KeyList();
    }

    // =================================
    // RESET NEW GAME
    // =================================

    public void NewGame()
    {
        ResetAllProgress();

        SaveScene("Opening_Day1");

        SceneManager.LoadScene("Opening_Day1");

        Debug.Log("NEW GAME DIMULAI!");
    }

    private void ResetAllProgress()
    {
        // Hapus semua key yang sudah terdaftar.
        KeyList data = LoadRegisteredKeys();

        foreach (string key in data.keys)
        {
            PlayerPrefs.DeleteKey(key);
        }


    string[] day1Keys =
    {
        // Progres utama Day 1
        "Day1_SudahKeHutan",
        "Day1_TeleponDamkarSelesai",
        "Day1_KeputusanDamkar",
        "MisiPemadamanSelesai",
        "MisiEvakuasiGokilSelesai",
        "MisiPertolonganSelesai",

        // Progres mini-game pemadaman
        "Day1_FireCount",
        "Day1_FireMiniGameStarted",
        "Day1_FireTimer",
        "Day1_FireTimerStarted",
        "Day1_Fire_1_Extinguished",
        "Day1_Fire_2_Extinguished",
        "Day1_Fire_3_Extinguished",
        "Day1_Fire_4_Extinguished",

        // Progres NPC Gokil
        "Day1_GokilProgress",
        "Day1_GokilX",
        "Day1_GokilY",
        "Day1_GokilFollowing",
        "Day1_GokilDropped",
        "Day1_GokilDialogPoint",
        "Day1_GokilDialogLine",
        "Day1_GokilDialogActive",

        // Posisi pemain
        "Day1_PlayerX",
        "Day1_PlayerY",
        "Day1_PlayerPositionSaved"
    };


        foreach (string key in day1Keys)
        {
            PlayerPrefs.DeleteKey(key);
        }

        // Hapus data tetap Day 2.
        string[] day2Keys =
        {
            "Day2_MisiAktif",
            "Day2_Langkah",
            "Day2_PoskoMasker_Posko",
            "Day2_MaskerDimiliki",
            "Day2_MaskerDibagikan",
            "Day2_HewanDirawat",
            "Day2_SampahDiambil",
            "Day2_PemilahanSelesai",
            "Day2_PohonDitanam",
            "Day2_Selesai"
        };

        foreach (string key in day2Keys)
        {
            PlayerPrefs.DeleteKey(key);
        }
        
        string[] day3Keys =
        {
            "Day3_Stage",
            "Day3_DialogueLine",
            "Day3_QuizQuestion",
            "Day3_QuizActive",
            "Day3_QuizCompleted",
            "Day3_QuizResponseText",
            "Day3_Completed"
        };

        foreach (string key in day3Keys)
        {
            PlayerPrefs.DeleteKey(key);
        }

        // Hapus daftar key lama.
        PlayerPrefs.DeleteKey(PROGRESS_REGISTRY);

        PlayerPrefs.Save();

        Debug.Log("Progres permainan berhasil direset!");
    }

    // =================================
    // SAVE SAAT KELUAR
    // =================================

    public void SaveOnExit()
    {
        SaveCurrentScene();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            SaveCurrentScene();
    }

    private void OnApplicationQuit()
    {
        SaveCurrentScene();
    }
}
