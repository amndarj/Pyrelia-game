using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizDay3 : MonoBehaviour
{
    [Header("Question")]
    public TMP_Text questionText;

    [Header("Options")]
    public Button optionA;
    public TMP_Text textOptionA;

    public Button optionB;
    public TMP_Text textOptionB;

    public Button optionC;
    public TMP_Text textOptionC;

    [Header("NPC and MC")]
    public NPCbocil npcBocil;
    public PlayerMC playerMC;

    private int currentQuestion;
    private bool answered;

    private const string QUESTION_KEY = "Day3_QuizQuestion";
    private const string STAGE_KEY = "Day3_Stage";
    private const string ACTIVE_KEY = "Day3_QuizActive";
    private const string COMPLETED_KEY = "Day3_QuizCompleted";
    private const string RESPONSE_KEY = "Day3_QuizResponseText";

    private string[] questions =
    {
        "Manakah kegiatan manusia yang dapat menyebabkan kebakaran hutan?",
        "Mengapa bermain petasan di sekitar hutan sangat berbahaya?",
        "Apa yang sebaiknya dilakukan untuk mencegah kebakaran hutan?",
        "Jika melihat kebakaran hutan, apa tindakan pertama yang harus dilakukan?",
        "Apa yang harus dilakukan setelah menjauh dari lokasi kebakaran?"
    };

    private string[,] options =
    {
        {
            "Menyiram tanaman di sekitar hutan",
            "Membuang puntung rokok sembarangan dan membakar sampah di dekat hutan",
            "Membersihkan sampah tanpa membakarnya"
        },
        {
            "Percikan api petasan dapat mengenai tumbuhan kering dan memicu kebakaran",
            "Petasan dapat membuat pohon tumbuh lebih cepat",
            "Suara petasan dapat membuat hutan menjadi panas"
        },
        {
            "Membakar sampah di dekat pepohonan",
            "Membuat api unggun tanpa pengawasan",
            "Tidak menyalakan api sembarangan di area hutan"
        },
        {
            "Mendekati api untuk melihat seberapa besar kebakarannya",
            "Segera menjauh menuju tempat yang aman",
            "Mencoba memadamkan api sendiri"
        },
        {
            "Kembali mendekati api untuk memastikan kondisinya",
            "Diam saja dan menunggu api padam",
            "Melapor kepada orang dewasa atau petugas mengenai lokasi kebakaran"
        }
    };

    private int[] correctAnswers = { 1, 0, 2, 1, 2 };

    private string[,] explanations =
    {
        {
            "Benar! Puntung rokok yang masih menyala dan aktivitas membakar sampah dapat memicu kebakaran, terutama ketika mengenai tumbuhan kering.",
            "Belum tepat. Membuang puntung rokok sembarangan dan membakar sampah di dekat hutan dapat memicu kebakaran."
        },
        {
            "Benar! Percikan api dari petasan dapat mengenai rumput atau daun kering sehingga memicu kebakaran yang dapat menyebar dengan cepat.",
            "Belum tepat. Petasan menghasilkan percikan api yang dapat menyambar tumbuhan kering dan menyebabkan kebakaran hutan."
        },
        {
            "Benar! Menghindari penggunaan api sembarangan di kawasan hutan merupakan salah satu cara penting untuk mencegah kebakaran.",
            "Belum tepat. Membakar sampah dan membuat api unggun tanpa pengawasan dapat meningkatkan risiko kebakaran hutan."
        },
        {
            "Benar! Ketika melihat kebakaran, segera menjauh dari lokasi menuju tempat yang aman. Jangan mengambil risiko mendekati api.",
            "Belum tepat. Jangan mendekati atau mencoba memadamkan kebakaran sendiri. Segera menjauh menuju tempat yang aman."
        },
        {
            "Benar! Setelah berada di tempat aman, segera beri tahu orang dewasa atau petugas pemadam kebakaran dan sampaikan lokasi kebakaran dengan jelas.",
            "Belum tepat. Jangan kembali mendekati lokasi kebakaran. Utamakan keselamatan dan segera laporkan kejadian kepada orang dewasa atau petugas."
        }
    };

    void Awake()
    {
        optionA.onClick.AddListener(() => CheckAnswer(0));
        optionB.onClick.AddListener(() => CheckAnswer(1));
        optionC.onClick.AddListener(() => CheckAnswer(2));
    }

    public void StartQuiz()
    {
        currentQuestion = 0;
        answered = false;

        PlayerPrefs.SetInt(QUESTION_KEY, 0);
        PlayerPrefs.SetInt(STAGE_KEY, PlayerMC.STAGE_QUIZ);
        PlayerPrefs.SetInt(ACTIVE_KEY, 1);
        PlayerPrefs.SetInt(COMPLETED_KEY, 0);
        PlayerPrefs.DeleteKey(RESPONSE_KEY);
        PlayerPrefs.Save();

        gameObject.SetActive(true);
        ShowQuestion();
    }

    // Memulihkan kuis berdasarkan progres terakhir.
    public void ResumeQuiz()
    {
        currentQuestion = PlayerPrefs.GetInt(QUESTION_KEY, 0);

        currentQuestion = Mathf.Clamp(
            currentQuestion,
            0,
            questions.Length - 1
        );

        int savedStage = PlayerPrefs.GetInt(
            STAGE_KEY,
            PlayerMC.STAGE_QUIZ
        );

        gameObject.SetActive(true);

        // Jika sebelumnya sedang membaca penjelasan jawaban.
        if (savedStage == PlayerMC.STAGE_QUIZ_RESPONSE)
        {
            answered = true;
            gameObject.SetActive(false);

            string savedResponse = PlayerPrefs.GetString(
                RESPONSE_KEY,
                ""
            );

            if (!string.IsNullOrEmpty(savedResponse) &&
                playerMC != null)
            {
                playerMC.ShowQuizResponse(
                    savedResponse,
                    NextQuestion
                );
            }
            else
            {
                Debug.LogError(
                    "Penjelasan jawaban terakhir tidak ditemukan!"
                );
            }

            return;
        }

        // Jika sebelumnya sedang mengerjakan soal.
        answered = false;
        ShowQuestion();

        Debug.Log(
            "Kuis dilanjutkan dari soal nomor " +
            (currentQuestion + 1)
        );
    }

    void ShowQuestion()
    {
        answered = false;

        questionText.text = questions[currentQuestion];

        textOptionA.text = "A. " + options[currentQuestion, 0];
        textOptionB.text = "B. " + options[currentQuestion, 1];
        textOptionC.text = "C. " + options[currentQuestion, 2];

        SetOptionsInteractable(true);
    }

    void CheckAnswer(int answer)
    {
        if (answered)
            return;

        answered = true;
        SetOptionsInteractable(false);

        bool isCorrect = answer == correctAnswers[currentQuestion];

        string response = explanations[
            currentQuestion,
            isCorrect ? 0 : 1
        ];

        // Simpan soal dan penjelasan sebelum menampilkan dialog MC.
        PlayerPrefs.SetInt(QUESTION_KEY, currentQuestion);
        PlayerPrefs.SetInt(STAGE_KEY, PlayerMC.STAGE_QUIZ_RESPONSE);
        PlayerPrefs.SetInt(ACTIVE_KEY, 1);
        PlayerPrefs.SetString(RESPONSE_KEY, response);
        PlayerPrefs.Save();

        gameObject.SetActive(false);

        if (playerMC != null)
        {
            playerMC.ShowQuizResponse(response, NextQuestion);
        }
        else
        {
            Debug.LogError("PlayerMC belum dihubungkan!");
        }
    }

    void NextQuestion()
    {
        currentQuestion++;

        PlayerPrefs.SetInt(QUESTION_KEY, currentQuestion);
        PlayerPrefs.DeleteKey(RESPONSE_KEY);

        if (currentQuestion < questions.Length)
        {
            PlayerPrefs.SetInt(STAGE_KEY, PlayerMC.STAGE_QUIZ);
            PlayerPrefs.SetInt(ACTIVE_KEY, 1);
            PlayerPrefs.Save();

            gameObject.SetActive(true);
            ShowQuestion();
        }
        else
        {
            PlayerPrefs.SetInt(COMPLETED_KEY, 1);
            PlayerPrefs.SetInt(ACTIVE_KEY, 0);
            PlayerPrefs.Save();

            if (npcBocil != null)
            {
                npcBocil.FinishEducation();
            }
            else
            {
                Debug.LogError("NPCbocil belum dihubungkan!");
            }
        }
    }

    void SetOptionsInteractable(bool interactable)
    {
        optionA.interactable = interactable;
        optionB.interactable = interactable;
        optionC.interactable = interactable;
    }
}
