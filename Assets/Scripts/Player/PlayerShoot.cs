using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private List<Transform> _allGunOffsets; // Tất cả các vị trí gun offset đã định trước

    [SerializeField]
    private float _timeBetweenShots;

    [SerializeField]
    private AudioClip _shootSound; // Âm thanh bắn súng

    private AudioSource _audioSource; // Nguồn phát âm thanh
    private List<Transform> _activeGunOffsets = new List<Transform>(); // Danh sách gun offset đang hoạt động
    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;

    void Start()
    {
        // Khởi tạo với một gun offset đầu tiên
        if (_allGunOffsets.Count > 0)
        {
            _activeGunOffsets.Add(_allGunOffsets[0]);
        }

        // Lấy AudioSource từ GameObject
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullets();

                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }

    private void FireBullets()
    {
        // Lấy vị trí chuột trong không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0;

        // Tính hướng bắn từ Player đến chuột
        Vector3 fireDirection = (mousePosition - transform.position).normalized;

        // Bắn từ tất cả các gun offset đang hoạt động
        foreach (Transform gunOffset in _activeGunOffsets)
        {
            // Tạo viên đạn tại vị trí của gun offset
            GameObject bullet = Instantiate(_bulletPrefab, gunOffset.position, Quaternion.identity);

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

            // Đặt vận tốc cho viên đạn theo hướng bắn
            rigidbody.velocity = fireDirection * _bulletSpeed;

            // Quay viên đạn theo hướng bắn
            float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        // Phát âm thanh bắn súng
        if (_audioSource != null && _shootSound != null)
        {
            _audioSource.PlayOneShot(_shootSound);
        }
    }

    private void OnFire(InputValue inputValue)
    {
        _fireContinuously = inputValue.isPressed;

        if (inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }

    public void AddGunOffset()
    {
        // Kích hoạt thêm gun offset nếu còn offset chưa được sử dụng
        if (_activeGunOffsets.Count < _allGunOffsets.Count)
        {
            _activeGunOffsets.Add(_allGunOffsets[_activeGunOffsets.Count]);
        }
    }

    public int GetAllGunOffsetsActive(){
        return _activeGunOffsets.Count;
    }
}
