using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public float speed = 10;
    public float rotationSpeed;
    //[SerializeField] private float turnSpeed = 360;

    private CharacterController characterController;


    void Start()
    {
        animator = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();
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

    void Look()
    {

        //
        //{

        //    
        //    var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        //    var skewedInput = matrix.MultiplyPoint3x4(input);

        //    var relative = (transform.position + skewedInput) - transform.position;
        //    var rot = Quaternion.LookRotation(relative, Vector3.up);

        //    transform.rotation, rot, turnSpeed * Time.deltaTime);

        //} else
        //{
        //    animator.SetBool("isMoving", false);
        //}
    }

}
