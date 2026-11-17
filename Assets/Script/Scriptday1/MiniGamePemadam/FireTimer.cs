using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class FireTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float waktuAwal = 120f;

    public UnityEvent waktuHabis;

    public GameObject missionFailedPanel;
    public CanvasGroup missionFailedCanvasGroup;
    public float fadeDuration = 2f;

    private const string TIMER_KEY = "Day1_FireTimer";
    private const string STARTED_KEY = "Day1_FireTimerStarted";
    private const string COUNT_KEY = "Day1_FireCount";

    private float waktuTersisa;
    private bool timerBerjalan;
    private bool misiSelesai;
    private bool waktuHabisSudahDiproses;

    private void Start()
    {
        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(false);

        bool sudahDimulai =
            PlayerPrefs.GetInt(STARTED_KEY, 0) == 1;

        misiSelesai =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        if (misiSelesai)
        {
            waktuTersisa = 0f;
            UpdateTimerText();
            return;
        }

        if (sudahDimulai)
        {
            waktuTersisa =
                PlayerPrefs.GetFloat(TIMER_KEY, waktuAwal);

            timerBerjalan = true;
        }
        else
        {
            waktuTersisa = waktuAwal;
            timerBerjalan = false;
        }

        UpdateTimerText();
    }

    private void Update()
    {
        if (!timerBerjalan || misiSelesai)
            return;

        waktuTersisa -= Time.deltaTime;

        if (waktuTersisa <= 0f)
        {
            waktuTersisa = 0f;
            timerBerjalan = false;

            UpdateTimerText();
            SimpanWaktu();

            if (!waktuHabisSudahDiproses)
            {
                waktuHabisSudahDiproses = true;
                waktuHabis?.Invoke();
                StartCoroutine(ShowMissionFailed());
            }

            return;
        }

        UpdateTimerText();

        SimpanWaktu();
    }

    private void SimpanWaktu()
    {
        PlayerPrefs.SetFloat(TIMER_KEY, waktuTersisa);
        PlayerPrefs.Save();
    }

    private void UpdateTimerText()
    {
        if (timerText == null)
            return;

        int menit = Mathf.FloorToInt(waktuTersisa / 60f);
        int detik = Mathf.FloorToInt(waktuTersisa % 60f);

        timerText.text =
            string.Format("{0:00}:{1:00}", menit, detik);
    }

    public void MulaiTimer()
    {
        Debug.Log("MulaiTimer dipanggil!");

        if (misiSelesai || timerBerjalan)
            return;

        PlayerPrefs.SetInt(STARTED_KEY, 1);
        PlayerPrefs.Save();

        timerBerjalan = true;
    }

    public void HentikanTimer()
    {
        timerBerjalan = false;
        misiSelesai = true;
        SimpanWaktu();
    }

    public void ResetTimer()
    {
        StopAllCoroutines();

        waktuTersisa = waktuAwal;
        timerBerjalan = false;
        misiSelesai = false;
        waktuHabisSudahDiproses = false;

        PlayerPrefs.DeleteKey(TIMER_KEY);
        PlayerPrefs.DeleteKey(STARTED_KEY);
        PlayerPrefs.Save();

        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(false);

        UpdateTimerText();
    }

    private IEnumerator ShowMissionFailed()
    {
        if (missionFailedPanel == null)
            yield break;

        missionFailedPanel.SetActive(true);

        if (missionFailedCanvasGroup == null)
            yield break;

        missionFailedCanvasGroup.alpha = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            missionFailedCanvasGroup.alpha =
                Mathf.Clamp01(elapsedTime / fadeDuration);

            yield return null;
        }

        missionFailedCanvasGroup.alpha = 1f;
        Debug.Log("Misi gagal. Pemain dapat mencoba kembali.");
    }

    public void UlangiMiniGame()
    {
        if (GameObject.FindObjectOfType<FireCounterManager>()
            != null)
        {
            GameObject.FindObjectOfType<FireCounterManager>()
                .ResetFireMission();
        }

        ResetTimer();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            SimpanWaktu();
    }

    private void OnApplicationQuit()
    {
        SimpanWaktu();
    }
}
