using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damageAmount = 10f; // Số sát thương
    [SerializeField] private float _damageInterval = 0.3f; // Thời gian giữa mỗi lần gây sát thương
    private Animator _animator;
    private HealthController _playerHealth; // Lưu tham chiếu đến HealthController của player
    private float _damageTimer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Nếu player đang trong phạm vi và đang bị tấn công
        if (_playerHealth != null)
        {
            _damageTimer += Time.deltaTime;

            // Gây sát thương mỗi 0.3 giây
            if (_damageTimer >= _damageInterval)
            {
                _damageTimer = 0f; // Reset timer
                _playerHealth.TakeDamage(_damageAmount);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            // Bắt đầu animation tấn công
            SetAnimation(true);

            // Lưu HealthController của player để gây sát thương sau này
            _playerHealth = collision.gameObject.GetComponent<HealthController>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            // Dừng animation tấn công
            SetAnimation(false);

            // Xóa tham chiếu đến HealthController
            _playerHealth = null;
            _damageTimer = 0f; // Reset timer
        }
    }

    private void SetAnimation(bool isAttacking)
    {
        _animator.SetBool("Attack", isAttacking);
    }
}
