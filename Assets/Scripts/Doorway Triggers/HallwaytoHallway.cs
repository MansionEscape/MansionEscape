using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaytoHallway : MonoBehaviour
{
    public GameObject player, stairs, wallDivider1, wallDivider2;

    private bool playerInHallwayMain;
    private bool playerInHallwayBack;

    private float playerxAxis;


    void Update()
    {
        playerxAxis = player.transform.position.x;
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {
            playerInHallwayMain = true;

        }
        else if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {
            playerInHallwayBack = true;

        }

        if (other.gameObject == player && playerInHallwayMain)
        {
            stairs.SetActive(false);
            wallDivider1.SetActive(false);
            wallDivider2.SetActive(false);


        }
        else if (other.gameObject == player && playerInHallwayBack)
        {
            stairs.SetActive(true);
            wallDivider1.SetActive(true);
            wallDivider2.SetActive(true);

        }

    }

    void OnTriggerExit(Collider other)
    {
        //if player z axis less than threshold, player moves to Main Hallway
        if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {

            stairs.SetActive(true);
            wallDivider1.SetActive(true);
            wallDivider2.SetActive(true);

            playerInHallwayMain = false;
            playerInHallwayBack = false;

        }
        //if player z axis greater than threshold, player moves to Back Hallway
        else if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {

            stairs.SetActive(false);
            wallDivider1.SetActive(false);
            wallDivider2.SetActive(false);

            playerInHallwayMain = false;
            playerInHallwayBack = false;

        }
    }
}
