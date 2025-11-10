using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            SceneManager.LoadScene("Question");
        }
        else if (collision.CompareTag("Answer"))
        {
            SceneManager.LoadScene("GameOver"); // ← đổi tên cho trùng với tên Scene bạn đã tạo
        }
    }
}
