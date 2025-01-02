using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Windows.WebCam;


public class MainController : MonoBehaviour
{
    public SpawnPlayer spawnPlayer;
    public GameObject Player;

    public GameObject playerManagerObject;
    public PlayerManager currentPlayer;

    public TMP_Text welcomePlayerName;
    public GameObject welcomeWindow;

    public GameObject pauseWindow;

    public GameObject MapPage;
    public GameObject ObjectivesPage;
    public GameObject InventoryPage;

    public GameObject PlayerStatsPage;
    public TMP_Text playerNameText;
    public TMP_Text playerCurrentLevel;
    public TMP_Text playerCurrentRoom;

    public Sprite objectiveComplete;
    public Sprite objectiveDefault;

    public GameObject objectiveCompleteWindow;
    public TMP_Text objectiveCompleteText;

    public TMP_Text LevelTitle;
    public TMP_Text objectiveOneTitle;
    public TMP_Text objectiveOneText;
    public TMP_Text objectiveTwoTitle;
    public TMP_Text objectiveTwoText;
    public TMP_Text objectiveThreeTitle;
    public TMP_Text objectiveThreeText;

    public Image objectiveOneIcon;
    public Image objectiveTwoIcon;
    public Image objectiveThreeIcon;
    //ROOMS 

    
    void Awake()
    {
        playerManagerObject = GameObject.Find("PlayerManager");
        currentPlayer = playerManagerObject.GetComponent<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer.LoadPlayer();

        if(!currentPlayer.data.firstTimePlaying)
        {
            welcomePlayerName.text = currentPlayer.data.playerName;
            welcomeWindow.SetActive(true);
            currentPlayer.data.firstTimePlaying = true;
            currentPlayer.data.currentRoom = "Entrance";
            currentPlayer.data.mansionLevel = "Ground";
            currentPlayer.Save();

        }

        if(currentPlayer.data.currentRoom != "Entrance")
        {
            spawnPlayer.SpawnPoint(currentPlayer.data.mansionLevel, currentPlayer.data.currentRoom);
        }

        CheckTutorial();

    }
    public void CheckTutorial()
    {
        if (!currentPlayer.data.tutorialComplete)
        {
            LoadTutorialObjectives();
        }
        else
        {
            LoadMainObjectives();
        }
    }
    public void LoadTutorialObjectives()
    {
        LevelTitle.text = "Tutorial";
        objectiveOneTitle.text = "Time to move!";
        objectiveOneText.text = "Using the arrow keys or WASD to move your player and start to explore the mansion.";
        LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleOneComplete, objectiveOneIcon);

        objectiveTwoTitle.text = "Grab and Go";
        objectiveTwoText.text = "Around the mansion will be objects you can pick up. When prompted pick up an item using 'E'!";
        LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleTwoComplete, objectiveTwoIcon);

        objectiveThreeTitle.text = "Level One";
        objectiveThreeText.text = "Open the menu and select inventory, this is where you will find the objects you picked up! Select the object you just found and use it on the corresponding door.";
        LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleThreeComplete, objectiveThreeIcon);
    }

    public void LoadMainObjectives()
    {
        if (currentPlayer.data.currentLevel == 1)
        {
            LevelTitle.text = "Level One: The Dining Room";
            objectiveOneTitle.text = "";
            objectiveOneText.text = "";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleOneComplete, objectiveOneIcon);

            objectiveTwoTitle.text = "";
            objectiveTwoText.text = "";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleTwoComplete, objectiveTwoIcon);

            objectiveThreeTitle.text = "";
            objectiveThreeText.text = "";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleThreeComplete, objectiveThreeIcon);
        }
        else if (currentPlayer.data.currentLevel == 2)
        {

        }
    }

    public void LoadObjectiveStatus(bool objectiveStatus, Image objectiveIcon)
    {
        if (objectiveStatus)
        {
            objectiveIcon.sprite = objectiveComplete;
        }
        else
        {
            objectiveIcon.sprite = objectiveDefault;
        }
    }

    public void UpdateObjective(string objective)
    {
        if (objective == "one")
        {
            if (!currentPlayer.data.ObjectivePuzzleOneComplete)
            {
                ObjectivePopUp(objectiveOneTitle.text);
                currentPlayer.data.ObjectivePuzzleOneComplete = true;
                currentPlayer.UpdatePlayer();
                CheckProgression("tutorial");

            }
        }
        else if (objective == "two")
        {
            if (!currentPlayer.data.ObjectivePuzzleTwoComplete)
            {
                ObjectivePopUp(objectiveTwoTitle.text);
                currentPlayer.data.ObjectivePuzzleTwoComplete = true;
                currentPlayer.UpdatePlayer();
                CheckProgression("tutorial");

            }
        }
        else if (objective == "three")
        {
            if (!currentPlayer.data.ObjectivePuzzleThreeComplete)
            {
                ObjectivePopUp(objectiveThreeTitle.text);
                currentPlayer.data.ObjectivePuzzleThreeComplete = true;
                currentPlayer.UpdatePlayer();
                CheckProgression("tutorial");

            }
        }
    }

    public void CheckProgression(string level)
    {
        if (level == "tutorial")
        {
            if (currentPlayer.data.ObjectivePuzzleOneComplete && currentPlayer.data.ObjectivePuzzleTwoComplete && currentPlayer.data.ObjectivePuzzleThreeComplete)
            {
                ObjectivePopUp("Tutorial Complete!");
                currentPlayer.data.tutorialComplete = true;
                currentPlayer.UpdatePlayer();
                currentPlayer.LoadPlayer();
                ResetObjectives();
                LoadMainObjectives();

            }
        }
        else
        {
            currentPlayer.data.currentLevel++;

            if(level == "two")
            {

            }
        }

    }

    public void ObjectivePopUp(string objective)
    {
        objectiveCompleteText.text = objective;
        objectiveCompleteWindow.SetActive(true);
    }

    public void UpdateCurrentPlayerRoom(string roomName)
    {
        currentPlayer.data.currentRoom = roomName;
        currentPlayer.UpdatePlayer();
    }

    public void MapMenu()
    {
        MapPage.SetActive(true);
    }
    public void ObjectiveMenu()
    {
        LoadMainObjectives();
        ObjectivesPage.SetActive(true);
    }

    public void InventoryMenu()
    {
        InventoryPage.SetActive(true);
    }
    
    public void PlayerStatsMenu()
    {
        playerNameText.text = currentPlayer.data.playerName;
        playerCurrentLevel.text = currentPlayer.data.currentLevel.ToString();
        playerCurrentRoom.text = currentPlayer.data.currentRoom;
        PlayerStatsPage.SetActive(true);

    }

    public void PauseGame()
    {
        pauseWindow.SetActive(true);

    }

    public void ResumeGame()
    {
        pauseWindow.SetActive(false);
    }

    public void ExitGame()
    {
        currentPlayer.Save();
        Destroy(this.playerManagerObject);
        SceneManager.LoadScene("StartMenu");
    }

    public void ResetObjectives()
    {
        currentPlayer.data.ObjectivePuzzleOneComplete = false;
        currentPlayer.data.ObjectivePuzzleTwoComplete = false;
        currentPlayer.data.ObjectivePuzzleThreeComplete = false;


    }

    
}
