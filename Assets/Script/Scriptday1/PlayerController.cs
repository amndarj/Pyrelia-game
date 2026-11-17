
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;

    // Referensi joystick
    public VirtualJoystick joystick;

    private bool isMoving;
    private Vector2 input;
    private Vector2 lastInput;
    private Animator animator;
    private Rigidbody2D rb;
    private const string PLAYER_X_KEY = "Day1_PlayerX";
    private const string PLAYER_Y_KEY = "Day1_PlayerY";
    private const string PLAYER_SAVED_KEY = "Day1_PlayerPositionSaved";

    private float saveTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name == "Day 1")
        {
            if (PlayerPrefs.GetInt(PLAYER_SAVED_KEY, 0) == 1)
            {
                float savedX = PlayerPrefs.GetFloat(PLAYER_X_KEY);
                float savedY = PlayerPrefs.GetFloat(PLAYER_Y_KEY);

                rb.position = new Vector2(savedX, savedY);
                transform.position = new Vector3(savedX, savedY, transform.position.z);
            }
        }
    }

    private void Update()
    {
        input = Vector2.zero;

        // Input joystick
        if (joystick != null &&
            joystick.InputDirection.magnitude > 0.2f)
        {
            Vector2 joyInput = joystick.InputDirection;

            // Prioritaskan arah yang paling dominan
            if (Mathf.Abs(joyInput.x) > Mathf.Abs(joyInput.y))
                input.x = Mathf.Sign(joyInput.x);
            else
                input.y = Mathf.Sign(joyInput.y);
        }
        // Input keyboard untuk testing di PC
        else if (Keyboard.current != null)
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

        // Mulai bergerak jika tidak sedang bergerak
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

        // Simpan posisi setiap 1 detik ketika Player diam.
        if (!isMoving)
        {
            saveTimer += Time.deltaTime;

            if (saveTimer >= 1f)
            {
                SavePlayerPosition();
                saveTimer = 0f;
            }
        }
        else
        {
            saveTimer = 0f;
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

            // Berhenti jika tertahan collider
            if ((rb.position - previousPosition).sqrMagnitude < 0.000001f)
                blockedFrames++;
            else
                blockedFrames = 0;

            if (blockedFrames >= 3)
                break;
        }

        isMoving = false;
    }

    private void SavePlayerPosition()
    {
        if (UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name != "Day 1")
            return;

        if (rb == null || isMoving)
            return;

        PlayerPrefs.SetFloat(PLAYER_X_KEY, rb.position.x);
        PlayerPrefs.SetFloat(PLAYER_Y_KEY, rb.position.y);
        PlayerPrefs.SetInt(PLAYER_SAVED_KEY, 1);

        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            SavePlayerPosition();
    }

    private void OnApplicationQuit()
    {
        SavePlayerPosition();
    }   

}
