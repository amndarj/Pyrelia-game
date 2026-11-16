using UnityEngine;

public class OpeningAnimationEventDay2 : MonoBehaviour
{
    public OpeningDay2Controller openingManager;

    public void FinishOpening()
    {
        openingManager.LoadDay2();
    }
}