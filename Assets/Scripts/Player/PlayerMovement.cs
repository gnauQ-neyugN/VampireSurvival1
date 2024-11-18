

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _screenBorder;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _smoothedInputMovementVelocity;
    private Camera _camera;

    public float dashBoost = 2f;
    private float dashTime;
    public float DashTime;
    private bool once;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _camera = Camera.main;
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) && dashTime <= 0)
        {
            _speed += dashBoost;
            dashTime = DashTime;
            once = true;
        }

        if (dashTime <= 0 && once)
        {
            _speed -= dashBoost;
            once = false;
        }
        else
        {
            dashTime -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        SetPlayerVelocity();
        FlipCharacter(); // Lật nhân vật theo hướng chuột
        SetAnimation();
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _smoothedInputMovementVelocity,
                    0.1f);

        rb.velocity = _smoothedMovementInput * _speed;
        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < _screenBorder && rb.velocity.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if ((screenPosition.y < _screenBorder && rb.velocity.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && rb.velocity.y > 0))
        {
           rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
    private void FlipCharacter()
    {
        // Lấy vị trí của chuột trên màn hình và chuyển sang không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f; // Đảm bảo z = 0 vì chúng ta đang làm việc trong không gian 2D

        // Tính toán hướng từ vị trí nhân vật đến vị trí chuột
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Lật nhân vật dựa trên hướng chuột
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Nhân vật nhìn sang phải
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Nhân vật nhìn sang trái
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);
    }
}
