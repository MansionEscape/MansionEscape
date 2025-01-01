using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenToBackHallway : MonoBehaviour
{
    public GameObject player, RoomRight, RoomLeft, wallDivider1, stairs;
    private GameObject mainGameController;

    public MainController mainController;


    private bool playerInRoomRight;
    private bool playerInRoomLeft;

    private float playerxAxis;

    private void Awake()
    {
        mainGameController = GameObject.Find("MainGameController");
        mainController = mainGameController.GetComponent<MainController>();
    }


    void Update()
    {
        playerxAxis = player.transform.position.x;
    }
    void OnTriggerEnter(Collider other)
    {

        // if player z axis is less than threshold, player enters from the left room.
        if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {
            playerInRoomLeft = true;

        }

        //if z axis is greater than threshold, player enters from right room.
        else if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {
            playerInRoomRight = true;
        }

        if (other.gameObject == player && playerInRoomRight)
        {
            RoomLeft.SetActive(true);
            stairs.SetActive(false);
            wallDivider1.SetActive(false);
            

        }
        else if (other.gameObject == player && playerInRoomLeft)
        {
            RoomRight.SetActive(true);
            stairs.SetActive(false);
            wallDivider1.SetActive(false);
            

        }

    }

    void OnTriggerExit(Collider other)
    {


        // if player z axis is less than threshold, player exits to the left room.
        if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {
            stairs.SetActive(true);
            wallDivider1.SetActive(true);
            
            RoomRight.SetActive(false);
            mainController.UpdateCurrentPlayerRoom("Kitchen");
            playerInRoomRight = false;
            playerInRoomLeft = false;

        }
        //if z axis is greater than threshold, player exits to the right room.
        else if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {

            stairs.SetActive(false);
            wallDivider1.SetActive(false);

            RoomLeft.SetActive(false);
            mainController.UpdateCurrentPlayerRoom("Back Hallway");
            playerInRoomRight = false;
            playerInRoomLeft = false;

        }
    }
}
