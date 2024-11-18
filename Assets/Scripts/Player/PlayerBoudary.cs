using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoudary : MonoBehaviour
{
    public Transform boundaryTransform;  // Kéo đối tượng Boundary vào đây
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void Start()
    {
        // Lấy vị trí của Boundary
        float X0 = boundaryTransform.position.x;
        float Y0 = boundaryTransform.position.y;

        // Kích thước của BoxCollider2D
        float sizeX = 51.75764f;
        float sizeY = 18.61734f;

        // Tính toán giới hạn dựa trên kích thước và vị trí của Boundary
        minX = X0 - (sizeX / 2);
        maxX = X0 + (sizeX / 2);
        minY = Y0 - (sizeY / 2);
        maxY = Y0 + (sizeY / 2);
    }

    void Update()
    {
        // Giới hạn vị trí của Player trong khu vực của Boundary
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
}
