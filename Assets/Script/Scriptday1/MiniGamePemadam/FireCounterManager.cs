
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

    public float fadeDuration = 5f;

    private int jumlahApiPadam = 0;
    private int totalApi = 4;
    private bool missionCompleted = false;

    private void Start()
    {
        missionCompletePanel.SetActive(false);
        UpdateCounter();
    }

    public void AddExtinguishedFire()
    {
        if (missionCompleted)
            return;

        jumlahApiPadam++;

        if (jumlahApiPadam > totalApi)
            jumlahApiPadam = totalApi;

        UpdateCounter();

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
        missionCompletePanel.SetActive(true);
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

        // Simpan status bahwa misi sudah berhasil.
        PlayerPrefs.SetInt("MisiPemadamanSelesai", 1);
        PlayerPrefs.Save();

        Debug.Log("Misi pemadaman berhasil!");

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene("Day 1");
    }

    private void UpdateCounter()
    {
        fireCounterText.text =
            "Api Dipadamkan: " + jumlahApiPadam + "/" + totalApi;
    }
}
