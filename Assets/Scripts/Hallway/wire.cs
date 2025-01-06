using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    private Camera mainCamera;
    private float zOffset;

    void Start()
    {
        mainCamera = Camera.main;
        // Calculate the offset for maintaining the wire's depth in 3D space
        zOffset = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseDown()
    {
        Debug.Log("Object clicked!");
    }

    private void OnMouseDrag()
    {
        Debug.Log("Dragging object");
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen space mouse position to world space
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Force Z position to remain consistent
        newPosition.z = transform.position.z;

        // Update the position of the wire
        transform.position = newPosition;
    }

}
