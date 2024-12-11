using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZaxisDoorTrigger : MonoBehaviour
{
    public GameObject player, RoomTop, RoomBottom;

    private bool playerInRoomTop;
    private bool playerInRoomBottom;

    private float playerzAxis;


    void Update()
    {
        playerzAxis = player.transform.position.z;
    }
    void OnTriggerEnter(Collider other)
    {
        playerInRoomTop = false;
        playerInRoomBottom = false;

        //if player z axis is less than threshold, player enters from the Left Room
        if (other.gameObject == player && playerzAxis > gameObject.transform.position.z)
        {
            playerInRoomTop = true;

        }
        //if player z axis is greater than threshold, player enters from the Right Room
        else if (other.gameObject == player && playerzAxis < gameObject.transform.position.z)
        {
            
            playerInRoomBottom = true;

        }

        if (other.gameObject == player && playerInRoomBottom)
        {
            RoomTop.SetActive(true);


        }
        else if (other.gameObject == player && playerInRoomTop)
        {
            RoomBottom.SetActive(true);

     
        }

    }

    void OnTriggerExit(Collider other)
    {
        
        //if player z axis is greater than threshold, player exits to the Right Room
        if (other.gameObject == player && playerzAxis < gameObject.transform.position.z)
        {
            
            RoomTop.SetActive(false);


        }
        //if player z axis is less than threshold, player exits to the Left Room
        else if (other.gameObject == player && playerzAxis > gameObject.transform.position.z)
        {
            
          
            RoomBottom.SetActive(false);

        }
    }
}
