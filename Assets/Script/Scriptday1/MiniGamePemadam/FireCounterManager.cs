using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FireCounterManager : MonoBehaviour
{
    public TMP_Text fireCounterText;
    public GameObject missionCompletePanel;
    public CanvasGroup missionCanvasGroup;
    public FireTimer fireTimer;

    public FirePoint[] firePoints;

    public float fadeDuration = 2f;

    private const string COUNT_KEY = "Day1_FireCount";
    private const string STARTED_KEY = "Day1_FireMiniGameStarted";

    private int jumlahApiPadam = 0;
    private int totalApi = 4;
    private bool missionCompleted = false;

    [Header("Testing")]
    public bool resetOnStartForTesting = false;
    
    private void Start()
    {
        if (fireTimer == null)
        {
            fireTimer = FindFirstObjectByType<FireTimer>();
        }
        
        if (resetOnStartForTesting)
        {
            ResetFireMission();
        }

        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(false);

        missionCompleted =
        PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;
        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(false);

        missionCompleted =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        if (missionCompleted)
        {
            jumlahApiPadam = totalApi;
            UpdateCounter();
            return;
        }

        jumlahApiPadam = PlayerPrefs.GetInt(COUNT_KEY, 0);
        jumlahApiPadam = Mathf.Clamp(jumlahApiPadam, 0, totalApi);

        PlayerPrefs.SetInt(STARTED_KEY, 1);
        PlayerPrefs.Save();

        UpdateCounter();
    }

    public void AddExtinguishedFire()
    {
        Debug.Log("MANAGER AKTIF: " + gameObject.name);
        Debug.Log("FIRE TIMER: " +
            (fireTimer == null ? "NULL" : fireTimer.name));

        if (missionCompleted)
            return;

        jumlahApiPadam++;

        jumlahApiPadam = Mathf.Clamp(
            jumlahApiPadam, 0, totalApi
        );

        PlayerPrefs.SetInt(STARTED_KEY, 1);
        PlayerPrefs.Save();

        UpdateCounter();

        // Mini-game sudah benar-benar dibuka.
        // Timer mulai berjalan dari sini.
        if (fireTimer != null)
        {
            Debug.Log("FireTimer ditemukan, mencoba menjalankan timer.");
            fireTimer.MulaiTimer();
        }
        else
        {
            Debug.LogError("FireTimer masih NULL!");
        }

        if (jumlahApiPadam >= totalApi)
        {
            missionCompleted = true;

            if (fireTimer != null)
                fireTimer.HentikanTimer();

            StartCoroutine(ShowMissionComplete());
        }
    }

    private IEnumerator ShowMissionComplete()
    {
        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(true);

        if (missionCanvasGroup != null)
        {
            missionCanvasGroup.alpha = 0f;

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;

                missionCanvasGroup.alpha =
                    Mathf.Clamp01(elapsedTime / fadeDuration);

                yield return null;
            }

            missionCanvasGroup.alpha = 1f;
        }

        PlayerPrefs.SetInt("MisiPemadamanSelesai", 1);
        PlayerPrefs.SetInt(COUNT_KEY, totalApi);
        PlayerPrefs.SetInt(STARTED_KEY, 0);
        PlayerPrefs.Save();

        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.SaveScene("Day 1");

        Debug.Log("Misi pemadaman berhasil!");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Day 1");
    }

    private void UpdateCounter()
    {
        if (fireCounterText != null)
        {
            fireCounterText.text =
                "Api Dipadamkan: " + jumlahApiPadam + "/" + totalApi;
        }
    }

    public void ResetFireMission()
    {
        StopAllCoroutines();

        jumlahApiPadam = 0;
        missionCompleted = false;

        PlayerPrefs.DeleteKey(COUNT_KEY);
        PlayerPrefs.DeleteKey("Day1_FireTimer");
        PlayerPrefs.DeleteKey(STARTED_KEY);
        PlayerPrefs.DeleteKey("Day1_FireTimerStarted");

        PlayerPrefs.SetInt("MisiPemadamanSelesai", 0);
        PlayerPrefs.Save();

        if (firePoints != null)
        {
            foreach (FirePoint point in firePoints)
            {
                if (point != null)
                    point.ResetFirePoint();
            }
        }

        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(false);

        UpdateCounter();
    }
}
