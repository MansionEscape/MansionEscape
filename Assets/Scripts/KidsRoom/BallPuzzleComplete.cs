using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class BallPuzzleComplete : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 12f;
    public string finishZoneTag = "finishZone";
    public TMP_Text puzzleCompleteText;
    public InputAction ballMovement;
    
    Vector2 moveInput = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ballMovement.Enable();
    }

    private void OnDisable()
    {
        ballMovement.Disable();
    }

    void Update()
    {
        // Get input for movement
        //float moveX = Input.GetAxis("Horizontal"); 
        //float moveY = Input.GetAxis("Vertical");
        moveInput = ballMovement.ReadValue<Vector2>();

    }

    void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 movementForce = new Vector3(moveInput.x, 0, moveInput.y) * speed;
            rb.AddForce(movementForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter" + other.gameObject.tag);

        if (other.gameObject.tag == finishZoneTag)
        {
            gameObject.SetActive(false);

            if (puzzleCompleteText != null)
            {
                puzzleCompleteText.gameObject.SetActive(true);
            }
        }
    }
}
