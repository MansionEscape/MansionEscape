using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestInteraction : MonoBehaviour
{
    public PlayerManager player; // Reference to player manager
    public MainController controller; // Reference to main controller
    public Item keyItem; // The key item to add to inventory
    public InputActionReference interact; // Input action for interacting
    private bool isPlayerNearby = false;
    private bool wasPressed;

    public string correctCombination = "4321"; // The correct 4-digit combination
    private string playerInput = ""; // The player's current input
    public GameObject combinationUI; // UI panel for input
    public TMPro.TMP_Text feedbackText; // Text to show feedback

    private void Start()
    {
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        controller = GameObject.Find("MainGameController").GetComponent<MainController>();

        // Hide combination UI initially
        if (combinationUI != null)
        {
            combinationUI.SetActive(false);
        }
    }

    private void Update()
    {
        wasPressed = interact.action.WasPressedThisFrame();
        if (isPlayerNearby && wasPressed)
        {
            OpenCombinationUI();
        }
    }

    private void OpenCombinationUI()
    {
        if (combinationUI != null)
        {
            combinationUI.SetActive(true);
            feedbackText.text = "Enter a 4-digit combination:";
            playerInput = ""; // Reset input
        }
    }
    public void CloseCombinationUI()
    {
        if (combinationUI != null)
        {
            combinationUI.SetActive(false);
            feedbackText.text = ""; // Clear feedback text
        }
    }


    public void InputDigit(string digit)
    {
        if (playerInput.Length < 4)
        {
            playerInput += digit;
            feedbackText.text = $"Entered: {playerInput}";
        }

        if (playerInput.Length == 4)
        {
            CheckCombination();
        }
    }

    public void ClearInput()
    {
        playerInput = "";
        feedbackText.text = "Enter a 4-digit combination:";
    }

    public void SubmitCombination()
    {
        if (playerInput.Length == 4)
        {
            CheckCombination();
        }
        else
        {
            feedbackText.text = "Please enter a 4-digit combination.";
        }
    }
        public void CheckCombination()
    {
        if (playerInput == correctCombination)
        {
            feedbackText.text = "Correct! You received a key.";
            InventoryManager.Instance.ObjectPickedUp(keyItem); // Add key to inventory

            // Hide the UI
            //if (combinationUI != null)
            //{
            //    combinationUI.SetActive(false);
            //}
        }
        else
        {
            feedbackText.text = "Incorrect combination. Try again.";
            playerInput = ""; // Reset input
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            controller.instructionBox.SetActive(true);
            controller.instructionText.text = "Press 'E' to interact with the chest.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            controller.instructionBox.SetActive(false);
            controller.instructionText.text = "";

            // Hide the UI
            if (combinationUI != null)
            {
                combinationUI.SetActive(false);
            }
        }
    }
}
