using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class ResetMissionData : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("MisiPemadamanSelesai", 0);
        PlayerPrefs.Save();

        Debug.Log("Status misi damkar direset menjadi 0.");
    }
}