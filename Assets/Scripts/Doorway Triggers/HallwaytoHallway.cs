using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwaytoHallway : MonoBehaviour
{
    public GameObject player, stairs, wallDivider;
    private GameObject mainGameController;

    public MainController mainController;

    private bool playerInHallwayMain;
    private bool playerInHallwayBack;

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
            wallDivider.SetActive(false);
            


        }
        else if (other.gameObject == player && playerInHallwayBack)
        {
            stairs.SetActive(true);
            wallDivider.SetActive(false);

        }

    }

    void OnTriggerExit(Collider other)
    {
        //if player z axis less than threshold, player moves to Main Hallway
        if (other.gameObject == player && playerxAxis < gameObject.transform.position.x)
        {

            stairs.SetActive(true);
            wallDivider.SetActive(true);
            mainController.UpdateCurrentPlayerRoom("Main Hallway");
            playerInHallwayMain = false;
            playerInHallwayBack = false;

        }
        //if player z axis greater than threshold, player moves to Back Hallway
        else if (other.gameObject == player && playerxAxis > gameObject.transform.position.x)
        {

            stairs.SetActive(false);
            wallDivider.SetActive(false);
            mainController.UpdateCurrentPlayerRoom("Back Hallway");
            playerInHallwayMain = false;
            playerInHallwayBack = false;

        }
    }
}
