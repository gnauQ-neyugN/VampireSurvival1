using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    public int minDamage = 6;
    public int maxDamage = 10;
    private void Awake()
    {
        _camera = Camera.main;    
    }

    public int getMinDamage(){
        return minDamage; 
    }

     public int getMaxDamage(){
        return maxDamage; 
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            int damage = Random.Range(minDamage, maxDamage);
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}