
using UnityEngine;
using UnityEngine.InputSystem;

public class Playerwalkday2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public VirtualJoystickDay2 joystick;

    private Vector2 input;
    private Vector2 lastInput;

    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource footstepAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        footstepAudio = GetComponent<AudioSource>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D belum dipasang pada Player!");
            enabled = false;
        }

        if (footstepAudio == null)
        {
            Debug.LogError("AudioSource belum dipasang pada Player!");
        }
    }

    private void Update()
    {
        input = Vector2.zero;

        // Input keyboard untuk PC
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

        // Joystick untuk mobile
        if (joystick != null &&
            joystick.InputDirection.sqrMagnitude > 0.01f)
        {
            input = joystick.InputDirection;
        }

        input = Vector2.ClampMagnitude(input, 1f);

        bool isMoving = input.sqrMagnitude > 0.01f;

        if (isMoving)
            lastInput = input;

        // Animasi player
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);

            Vector2 arah = isMoving ? input : lastInput;

            animator.SetFloat("moveX", arah.x);
            animator.SetFloat("moveY", arah.y);
        }

        // Sound effect langkah kaki
        if (footstepAudio != null)
        {
            if (isMoving && !footstepAudio.isPlaying)
            {
                footstepAudio.Play();
            }
            else if (!isMoving && footstepAudio.isPlaying)
            {
                footstepAudio.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    private void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (footstepAudio != null)
            footstepAudio.Stop();
    }
}
