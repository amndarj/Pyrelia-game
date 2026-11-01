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

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Membaca input setiap frame
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

        // Simpan arah terakhir
        if (input != Vector2.zero)
            lastInput = input;

        // Mulai gerakan jika tidak sedang bergerak
        if (!isMoving && input != Vector2.zero)
        {
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);

            Vector3 targetPos = transform.position;
            targetPos.x += input.x;
            targetPos.y += input.y;

            StartCoroutine(Move(targetPos));
        }

        // Perbarui animasi
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

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}

