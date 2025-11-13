using UnityEngine;
using System.Collections.Generic; // Cần cho List

// ĐỊNH NGHĨA DỮ LIỆU CÂU HỎI
// Class này không phải là MonoBehaviour, nó chỉ là một bộ chứa dữ liệu
// [System.Serializable] để nó có thể hiển thị trong Inspector
[System.Serializable]
public class QuestionData
{
    [TextArea(3, 5)] // Giúp gõ câu hỏi dài dễ hơn
    public string questionText;
    
    public string[] answers = new string[4]; // Mảng 4 câu trả lời
    
    [Tooltip("Đáp án đúng (0 = A, 1 = B, 2 = C, 3 = D)")]
    public int correctAnswerIndex;
}


// SCRIPT KÍCH HOẠT
// Gắn script này vào các khối "Answer 1", "Answer 2"...
public class QuizTrigger : MonoBehaviour
{
    // Đây là nơi bạn nhập 10 câu hỏi cho KHỐI NÀY
    [Header("Bộ 10 câu hỏi")]
    public List<QuestionData> questions;

    private bool quizTriggered = false; // Đảm bảo chỉ kích hoạt 1 lần

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem có phải là Player không (đảm bảo Player có tag "Player")
        if (other.CompareTag("Player") && !quizTriggered)
        {
            quizTriggered = true; // Đánh dấu đã kích hoạt
            
            // GỌI BỘ NÃO: Yêu cầu QuizManager bắt đầu với bộ câu hỏi CỦA TÔI
            QuizManager.Instance.StartQuiz(questions);

            // Tùy chọn: Ẩn khối này đi sau khi chạm vào
            // gameObject.SetActive(false);
        }
    }
}