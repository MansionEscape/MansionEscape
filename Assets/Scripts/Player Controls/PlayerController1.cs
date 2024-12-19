using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{

    public InputActionReference moveInput;

    private Animator animator;
    public CharacterController controller;

    private Vector3 playerVelocity;
    private Vector2 inputRead;

    public bool playerGrounded;

    public float gravity = -10.0f;
    public float movementSpeed = 10.0f;
    public float rotationSpeed = 1.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (playerGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //moveDirection is set using the move input action references
        inputRead = moveInput.action.ReadValue<Vector2>();

        //moveDirection.x set as the horizontal input
        float horizontalInput = inputRead.x * movementSpeed;

        //moveDirection.y is set as the vertical Input
        float verticalInput = inputRead.y * movementSpeed;

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        controller.Move(move * Time.deltaTime * movementSpeed);

        if (move != Vector3.zero)

        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);

            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);
            
        }
        else
        {

            animator.SetBool("isMoving", false);
        }

       

        
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        //apply velocity to rigidbody as Vector3 (3d space) and apple the horizontal input to x and the vertical input to z leaving y as 0.
        //In the 3D space y would raise the player up. 

        playerGrounded = controller.isGrounded;

        
    }

}
