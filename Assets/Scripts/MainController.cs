using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MainController : MonoBehaviour
{
    public float typingSpeed = 0.01f;
    public bool isPaused;
    public bool menuOpen;
    public SpawnPlayer spawnPlayer;
    public GameObject Player;

    public GameObject instructionBox;
    public TMP_Text instructionText;

    public GameObject playerManagerObject;
    public PlayerManager currentPlayer;

    public TMP_Text welcomePlayerName;
    public GameObject welcomeWindow;

    public GameObject pauseWindow;

    public GameObject DialogueBox;
    public TMP_Text DialogueText;

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
        //Assigns object from StartMenu
        playerManagerObject = GameObject.Find("PlayerManager");
        currentPlayer = playerManagerObject.GetComponent<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //initiates booleans - if one is false, player movement disabled
        menuOpen = false;
        isPaused = false;

 
        //Loads currently player data from the player manager
        currentPlayer.LoadPlayer();

        //Checks if the player is new by checking if they have completed the first time playing
        if(!currentPlayer.data.firstTimePlayingComplete)
        {
            //sets menuOpen as true (for tutorial page)
            menuOpen = true;
            welcomePlayerName.text = currentPlayer.data.playerName; // player name data for welcome message
            welcomeWindow.SetActive(true); //Opens the welcome message
            currentPlayer.data.firstTimePlayingComplete = true; //sets the players first time playing to
            currentPlayer.data.currentRoom = "Entrance"; //intiate the players current room - entrance starting room
            currentPlayer.data.mansionLevel = "Ground"; // ground floor as the first set of rooms
            currentPlayer.Save(); // saves the players data

        }

        //checks if the player is not in the entrance, and spawns the players according to the current room
        if(currentPlayer.data.currentRoom != "Entrance")
        {
            spawnPlayer.SpawnPoint(currentPlayer.data.mansionLevel, currentPlayer.data.currentRoom);
        }

        //Load Main Objectives after all the load up
        LoadMainObjectives();

    }

    public void LoadInstructionPrompt(string instruction)
    {
        instructionBox.SetActive(true);
    }

    //Objectives Text and information
    public void LoadMainObjectives()
    {
        //Checks what the players current level is and loads the main objectives accordingly
        // All set the levels title, objective titles and objective descriptions
        if (currentPlayer.data.currentLevel == 0)
        {
            LevelTitle.text = "Tutorial";
            objectiveOneTitle.text = "Objective 1: Time to move!";
            objectiveOneText.text = "Using the arrow keys or WASD to move your player and start to explore the mansion.";

            //LoadObjectiveStatus checks the status of the objective and assigns the corresponding icon. (See method for more detailed comments)
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleOneComplete, objectiveOneIcon);

            objectiveTwoTitle.text = "Objective 2: Grab and Go";
            objectiveTwoText.text = "Around the mansion will be objects you can pick up. When prompted pick up an item using 'E'!";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleTwoComplete, objectiveTwoIcon);

            objectiveThreeTitle.text = "Objective 3: Level One";
            objectiveThreeText.text = "Open the menu and select inventory, this is where you will find the objects you picked up! Select the object you just found and use it on the corresponding door.";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleThreeComplete, objectiveThreeIcon);
        }
        else if (currentPlayer.data.currentLevel == 1)
        {
            LevelTitle.text = "Level One: The Dining Room";
            objectiveOneTitle.text = "Objective 1: Smashing!";
            objectiveOneText.text = "Maybe theres an object around the room that can go on the table?";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleOneComplete, objectiveOneIcon);

            objectiveTwoTitle.text = "Objective 2: The Full Dining Experience";
            objectiveTwoText.text = "This Dining Room is fit for a king! Although you could never dine at an unset table. Correctly arrange and set the table, maybe it will reveal something... ";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleTwoComplete, objectiveTwoIcon);

            objectiveThreeTitle.text = "Objective 3: I'm Hungry!";
            objectiveThreeText.text = "We need to open the door to the kitchen! It must be around here somewhere...";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleThreeComplete, objectiveThreeIcon);
        }
        else if (currentPlayer.data.currentLevel == 2)
        {
            LevelTitle.text = "Level Two: The Kitchen";
            objectiveOneTitle.text = "Objective 1: Recipe For disaster";
            objectiveOneText.text = "In the kitchen theres a recipe book, have a look at it";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleOneComplete, objectiveOneIcon);

            objectiveTwoTitle.text = "Objective 2: Unlock The Pantry";
            objectiveTwoText.text = "";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleTwoComplete, objectiveTwoIcon);

            objectiveThreeTitle.text = "Objective 3: Burn Baby Burn";
            objectiveThreeText.text = "";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleThreeComplete, objectiveThreeIcon);
        }
        else if (currentPlayer.data.currentLevel == 3)
        {
            LevelTitle.text = "Level Three: The Living Room";
            objectiveOneTitle.text = "Objective 1: The Correct Notes";
            objectiveOneText.text = "Theres a grand piano! Maybe theres some sheet music around here somewhere..";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleOneComplete, objectiveOneIcon);

            objectiveTwoTitle.text = "Objective 2: Muscial To My Ears";
            objectiveTwoText.text = "I wonder what will happen if we play the correct";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleTwoComplete, objectiveTwoIcon);

            objectiveThreeTitle.text = "Objective 3: Level Two";
            objectiveThreeText.text = "";
            LoadObjectiveStatus(currentPlayer.data.ObjectivePuzzleThreeComplete, objectiveThreeIcon);
        }
    }

    //Load Objective Status takes the objective boolean from the player and the corresponding objective Icon
    public void LoadObjectiveStatus(bool objectiveStatus, Image objectiveIcon)
    {
        //If the player has completed the objective the complete sprite will be applied and be turned green.
        if (objectiveStatus)
        {
            objectiveIcon.sprite = objectiveComplete;
            objectiveIcon.color = Color.green;
        }
        else
        {
            //Else the defauls X will be used to represent incomplete objectives
            objectiveIcon.sprite = objectiveDefault;
            objectiveIcon.color = Color.red;
        }
    }

    //Update Objectives takes a string that will state if its the first, second or third objective
    public void UpdateObjective(string objective)
    {
        if (objective == "one")
        {
            //Checks if this objective has not already been completed
            if (!currentPlayer.data.ObjectivePuzzleOneComplete)
            {
                //Pop up will be displayed with the text of the objective
                ObjectivePopUp(objectiveOneTitle.text);
                //sets the objective to complete
                currentPlayer.data.ObjectivePuzzleOneComplete = true;
                currentPlayer.UpdatePlayer(); // Updates player which saves the changed values
                CheckProgression(); //Check Progression checks if this is the last objective to be completed in the level

            }
        }
        else if (objective == "two")
        {
            if (!currentPlayer.data.ObjectivePuzzleTwoComplete)
            {
                ObjectivePopUp(objectiveTwoTitle.text);
                currentPlayer.data.ObjectivePuzzleTwoComplete = true;
                currentPlayer.UpdatePlayer();
                CheckProgression();

            }
        }
        else if (objective == "three")
        {
            if (!currentPlayer.data.ObjectivePuzzleThreeComplete)
            {
                ObjectivePopUp(objectiveThreeTitle.text);
                currentPlayer.data.ObjectivePuzzleThreeComplete = true;
                currentPlayer.UpdatePlayer();
                CheckProgression();

            }
        }
    }

    //Checks the Progression of the Player
    public void CheckProgression()
    {
        //Checks if all objectives are completed
        if (currentPlayer.data.ObjectivePuzzleOneComplete && currentPlayer.data.ObjectivePuzzleTwoComplete && currentPlayer.data.ObjectivePuzzleThreeComplete)
        {
            //If all objectives complete, players level is increased by 1
            currentPlayer.data.currentLevel++;
            int level = currentPlayer.data.currentLevel;

            //checks what the players new level is and Loads the code based on that
            if(level == 1)
            {

                ObjectivePopUp("Tutorial Complete!");
                currentPlayer.data.tutorialComplete = true;
                currentPlayer.UpdatePlayer();
                currentPlayer.LoadPlayer();
                ResetObjectives();
                LoadMainObjectives();
            }
            else if (level == 2)
            {
                ObjectivePopUp("Level One Complete!");
                currentPlayer.data.levelOneComplete = true;
                currentPlayer.UpdatePlayer();
                currentPlayer.LoadPlayer();
                ResetObjectives();
                LoadMainObjectives();
            }

        }
        
        
    }

    //Provide hints in dialogue box for the player - works the same as main objectives where the hints are loaded determined on the players level
    //Hints one, Two and Three all work the same way
    public void HintOne()
    {
        //makes sure if a hint is called at the same time all Coroutines are stopped and the dialogue box and text are not active/blank
        StopAllCoroutines();
        DialogueBox.SetActive(false);
        DialogueText.text = "";
        if (currentPlayer.data.currentLevel == 0)
        {
            if (!currentPlayer.data.ObjectivePuzzleOneComplete)
            {
                menuOpen = false;
                StartCoroutine(RunDialogue("Lets explore! Move me with WASD or the arrow keys!"));
            }
            else
            {
                menuOpen = false;
                StartCoroutine(RunDialogue("This objective is complete! What's next?"));
            }
        }
        if (currentPlayer.data.currentLevel == 1)
        {
            if (!currentPlayer.data.ObjectivePuzzleOneComplete)
            {
                menuOpen = false;
                StartCoroutine(RunDialogue("There's plates and cutlery on the table, I wonder what will happen if we set the table!"));
            }
            else
            {
                menuOpen = false;
            }
        }
    }

    public void HintTwo()
    {
        StopAllCoroutines();
        DialogueBox.SetActive(false);
        DialogueText.text = "";
        if (currentPlayer.data.currentLevel == 0)
        {
            if (!currentPlayer.data.ObjectivePuzzleTwoComplete)
            {
                menuOpen = false;
                StartCoroutine(RunDialogue("There's a key on the ground.. lets pick it up!"));
            }
            else
            {
                menuOpen = false;
                StartCoroutine(RunDialogue("The key has been collected! What should we do with it?"));
            }
        }
        if (currentPlayer.data.currentLevel == 1)
        {
            if (!currentPlayer.data.ObjectivePuzzleTwoComplete)
            {
                menuOpen = false;
                RunDialogue("");
            }
            else
            {
                menuOpen = false;
                RunDialogue("");
            }
        }
    }
       

    public void HintThree()
    {
        StopAllCoroutines();
        DialogueBox.SetActive(false);
        DialogueText.text = "";
        if (currentPlayer.data.currentLevel == 0)
        {
            if (!currentPlayer.data.ObjectivePuzzleThreeComplete)
            {
                menuOpen = false;
                StartCoroutine(RunDialogue("The name of the key must match a room in this hallway!"));
            }
            else
            {
                menuOpen = false;
                RunDialogue("");
            }
        }
        if (currentPlayer.data.currentLevel == 1)
        {
            if (!currentPlayer.data.ObjectivePuzzleThreeComplete)
            {
                menuOpen = false;
                RunDialogue("");
            }
            else
            {
                menuOpen = false;
                RunDialogue("");
            }
        }
    }

    //Trigger dialogue to allow for dialogues to be triggered in other methods
    public void TriggerDialogue(string text)
    {
        DialogueBox.SetActive(false);
        DialogueText.text = "";
        StopAllCoroutines();
        StartCoroutine(RunDialogue(text));
    }

    //Allows for a pop up to occur following the update of any objective
    public void ObjectivePopUp(string objective)
    {
        objectiveCompleteText.text = objective;
        objectiveCompleteWindow.SetActive(true);
    }

    //Method used in door trigger scripts that toggle rooms on and off, sets the players room name 
    public void UpdateCurrentPlayerRoom(string roomName)
    {
        currentPlayer.data.currentRoom = roomName;
        currentPlayer.UpdatePlayer();
    }

    //Opens the mapmenu page
    public void MapMenu()
    {
        MapPage.SetActive(true);
    }
    //Opens the objective menu page
    public void ObjectiveMenu()
    {
        LoadMainObjectives();
        ObjectivesPage.SetActive(true);
    }

    //Opens the inventory menu page
    public void InventoryMenu()
    {
        InventoryPage.SetActive(true);
    }

    //Opens the player stats menu page
    public void PlayerStatsMenu()
    {
        playerNameText.text = currentPlayer.data.playerName;
        playerCurrentLevel.text = currentPlayer.data.currentLevel.ToString();
        playerCurrentRoom.text = currentPlayer.data.currentRoom;
        PlayerStatsPage.SetActive(true);

    }

    //Opens the menu page Loads Main Objectives on opening and sets menu Open to true
    public void MenuOpen()
    {
        LoadMainObjectives();
        menuOpen = true; // 
    }

    public void MenuClosed()
    {
        menuOpen = false;
    }

    public void PauseGame()
    {
        pauseWindow.SetActive(true);
        isPaused = true;

    }

    public void ResumeGame()
    {
        pauseWindow.SetActive(false);
        isPaused = false;
    }

    //On Exit Player data is saved, the current player manager object is destroyed and start menu going
    public void ExitGame()
    {
        currentPlayer.Save();
        Destroy(this.playerManagerObject);
        SceneManager.LoadScene("StartMenu");
    }

    //Dialgoue box Runs text as if its being typed

    public IEnumerator RunDialogue(string Text)
    {
        DialogueBox.SetActive(true);
        yield return StartCoroutine(RunDialogueText(Text));
        yield return new WaitForSeconds(6);
        DialogueBox.SetActive(false);
        DialogueText.text = "";
    }
    IEnumerator RunDialogueText(string dialogueText)
    {
        foreach (char letter in dialogueText)
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

   //resets objectives once all objectives are completed.
    public void ResetObjectives()
    {
        currentPlayer.data.ObjectivePuzzleOneComplete = false;
        currentPlayer.data.ObjectivePuzzleTwoComplete = false;
        currentPlayer.data.ObjectivePuzzleThreeComplete = false;


    }

    
}
