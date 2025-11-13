using UnityEngine;

public class SpawnPlayerOnLoad : MonoBehaviour
{
    // Kéo PlayerSpawnPoint từ Hierarchy vào đây
    public Transform spawnPoint;

    // Tên tag của nhân vật, phải khớp với Bước 1
    private string playerTag = "Player"; 

    void Start()
    {
        // Tìm GameObject của nhân vật bằng tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null && spawnPoint != null)
        {
            Debug.Log("Đã tìm thấy Player, đang di chuyển đến SpawnPoint...");

            // Nếu bạn dùng CharacterController (game 3D), hãy tắt nó đi
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            // Nếu bạn dùng Rigidbody 2D (game 2D), bạn có thể cần set vận tốc về 0
            Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
            if (rb2d != null) rb2d.linearVelocity = Vector2.zero;

            // DỊCH CHUYỂN nhân vật đến vị trí của spawnPoint
            player.transform.position = spawnPoint.position;

            // Bật lại (nếu đã tắt)
            if (cc != null) cc.enabled = true;
        }
        else
        {
            if (player == null)
                Debug.LogError("SpawnPlayerOnLoad: Không tìm thấy nhân vật với tag '" + playerTag + "'!");
            if (spawnPoint == null)
                Debug.LogError("SpawnPlayerOnLoad: Bạn chưa gán SpawnPoint trong Inspector!");
        }
    }
}