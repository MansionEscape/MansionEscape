using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireplaceInteraction : MonoBehaviour
{
    public PlayerManager player;
    public MainController controller;

    public List<Item> requiredItems;
    public Item cauldronItem;
    public Item keyItem;

    public GameObject cauldronPrefab;
    public Transform fireplacePosition;
    public GameObject fireAnimation;

    public InputActionReference interact; 
    private bool wasPressed;

    private bool isPlayerNearby = false; 

    private void Start()
    {
        player = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
        controller = GameObject.FindWithTag("MainGameController").GetComponent<MainController>();

        if (fireAnimation != null)
        {
            fireAnimation.SetActive(false); // Ensure fire animation is disabled initially
        }
    }

    private void Update()
    {
        wasPressed = interact.action.WasPressedThisFrame();
        if (isPlayerNearby && wasPressed)
        {
            if (PlayerHasAllItems(requiredItems))
            {
                if (PlayerHasItem(cauldronItem))
                {
                    TriggerFireplaceInteraction();
                }
                else
                {
                    controller.instructionBox.SetActive(true);
                    controller.instructionText.text = "How are you going to cook the food? You need something...";
                }
            }
            else
            {
                controller.instructionBox.SetActive(true);
                controller.instructionText.text = "You are missing some ingredients.";
            }
        }
    }

    private void TriggerFireplaceInteraction()
    {
        // Spawn the cauldron in the fireplace
        if (cauldronPrefab != null && fireplacePosition != null)
        {
            Instantiate(cauldronPrefab, fireplacePosition.position, fireplacePosition.rotation);
        }

        // Enable the fire animation
        if (fireAnimation != null)
        {
            fireAnimation.SetActive(true);
        }

        // Remove all required items from the player's inventory
        foreach (var requiredItem in requiredItems)
        {
            InventoryManager.Instance.Remove(requiredItem); // Remove each required item
        }
        InventoryManager.Instance.Remove(cauldronItem); // Remove the cauldron item

        // Add the key to the player's inventory
        InventoryManager.Instance.ObjectPickedUp(keyItem);

        // Update UI and notify player
        controller.instructionBox.SetActive(false);
        controller.instructionText.text = "The key has been added to your inventory!";
        controller.UpdateObjective("three");
        Debug.Log("Key added to inventory and ingredients removed!");
    }

    private bool PlayerHasItem(Item requiredItem)
    {
        foreach (var item in player.data.items)
        {
            if (item == requiredItem)
            {
                return true;
            }
        }
        return false;
    }

    private bool PlayerHasAllItems(List<Item> items)
    {
        foreach (var requiredItem in items)
        {
            if (!PlayerHasItem(requiredItem))
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!player.data.levelOneComplete && player.data.currentLevel == 2)
            {
                controller.UpdateObjective("one");
            }
            isPlayerNearby = true;
            controller.instructionBox.SetActive(true);
            controller.instructionText.text = "Press 'E' to interact with the fireplace.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            controller.instructionBox.SetActive(false);
            controller.instructionText.text = "";
        }
    }
}
