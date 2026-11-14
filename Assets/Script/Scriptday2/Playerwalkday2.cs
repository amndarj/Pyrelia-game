
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playerwalkday2 : MonoBehaviour
{
    public float moveSpeed = 7f;

    private bool isMoving;
    private Vector2 input;
    private Vector2 lastInput;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D belum dipasang pada Player!");
            enabled = false;
        }
    }

    private void Update()
    {
        input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
                input.x = -1;

            else if (Keyboard.current.dKey.isPressed ||
                     Keyboard.current.rightArrowKey.isPressed)
                input.x = 1;

            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
                input.y = 1;

            else if (Keyboard.current.sKey.isPressed ||
                     Keyboard.current.downArrowKey.isPressed)
                input.y = -1;
        }

        // Prioritaskan gerakan horizontal
        if (input.x != 0)
            input.y = 0;

        if (input != Vector2.zero)
            lastInput = input;

        if (!isMoving && input != Vector2.zero)
        {
            if (animator != null)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
            }

            Vector2 targetPos = rb.position;
            targetPos.x += input.x;
            targetPos.y += input.y;

            StartCoroutine(Move(targetPos));
        }

        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);

            if (!isMoving)
            {
                animator.SetFloat("moveX", lastInput.x);
                animator.SetFloat("moveY", lastInput.y);
            }
        }
    }

    IEnumerator Move(Vector2 targetPos)
    {
        isMoving = true;
        int blockedFrames = 0;

        while ((targetPos - rb.position).sqrMagnitude > 0.0001f)
        {
            Vector2 previousPosition = rb.position;

            rb.MovePosition(
                Vector2.MoveTowards(
                    rb.position,
                    targetPos,
                    moveSpeed * Time.fixedDeltaTime
                )
            );

            yield return new WaitForFixedUpdate();

            if ((rb.position - previousPosition).sqrMagnitude < 0.000001f)
                blockedFrames++;
            else
                blockedFrames = 0;

            if (blockedFrames >= 3)
                break;
        }

        isMoving = false;
    }
}
