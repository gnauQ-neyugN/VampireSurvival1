using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Thêm thư viện quản lý Scene

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 15 * 60; // 15 phút (đơn vị giây)
    public TextMeshProUGUI countdownText;
    public GameObject completionPanel;
    public GameObject bossPrefab;        // Prefab của con boss
    public Transform[] bossSpawnPoints;  // Mảng các điểm spawn của boss

    private bool bossSpawned = false;    // Để tránh tạo boss nhiều lần
    private bool isGameOver = false;

    private void Start()
    {
        if (completionPanel != null)
        {
            completionPanel.SetActive(false); // Ẩn panel ban đầu
        }
    }

    private void Update()
    {
        if (isGameOver) return;

        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime; // Trừ thời gian trôi qua mỗi frame
            UpdateTimerUI(countdownTime);

            // Kiểm tra thời gian còn 5 phút
            if (!bossSpawned && countdownTime <= 5 * 60)
            {
                SpawnBoss();
            }
        }
        else
        {
            countdownTime = 0; // Đảm bảo không giảm dưới 0
            GameOver();
        }
    }

    void UpdateTimerUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Lấy số phút
        int seconds = Mathf.FloorToInt(time % 60); // Lấy số giây còn lại

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Định dạng mm:ss
    }

    void SpawnBoss()
    {
        bossSpawned = true; // Đánh dấu boss đã xuất hiện

        if (bossPrefab != null && bossSpawnPoints.Length > 0)
        {
            foreach (Transform spawnPoint in bossSpawnPoints)
            {
                Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("Boss đã xuất hiện tại vị trí: " + spawnPoint.position);
            }
        }
    }

    void GameOver()
    {
        isGameOver = true; // Đánh dấu trò chơi đã kết thúc

        // Hiển thị thông báo hoàn thành
        if (completionPanel != null)
        {
            completionPanel.SetActive(true); // Bật panel thông báo
        }

        // Dừng trò chơi
        Time.timeScale = 0;

        Debug.Log("Trò chơi hoàn thành!");
    }

    // Hàm xử lý khi nhấn nút "Quay về Menu"
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1; // Khôi phục thời gian trước khi quay về menu
        SceneManager.LoadScene("Main Menu"); // Thay "MainMenu" bằng tên Scene menu chính
    }
}
