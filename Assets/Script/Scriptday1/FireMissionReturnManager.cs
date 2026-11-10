
using UnityEngine;

public class FireMissionReturnManager : MonoBehaviour
{
    [Header("NPC")]
    public GameObject gokilNPC;

    [Header("Pohon Hutan")]
    public Sprite normalTreeSprite;

    [Header("Objective")]
    public MissionObjective missionObjective;

    private void Start()
    {
        if (PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) != 1)
            return;

        // Mengubah semua pohon dengan tag PohonTerbakar
        GameObject[] allTrees = GameObject.FindGameObjectsWithTag("PohonTerbakar");

        foreach (GameObject tree in allTrees)
        {
            SpriteRenderer renderer = tree.GetComponent<SpriteRenderer>();

            if (renderer != null)
            {
                renderer.sprite = normalTreeSprite;
            }
        }

        // Memunculkan Gokil
        if (gokilNPC != null)
            gokilNPC.SetActive(true);

        // Mengubah objective
        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Temui Gokil di hutan dan antarkan ke posko pengungsian."
            );
        }

        Debug.Log("Semua pohon berhasil diubah menjadi normal.");
    }
}
