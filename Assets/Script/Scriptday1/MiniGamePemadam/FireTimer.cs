using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class FireTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float waktuAwal = 180f;

    public UnityEvent waktuHabis;

    public GameObject missionFailedPanel;
    public CanvasGroup missionFailedCanvasGroup;
    public float fadeDuration = 5f;
    public string namaSceneDesa = "Day 1";

    private float waktuTersisa;
    private bool timerBerjalan = false;
    private bool misiSelesai = false;

    private void Start()
    {
        if (missionFailedPanel != null)
            missionFailedPanel.SetActive(false);

        waktuTersisa = waktuAwal;
        UpdateTimerText();
        timerBerjalan = true;
    }

    private void Update()
    {
        if (!timerBerjalan)
            return;

        waktuTersisa -= Time.deltaTime;

        if (waktuTersisa <= 0f)
        {
            waktuTersisa = 0f;
            timerBerjalan = false;
            UpdateTimerText();

            if (!misiSelesai)
            {
                waktuHabis?.Invoke();
                StartCoroutine(ShowMissionFailed());
            }

            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int menit = Mathf.FloorToInt(waktuTersisa / 60f);
        int detik = Mathf.FloorToInt(waktuTersisa % 60f);

        timerText.text =
            string.Format("Waktu: {0:00}:{1:00}", menit, detik);
    }

    public void HentikanTimer()
    {
        timerBerjalan = false;
        misiSelesai = true;
    }

    private IEnumerator ShowMissionFailed()
    {
        missionFailedPanel.SetActive(true);
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

        SceneManager.LoadScene(namaSceneDesa);
    }
}