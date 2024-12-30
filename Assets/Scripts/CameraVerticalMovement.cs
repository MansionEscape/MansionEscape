using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVerticalMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the camera movement
    public float minY = -5f;    // Minimum Y position for the camera
    public float maxY = 5f;     // Maximum Y position for the camera

    void Update()
    {
        // Optional: Disable keyboard controls if using only UI buttons
        // You can remove this if you want both UI and keyboard options.
    }

    public void MoveUp()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += moveSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }

    public void MoveDown()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= moveSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = newPosition;
    }
}
