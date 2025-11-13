using UnityEngine;
using UnityEngine.UI; // Cần cho UI
using TMPro; // Cần cho TextMeshPro
using System.Collections.Generic; // Cần cho List

public class QuizManager : MonoBehaviour
{
    // Singleton (để các script khác dễ dàng gọi)
    public static QuizManager Instance;

    [Header("UI Components")]
    public GameObject quizPanel; // Kéo QuizPanel vào đây
    public TextMeshProUGUI questionText; // Kéo QuestionText vào đây
    public Button[] answerButtons = new Button[4]; // Kéo 4 nút vào đây

    [Header("Quiz State")]
    private List<QuestionData> currentQuizQuestions; // Danh sách câu hỏi của bộ quiz hiện tại
    private int currentQuestionIndex;
    
    // (Phần tính điểm sẽ thêm sau)
    // public int score; 

    void Awake()
    {
        // Thiết lập Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ẩn panel khi game bắt đầu
        quizPanel.SetActive(false); 
    }

    // Hàm này được gọi bởi các khối "Answer"
    public void StartQuiz(List<QuestionData> questions)
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("Không có câu hỏi nào để bắt đầu!");
            return;
        }

        currentQuizQuestions = questions;
        currentQuestionIndex = 0;
        // score = 0; // (Reset điểm sau này)

        // Bật panel lên
        quizPanel.SetActive(true);
        // Tạm dừng game (nếu muốn)
        Time.timeScale = 0f; 

        ShowQuestion(currentQuestionIndex);
    }

    void ShowQuestion(int index)
    {
        QuestionData question = currentQuizQuestions[index];

        // Hiển thị câu hỏi
        questionText.text = question.questionText;

        // Hiển thị 4 câu trả lời lên 4 nút
        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Lấy component Text của nút
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            
            if (i < question.answers.Length)
            {
                buttonText.text = question.answers[i];
                answerButtons[i].gameObject.SetActive(true);

                // Quan trọng: Xóa các listener cũ và thêm listener mới
                int buttonIndex = i; // Cần tạo biến tạm
                answerButtons[i].onClick.RemoveAllListeners(); 
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(buttonIndex));
            }
            else
            {
                // Ẩn nút nếu không có đủ 4 câu trả lời (hiếm)
                answerButtons[i].gameObject.SetActive(false); 
            }
        }
    }

    void OnAnswerSelected(int selectedIndex)
    {
        // Kiểm tra câu trả lời (sau này thêm tính điểm ở đây)
        bool isCorrect = (selectedIndex == currentQuizQuestions[currentQuestionIndex].correctAnswerIndex);
        
        if (isCorrect)
        {
            Debug.Log("Đúng rồi!");
            // score++;
        }
        else
        {
            Debug.Log("Sai rồi!");
        }

        // Chuyển sang câu tiếp theo
        currentQuestionIndex++;

        if (currentQuestionIndex < currentQuizQuestions.Count)
        {
            // Nếu còn câu hỏi, hiển thị câu tiếp theo
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            // Nếu hết câu hỏi, kết thúc quiz
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        // Ẩn panel đi
        quizPanel.SetActive(false);
        // Cho game chạy lại
        Time.timeScale = 1f; 

        Debug.Log("Kết thúc quiz!");
        // (Hiển thị điểm tổng ở đây)
        // Debug.Log("Điểm của bạn: " + score);
    }
}