using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDiretion;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }
    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDiretion = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDiretion = Vector2.zero;
        }
    }
    private void RotateTowardsTarget()
    {
        //if (_targetDiretion == Vector2.zero)
        //{
        //    return;
        //}
        //Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDiretion);
        //Quaternion rotationg = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        //_rigidbody.SetRotation(rotationg);
        if (_targetDiretion == Vector2.zero)
        {
            return;
        }

        // Nếu người chơi ở bên trái, scale sẽ là (-1, 1, 1); nếu bên phải, scale là (1, 1, 1)
        if (_targetDiretion.x < 0) // Người chơi ở bên trái
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_targetDiretion.x > 0) // Người chơi ở bên phải
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void SetVelocity()
    {
        if (_targetDiretion == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = _targetDiretion * _speed;
        }
    }
}
