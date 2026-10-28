using UnityEngine;

public class NPCFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float followDistance = 1.2f;

    private bool isFollowing;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void Update()
    {
        if (!isFollowing || player == null)
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
        else if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;

        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
        }
    }
}