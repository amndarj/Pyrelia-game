using UnityEngine;

public class OpeningAnimationEventDay3 : MonoBehaviour
{
    public OpeningDay3Controller openingManager;

    public void FinishOpening()
    {
        openingManager.LoadDay3();
    }
}