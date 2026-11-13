
using UnityEngine;

public class OpeningCinematicController : MonoBehaviour
{
    [Header("Shot 3")]
    [SerializeField] private Animator shot3Animator;

    [Header("Timing")]
    [SerializeField] private float shot3StartTime = 17f;

    private float cinematicTimer;
    private bool shot3Started;

    private void Start()
    {
        cinematicTimer = 0f;
        shot3Started = false;

        if (shot3Animator != null)
        {
            shot3Animator.enabled = false;
        }
    }

    private void Update()
    {
        cinematicTimer += Time.deltaTime;

        if (!shot3Started && cinematicTimer >= shot3StartTime)
        {
            shot3Animator.enabled = true;
            shot3Animator.Play(0, 0, 0f);

            shot3Started = true;
        }
    }
}
