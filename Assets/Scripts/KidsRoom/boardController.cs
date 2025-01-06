using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardController : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public float maxRotation = 10f;

    public InputAction moveAction; // Use a single action for both vertical and horizontal input

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        // Read the Vector2 value from the moveAction
        Vector2 input = moveAction.ReadValue<Vector2>();

        // Extract vertical and horizontal components
        float rotationX = input.y * rotationSpeed * Time.deltaTime;
        float rotationZ = -input.x * rotationSpeed * Time.deltaTime;

        // Get the current rotation and adjust for Unity's 360° system
        Vector3 currentRotation = transform.localEulerAngles;

        if (currentRotation.x > 180f) currentRotation.x -= 360f;
        if (currentRotation.z > 180f) currentRotation.z -= 360f;

        // Calculate the new rotation values within the clamped range
        float newRotationX = Mathf.Clamp(currentRotation.x + rotationX, -maxRotation, maxRotation);
        float newRotationZ = Mathf.Clamp(currentRotation.z + rotationZ, -maxRotation, maxRotation);

        // Apply the new rotation to the board
        transform.localEulerAngles = new Vector3(newRotationX, currentRotation.y, newRotationZ);
    }
}
