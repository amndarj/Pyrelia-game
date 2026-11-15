
using UnityEngine;
using TMPro;

public class MissionObjectiveDay3 : MonoBehaviour
{
    [Header("Objective UI")]
    public TMP_Text objectiveText;

    [Header("Objective Steps")]
    [TextArea(2, 4)]
    public string[] objectives;

    private int currentObjectiveIndex = -1;

    private void Start()
    {
        ShowObjective(0);
    }

    public void ShowObjective(int index)
    {
        if (objectives == null ||
            index < 0 ||
            index >= objectives.Length)
        {
            Debug.LogWarning("Objective tidak ditemukan!");
            return;
        }

        currentObjectiveIndex = index;

        if (objectiveText != null)
        {
            objectiveText.text = objectives[currentObjectiveIndex];
        }
    }

    public void NextObjective()
    {
        int nextIndex = currentObjectiveIndex + 1;

        if (nextIndex >= objectives.Length)
        {
            Debug.Log("Semua objective selesai!");
            return;
        }

        ShowObjective(nextIndex);
    }

    public void CompleteObjective()
    {
        if (objectiveText != null)
        {
            objectiveText.text = "Semua misi selesai!";
        }

        currentObjectiveIndex = objectives.Length;
    }
}
