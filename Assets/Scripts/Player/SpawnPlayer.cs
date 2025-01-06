using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;

    public GameObject playerControl;
    public PlayerManager playerManager;

    public GameObject GroundLevel;
    public GameObject Entrance, Hallway, Stairs, WallDivider, LivingRoom, Bathroom, DiningRoom, Kitchen, Pantry;

    public GameObject LevelOne;
    public GameObject UpstairsHallway, UpstairsBathroom, MasterBedroom, Study, TwinsBedroom, SecretRoom;


    public Vector3 HallwaySpawnPoint = new(-11, 4, -7);
    public Vector3 LivingRoomSpawnPoint = new(0, 4, -2);
    public Vector3 DiningRoomSpawnPoint = new(-20, 4, -8);
    public Vector3 KitchenSpawnPoint = new(-25, 4, 0);
    public Vector3 PantrySpawnPoint = new(-36, 4, 6);
    public Vector3 BathroomSpawnPoint = new(-4, 4, -10);


    public Vector3 UpstairsHallwaySpawnPoint = new(-10, 9, -5);
    public Vector3 TwinsBedroomSpawnPoint = new(-2, 9, -5);
    public Vector3 StudySpawnPoint = new(7, 9, 8);
    public Vector3 MasterBedroomSpawnPoint = new(-18, 9, 7);
    public Vector3 UpstairsBathroomSpawnPoint = new(-18, 9, -5);
    public Vector3 SecretRoomSpawnPoint = new(-32, 9, 5);

    void Start()
    {
        playerControl = GameObject.FindWithTag("PlayerManager");
        playerManager = playerControl.GetComponent<PlayerManager>();
    }
    public void RespawnPlayer()
    {
        SpawnPoint("Ground", "Entrance");
        playerManager.UpdatePlayer();
        
    }
    public void SpawnPoint(string mansionLevel, string currentRoom)
    {
        Entrance.SetActive(false);
        if (mansionLevel == "Ground")
        {
            if (currentRoom == "Main Hallway" || currentRoom == "Back Hallway")
            {

                Hallway.SetActive(true);
                Player.transform.position = HallwaySpawnPoint;
            }
            else if (currentRoom == "Dining Room")
            {
                DiningRoom.SetActive(true);
                Player.transform.position = DiningRoomSpawnPoint;
            }
            else if (currentRoom == "Kitchen")
            {
                Kitchen.SetActive(true);
                Player.transform.position = KitchenSpawnPoint;
            }
            else if (currentRoom == "Pantry")
            {
                Pantry.SetActive(true);
                Player.transform.position = PantrySpawnPoint;
            }
            else if (currentRoom == "Bathroom")
            {
                Stairs.SetActive(false);
                WallDivider.SetActive(false);
                Bathroom.SetActive(true);
                Player.transform.position = BathroomSpawnPoint;
            }
            else if (currentRoom == "Living Room")
            {
                Stairs.SetActive(false);
                WallDivider.SetActive(false);
                LivingRoom.SetActive(true);
                Player.transform.position = LivingRoomSpawnPoint;
            }
        }
        else
        {
            LevelOne.SetActive(true);
            Hallway.SetActive(true);
            GroundLevel.SetActive(false);
            UpstairsHallway.SetActive(false);

            if (currentRoom == "Upstairs Hallway")
            {
                UpstairsHallway.SetActive(true);
                Player.transform.position = UpstairsHallwaySpawnPoint;
            }
           
            else if (currentRoom == "Master Bedroom" || currentRoom == "Master Bathroom")
            {
                MasterBedroom.SetActive(true);
                Player.transform.position = MasterBedroomSpawnPoint;
            }
            else if (currentRoom == "Study")
            {
                Debug.Log("study Room active");
                Study.SetActive(true);
                Player.transform.position = StudySpawnPoint;
            }
            else if (currentRoom == "Secret Room")
            {
                SecretRoom.SetActive(true);
                Player.transform.position = SecretRoomSpawnPoint;
            }
            else if (currentRoom == "Upstairs Bathroom")
            {
                UpstairsBathroom.SetActive(true);
                Player.transform.position = UpstairsBathroomSpawnPoint;
            }
            else if (currentRoom == "Twins Bedroom")
            {
                TwinsBedroom.SetActive(true);
                Player.transform.position = TwinsBedroomSpawnPoint;
            }
        
        }
    }

}
