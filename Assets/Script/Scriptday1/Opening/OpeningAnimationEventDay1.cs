using UnityEngine;

public class OpeningAnimationEventDay1 : MonoBehaviour
{
    public OpeningDay1Controller openingManager;

    public void FinishOpening()
    {
        openingManager.LoadDay1();
    }
}