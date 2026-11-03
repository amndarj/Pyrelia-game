using UnityEngine;
using TMPro;

public class MissionObjective : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    public void UpdateObjective(string newObjective)
    {
        objectiveText.text = newObjective;
    }
}