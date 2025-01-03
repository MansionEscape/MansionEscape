using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardController : MonoBehaviour
{

    public float rotationSpeed = 20f;
    public float maxRotation = 10f;

    void Update()
    {
        float rotationX = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        float rotationZ = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        Vector3 currentRotation = transform.localEulerAngles;

        if (currentRotation.x > 180f) currentRotation.x -= 360f;
        if (currentRotation.z > 180f) currentRotation.z -= 360f;

        float newRotationX = Mathf.Clamp(currentRotation.x + rotationX, -maxRotation, maxRotation);
        float newRotationZ = Mathf.Clamp(currentRotation.z + rotationZ, -maxRotation, maxRotation);

        transform.localEulerAngles = new Vector3(newRotationX, currentRotation.y, newRotationZ);

    }
}

