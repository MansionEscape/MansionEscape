using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallPuzzleComplete : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 12f;
    public string finishZoneTag = "finishZone";
    public TMP_Text puzzleCompleteText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (puzzleCompleteText != null)
        {
            puzzleCompleteText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");    

        // Apply movement force based on input
        Vector3 movementForce = new Vector3(moveX, 0, moveY) * speed;

        rb.AddForce(movementForce);  // Apply force to the Rigidbody for movement
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
