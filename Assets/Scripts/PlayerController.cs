using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public float speed = 10;
    public float rotationSpeed;
    private int count;
    public TMP_Text livesText;
    public GameOverScript gameOverScript;
    //[SerializeField] private float turnSpeed = 360;


    private CharacterController characterController;

    void Start()
    { 

        count = 3;
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        livesText.text = "Lives: " + count.ToString();

        if (gameOverScript.IsGameOver())
        {
            return;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude);

        if ( movementDirection!= Vector3.zero)
        {
           
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }


    }

    

  

}
