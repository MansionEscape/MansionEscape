using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XaxisDoorTrigger : MonoBehaviour
{

    public GameObject player, RoomLeft, RoomRight;

    public bool playerInRoomLeft;
    public bool playerInRoomRight;
   

    private float playerxAxis;


    void Update()
    {
        playerxAxis = player.transform.position.x;
    }
    void OnTriggerEnter(Collider other)
    {

        //If Player x axis is less than threshold, Player enters from Top room.
        if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {
            playerInRoomLeft = true;

        }
        // if player x axis is greater than the threshold , Player enters from the Botttom.
        else if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {

            playerInRoomRight = true;

        }

        if (other.gameObject == player && playerInRoomRight)
        {
            RoomLeft.SetActive(true);
 

        } else if (other.gameObject == player && playerInRoomLeft)
        {
            RoomRight.SetActive(true);

        }
        
    }

    void OnTriggerExit(Collider other)
    {
        // if player x axis is greater than the threshold , Player exits to the Botttom.
        if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {
 
            RoomLeft.SetActive(false);
            playerInRoomRight = false;
            playerInRoomLeft = false;


        }
        //If Player x axis is less than threshold, Player exits to the Top room.
        else if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {
           
            RoomRight.SetActive(false);
            playerInRoomRight = false;
            playerInRoomLeft = false;

        } 
    }
}
