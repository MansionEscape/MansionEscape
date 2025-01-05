using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public MainController controller;
    public PlayerManager player;

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
        CheckPlayerObjective(puzzleObjective);

        if (player.data.currentLevel > puzzleLevel)
        {
            Destroy(gameObject);

        }
        else if (playerObjective)
        {
            Destroy(gameObject);
        }


        controller = GameObject.Find("MainGameController").GetComponent<MainController>();

        wasPressed = false;
        IsPlayerNearby = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        wasPressed = interact.action.WasPressedThisFrame();

        if (wasPressed && IsPlayerNearby)
        {
            if (itemsRequired)
            {
                CheckPlayerInventory();
                if (itemsInInventory)
                {
                    controller.TriggerDialogue("We Have All The Items!! Load Scene Here!");
                }
                else
                {
                    controller.TriggerDialogue("I need to find the rest of the items first!");
                }
            }
            else
            {
                controller.TriggerDialogue("No Items Required! Load the scene.");
                
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

    public void LoadScene()
    {
        player.LoadPlayerGame();
        SceneManager.LoadScene(sceneName);
    }


}
