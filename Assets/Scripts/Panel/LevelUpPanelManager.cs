using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelUpPanel; // Tham chiếu đến Panel
    [SerializeField] private HealthController playerHealthController;
    [SerializeField] private Bullet playerBullet;
    [SerializeField] private PlayerShoot playerShoot; // Tham chiếu đến script PlayerShoot
    [SerializeField] private TextMeshProUGUI notificationText;

    // Hiển thị panel
    public void ShowPanel()
    {
        levelUpPanel.SetActive(true); // Hiển thị Panel
        Time.timeScale = 0; // Tạm dừng trò chơi
    }

    // Ẩn panel
    public void HidePanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1; // Tiếp tục trò chơi
    }

    // Hàm khi chọn tăng máu
    public void OnChooseIncreaseHealth()
    {
        playerHealthController.AddHealth(20); // Tăng 20 máu
        playerHealthController.SetMaximumHealth(playerHealthController.GetMaximumHealth() + 20); // Tăng giới hạn máu tối đa
        HidePanel(); // Đóng panel
    }

    // Hàm khi chọn tăng sát thương
    public void OnChooseIncreaseDamage()
    {
        playerBullet.minDamage += 5; // Tăng sát thương tối thiểu
        playerBullet.maxDamage += 10; // Tăng sát thương tối đa
        HidePanel(); // Đóng panel
    }

    // Hàm khi chọn tăng số gun offset
    public void OnChooseIncreaseGunOffset()
    {
        if (playerShoot.GetAllGunOffsetsActive() >= 18)
        {
            
            if (notificationText != null)
            {
                notificationText.text = "Đã nâng cấp tối đa đường đạn";
            }
            return; 
        }

        playerShoot.AddGunOffset(); 
        HidePanel();
    }
}
