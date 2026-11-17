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

    private const string KEY_PROGRESS = "Day1_GokilProgress";
    private const string KEY_X = "Day1_GokilX";
    private const string KEY_Y = "Day1_GokilY";
    private const string KEY_FOLLOWING = "Day1_GokilFollowing";
    private const string KEY_DROPPED = "Day1_GokilDropped";

    private float saveInterval = 0.5f;
    private float saveTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        LoadProgress();
        SetIdle();
    }

    private void Update()
    {
        if (isFollowing && !isPaused && player != null)
        {
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

        // Simpan posisi secara berkala.
        if (isFollowing || hasBeenDropped)
        {
            saveTimer += Time.deltaTime;

            if (saveTimer >= saveInterval)
            {
                SaveProgress();
                saveTimer = 0f;
            }
        }
    }

    private void LoadProgress()
    {
        if (PlayerPrefs.GetInt(KEY_PROGRESS, 0) != 1)
            return;

        // Jika misi sudah selesai, Gokil tetap dianggap sudah di posko.
        bool missionCompleted =
            PlayerPrefs.GetInt("MisiEvakuasiGokilSelesai", 0) == 1;

        if (missionCompleted)
        {
            hasBeenDropped = true;
            isFollowing = false;
            return;
        }

        float x = PlayerPrefs.GetFloat(KEY_X, transform.position.x);
        float y = PlayerPrefs.GetFloat(KEY_Y, transform.position.y);

        transform.position = new Vector3(
            x,
            y,
            transform.position.z
        );

        isFollowing =
            PlayerPrefs.GetInt(KEY_FOLLOWING, 0) == 1;

        hasBeenDropped =
            PlayerPrefs.GetInt(KEY_DROPPED, 0) == 1;
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt(KEY_PROGRESS, 1);

        PlayerPrefs.SetFloat(KEY_X, transform.position.x);
        PlayerPrefs.SetFloat(KEY_Y, transform.position.y);

        PlayerPrefs.SetInt(
            KEY_FOLLOWING,
            isFollowing ? 1 : 0
        );

        PlayerPrefs.SetInt(
            KEY_DROPPED,
            hasBeenDropped ? 1 : 0
        );

        PlayerPrefs.Save();
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
        // Gokil hanya boleh mengikuti pemain setelah api padam.
        if (PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) != 1)
            return;

        if (hasBeenDropped)
            return;

        isPaused = false;
        isFollowing = true;

        SaveProgress();
    }

    public void PauseFollowing()
    {
        isPaused = true;
        SetIdle();

        SaveProgress();
    }

    public void ResumeFollowing()
    {
        if (hasBeenDropped)
            return;

        isPaused = false;
        isFollowing = true;

        SaveProgress();
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

        SaveProgress();

        Debug.Log("NPC sudah sampai di Safe Zone!");
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
            SaveProgress();
    }

    private void OnApplicationQuit()
    {
        SaveProgress();
    }
}
