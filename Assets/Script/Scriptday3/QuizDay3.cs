
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

    private string[] questions =
    {
        "Kamu ingin bermain petasan. Di mana tempat yang aman?",
        "Jika melihat api mulai membakar rumput di hutan, apa yang harus dilakukan?",
        "Apa yang sebaiknya dilakukan untuk mencegah kebakaran hutan?"
    };

    private string[,] options =
    {
        {
            "Di dekat hutan yang banyak daun kering",
            "Di tempat terbuka, jauh dari bahan mudah terbakar, dengan pengawasan orang dewasa",
            "Di dekat tumpukan kayu"
        },
        {
            "Mendekati api untuk memadamkannya sendiri",
            "Menjauh dan segera melapor kepada orang dewasa atau petugas",
            "Membiarkannya karena api masih kecil"
        },
        {
            "Membakar sampah di dekat pepohonan",
            "Membuang puntung rokok sembarangan",
            "Tidak menyalakan api sembarangan di area hutan"
        }
    };

    private int[] correctAnswers = { 1, 1, 2 };

    // Penjelasan khusus untuk setiap pertanyaan
    private string[,] explanations =
    {
        {
            "Benar! Petasan harus dijauhkan dari hutan dan bahan mudah terbakar. Penggunaan petasan juga harus mengikuti aturan keselamatan dan pengawasan orang dewasa.",
            "Belum tepat. Daun kering dan tumpukan kayu mudah terbakar. Jangan bermain petasan di dekat hutan atau bahan yang mudah terbakar."
        },
        {
            "Benar! Menjauh dari api dan segera melapor kepada orang dewasa atau petugas adalah tindakan yang tepat. Jangan mengambil risiko memadamkan api sendiri.",
            "Belum tepat. Api sekecil apa pun dapat menyebar dengan cepat. Segera menjauh dan laporkan kepada orang dewasa atau petugas."
        },
        {
            "Benar! Tidak menyalakan api sembarangan di area hutan dapat membantu mencegah kebakaran.",
            "Belum tepat. Membakar sampah dan membuang puntung rokok sembarangan dapat memicu kebakaran. Hindari sumber api di area hutan."
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

        gameObject.SetActive(true);
        ShowQuestion();
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

        // Sembunyikan quiz sementara MC menjelaskan
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

        if (currentQuestion < questions.Length)
        {
            gameObject.SetActive(true);
            ShowQuestion();
        }
        else
        {
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
