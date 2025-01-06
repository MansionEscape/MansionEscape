using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject mainController;
    public MainController controller;

    public GameObject playerControl;
    public PlayerManager player;

    public GameObject inventoryControl;
    public InventoryManager inventory;

    public string sceneName;
    public string puzzleName;

    public int puzzleLevel;
    public string puzzleObjective;

    public bool playerObjective;
    public bool itemsRequired;

    public bool itemsInInventory;

    public List<string> requiredItems = new List<string>();
    private List<bool> itemCollected;

    public bool IsPlayerNearby;

    public InputActionReference interact;
    private bool wasPressed;

    // Start is called before the first frame update
    void Start()
    {
        

        mainController = GameObject.FindWithTag("MainGameController");
        controller = mainController.GetComponent<MainController>();

        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();

        inventoryControl = GameObject.FindWithTag("InventoryManager");
        inventory = inventoryControl.GetComponent<InventoryManager>();

        CheckIfObjectiveUpdated();

        wasPressed = false;
        IsPlayerNearby = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfObjectiveUpdated();
        if (mainController == null || playerControl == null || inventoryControl == null)
        {
            SearchForObjects();
        }
        
        wasPressed = interact.action.WasPressedThisFrame();

        if (wasPressed && IsPlayerNearby)
        {
            if (itemsRequired)
            {
                CheckPlayerInventory();
                if (itemsInInventory)
                {
                    LoadScene();
                }
                else
                {
                    controller.TriggerDialogue("I need to find the rest of the items first!");
                }
            }
            else
            {
                LoadScene();
                
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = true;

            controller.instructionBox.SetActive(true);
            controller.instructionText.text = "Start the " + puzzleName;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = false;

            controller.instructionBox.SetActive(false);
            controller.instructionText.text = "";
        }
    }

    public void CheckPlayerObjective(string objective)
    {
        if (objective == "one")
        {
            playerObjective = player.data.ObjectivePuzzleOneComplete;
        }
        else if (objective == "two")
        {
            playerObjective = player.data.ObjectivePuzzleTwoComplete;
        }
        else if (objective == "three")
        {
            playerObjective = player.data.ObjectivePuzzleThreeComplete;
        }

    }

    public void CheckIfObjectiveUpdated()
    {
        CheckPlayerObjective(puzzleObjective);
        if (player.data.currentLevel == puzzleLevel && playerObjective)
        {
            controller.instructionText.text = "";
            controller.instructionBox.SetActive(false);
            for (int i = 0; i < requiredItems.Count; i++)
            {
                foreach (var item in player.data.items)
                {
                    if (requiredItems[i] == item.itemName)
                    {
                        player.RemoveItemPuzzleItem(item);
                    }
                }
            }
            gameObject.SetActive(false);

        }
    }

    public void CheckPlayerInventory()
    {
        itemCollected = new List<bool>();

        for (int i = 0; i < requiredItems.Count; i++)
        {
            itemCollected.Add(false);

            foreach (var item in controller.currentPlayer.data.items)
            {
                if (item.itemName == requiredItems[i])
                {
                    itemCollected[i] = true;
                }
            }
        }

        itemsInInventory = !itemCollected.Contains(false);

    }

    public void SearchForObjects()
    {
        mainController = GameObject.FindWithTag("MainGameController");
        controller = mainController.GetComponent<MainController>();

        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();

        inventoryControl = GameObject.FindWithTag("InventoryManager");
        inventory = inventoryControl.GetComponent<InventoryManager>();
    }

    public void LoadScene()
    {
        player.LoadPlayerGame();
        SceneManager.LoadScene(sceneName);
    }


}
