using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public float speed = 10;
    public float rotationSpeed;
    private int count;
    public TMP_Text livesText;
    //[SerializeField] private float turnSpeed = 360;


    private CharacterController characterController;


    void Start()
    {
        animator = GetComponent<Animator>();

        count = 3;

        characterController = GetComponent<CharacterController>();

        livesText.text = "Lives: " + count.ToString();
   
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude);

        if ( movementDirection!= Vector3.zero)
        {
            animator.SetBool("isMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } else
        {
            animator.SetBool("isMoving", false);
        }

    }

    

  

}
