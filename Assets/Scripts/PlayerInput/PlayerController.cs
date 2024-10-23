using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public float speed = 10; // Movement speed of the player.
    private int count;
    public TMP_Text livesText;
    public GameObject chest;
    public GameObject chestTrigger;
    
    // create a vector2 object for (x, y) locations
    private Vector2 moveDirection;

    //call public rigidbody and assign it from the player.
    public Rigidbody rb;

    //attach Player/Move input action references from the input actions script.
    public InputActionReference move;


    private void Start()
    {
        chest.SetActive(false); 
        chestTrigger.SetActive(false);

        if (GameState.isPuzzleCompleted)
        {
            chest.SetActive(true);
            chestTrigger.SetActive(true);
        }


    }

    //private void OnDestroy()
    //{
    //    PlayerPrefs.DeleteKey("PuzzleCompleted"); // Clean up after use
    //}

    void Update()
    {
       //moveDirection is set using the move input action references
        moveDirection = move.action.ReadValue<Vector2>();
        
        //moveDirection.x set as the horizontal input
        float horizontalInput = moveDirection.x * speed;

        //moveDirection.y is set as the vertical Input
        float verticalInput = moveDirection.y * speed;

        //apply velocity to rigidbody as Vector3 (3d space) and apple the horizontal input to x and the vertical input to z leaving y as 0.
        //In the 3D space y would raise the player up. 
        rb.velocity = new Vector3(horizontalInput, 0, verticalInput);

    }



}
