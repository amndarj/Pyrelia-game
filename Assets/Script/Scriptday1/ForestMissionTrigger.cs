using UnityEngine;

public class ForestMissionTrigger : MonoBehaviour
{
    private const string FOREST_KEY = "Day1_SudahKeHutan";

    public MissionObjective missionObjective;

    private bool missionStarted = false;

    private void Awake()
    {
        missionStarted =
            PlayerPrefs.GetInt(FOREST_KEY, 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        bool teleponSelesai =
            PlayerPrefs.GetInt("Day1_TeleponDamkarSelesai", 0) == 1;

        bool pemadamanSelesai =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        if (teleponSelesai || pemadamanSelesai)
            return;

        if (missionStarted)
            return;

        missionStarted = true;

        PlayerPrefs.SetInt(FOREST_KEY, 1);
        PlayerPrefs.Save();

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Pergi ke rumah dan hubungi petugas pemadam kebakaran."
            );
        }

        Debug.Log("Pemain sudah memasuki hutan!");
    }
}