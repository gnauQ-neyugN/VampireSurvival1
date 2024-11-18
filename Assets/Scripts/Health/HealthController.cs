using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    public HealthBarUI healthBar;
    public bool isDead = false;

    [SerializeField]
    private AudioClip _deathSound;

    private AudioSource _audioSource; // Nguồn phát âm thanh

    [SerializeField]
    private GameObject _deathPanel; // Tham chiếu đến Panel UI
    private void Start()
    {
        _currentHealth = _maximumHealth;
        _audioSource = GetComponent<AudioSource>();
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

    public void TakeDamage(float damageAmount)
    {
        if (healthBar != null)
            healthBar.UpdateHealth((int)_currentHealth, (int)_maximumHealth);

        if (_currentHealth == 0 || IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            if (this.gameObject.CompareTag("Enemy"))
            {
                FindObjectOfType<PlayerExp>().UpdateExperience(Random.Range(1, 4));
            }
            if (this.gameObject.CompareTag("Enemy2"))
            {
                FindObjectOfType<PlayerExp>().UpdateExperience(Random.Range(5, 9));
            }
            if (this.gameObject.CompareTag("Enemy3"))
            {
                FindObjectOfType<PlayerExp>().UpdateExperience(Random.Range(10, 15));
            }

            if (healthBar != null)
                healthBar.UpdateHealth((int)_currentHealth, (int)_maximumHealth);
            isDead = true;

            // Phát âm thanh khi chết
            if (_audioSource != null && _deathSound != null)
            {
                _audioSource.PlayOneShot(_deathSound);
            }

            if (_deathPanel != null)
            {
                _deathPanel.SetActive(true);
            }
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth >= _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }

        if (healthBar != null)
        {
            healthBar.UpdateHealth((int)_currentHealth, (int)_maximumHealth);
        }
    }

    public void SetMaximumHealth(float newMaxHealth)
    {
        _maximumHealth = newMaxHealth;
        _currentHealth = _maximumHealth; // Hồi đầy máu
        if (healthBar != null)
        {
            healthBar.UpdateHealth((int)_currentHealth, (int)_maximumHealth);
        }
    }

    public float GetMaximumHealth()
    {
        return _maximumHealth;
    }

     // Nút Retry
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load lại màn hiện tại
    }

    // Nút Main Menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Thay "MainMenu" bằng tên Scene menu chính của bạn
    }
}
