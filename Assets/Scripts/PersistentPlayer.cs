using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    // Dùng để tạo singleton, đảm bảo chỉ có 1 player duy nhất
    public static PersistentPlayer Instance;

    void Awake()
    {
        // Nếu chưa có Instance (đây là player đầu tiên)
        if (Instance == null)
        {
            Instance = this; // Gán Instance là object này
            DontDestroyOnLoad(gameObject); // Yêu cầu Unity KHÔNG hủy object này khi load scene mới
        }
        else
        {
            // Nếu đã có Instance (ví dụ: bạn quay lại map 1)
            // thì hủy object này đi để tránh bị trùng lặp 2 player
            Destroy(gameObject);
        }
    }
}