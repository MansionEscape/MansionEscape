using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public float speed = 10;
    private int count;
    public TMP_Text livesText;
    //[SerializeField] private float turnSpeed = 360;

    private Vector2 moveDirection;

    public Rigidbody rb;

    //private CharacterController characterController;
    public InputActionReference move;

    
    void Update()
    {
       
        moveDirection = move.action.ReadValue<Vector2>();
        float horizontalInput = moveDirection.x * speed;
        float verticalInput = moveDirection.y * speed;

        rb.velocity = new Vector3(horizontalInput, 0, verticalInput);

    }



}
