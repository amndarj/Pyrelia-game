using UnityEngine;

public class OpeningAnimationEvent : MonoBehaviour
{
    public OpeningDay3Controller openingManager;

    public void FinishOpening()
    {
        openingManager.LoadDay3();
    }
}