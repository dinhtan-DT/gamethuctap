using UnityEngine;
using UnityEngine.SceneManagement; // <-- Đã có, rất quan trọng

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;

    private Rigidbody2D rb;

    // MỚI: Đặt tên tag cho điểm spawn của bạn
    // Chúng ta sẽ đặt tag này cho "PlayerSpawnPoint" trong scene "Question"
    [SerializeField] private string spawnPointTag = "Respawn";

    private void Awake()
    {
        // Giữ lại Player khi đổi scene
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();

        // MỚI: Đăng ký lắng nghe sự kiện khi scene được tải
        // Chỉ instance duy nhất (persistent) mới lắng nghe
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // MỚI: Hủy đăng ký khi Player bị hủy (quan trọng)
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        // Dùng rb.velocity (chuẩn cho 2D) thay vì linearVelocity (dù vẫn chạy)
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    // MỚI: Đây là hàm "đón" player
    // Hàm này sẽ tự động chạy MỖI KHI một scene mới được tải xong
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene mới đã tải: " + scene.name);

        // Tìm GameObject có tag "Respawn" trong scene vừa tải
        GameObject spawnPoint = GameObject.FindGameObjectWithTag(spawnPointTag);

        if (spawnPoint != null)
        {
            // TÌM THẤY! Di chuyển player đến đó
            Debug.Log("Đã tìm thấy SpawnPoint. Di chuyển Player...");
            
            // Dừng mọi vận tốc cũ
            rb.linearVelocity = Vector2.zero; 
            
            // Dịch chuyển player đến vị trí spawn
            transform.position = spawnPoint.transform.position;
        }
        else
        {
            // Không tìm thấy (ví dụ: scene Main Menu không cần spawn point)
            Debug.LogWarning("Không tìm thấy GameObject với tag '" + spawnPointTag + "' trong scene " + scene.name);
        }
    }
}