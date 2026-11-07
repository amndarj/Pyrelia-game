
using UnityEngine;

public class NPCFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float followDistance = 1.2f;

    private bool isFollowing;
    private bool hasBeenDropped;
    private bool isPaused;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetIdle();
    }

    private void Update()
    {
        if (!isFollowing || isPaused || player == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance > followDistance)
        {
            Vector3 targetPosition = new Vector3(
                player.position.x,
                player.position.y,
                transform.position.z
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );

            if (animator != null)
            {
                animator.SetBool("IsMoving", true);

                float directionX =
                    player.position.x - transform.position.x;

                if (Mathf.Abs(directionX) > 0.05f)
                {
                    animator.SetFloat(
                        "moveX",
                        Mathf.Sign(directionX)
                    );
                }
            }
        }
        else
        {
            SetIdle();
        }
    }

    private void SetIdle()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void StartFollowing()
    {
        if (hasBeenDropped)
            return;

        isPaused = false;
        isFollowing = true;
    }

    public void PauseFollowing()
    {
        isPaused = true;
        SetIdle();
    }

    public void ResumeFollowing()
    {
        if (hasBeenDropped)
            return;

        isPaused = false;
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
        isPaused = false;
        hasBeenDropped = true;

        SetIdle();

        if (animator != null)
        {
            animator.Play("NPC_gokil_Idle", 0, 0f);
        }

        Debug.Log("NPC sudah sampai di Safe Zone dan kembali Idle!");
    }
}
