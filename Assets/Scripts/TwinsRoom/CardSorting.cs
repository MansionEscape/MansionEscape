using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CardSorting : MonoBehaviour
{
    public List<GameObject> compartments; // Assign the 4 compartments in the Inspector
    public List<GameObject> deckOfCards; // A list to store all the cards in the deck
    public GameObject currentCard; // The card currently being placed
    public TMP_Text feedbackText; // UI Text to provide feedback
    public Image puzzleCompleteImage;
    public TMP_Text codeDisplayText; // Displays the numbers of the special cards
    public List<GameObject> specialCards; // Assign the 4 special cards in the Inspector

    // Predefined special numbers
    public List<int> specialNumbers = new List<int> { 7, 4, 8, 9 };

    private int currentCompartmentIndex = 0;
    private List<int> foundSpecialNumbers = new List<int>(); // Track special card numbers found

    public InputAction navigateAction; // Handles left/right navigation
    public InputAction placeCardAction; // Handles placing the card
    public InputAction undoAction; // Handles undoing the last placement

    private void OnEnable()
    {
        navigateAction.Enable();
        placeCardAction.Enable();
        undoAction.Enable();
    }

    private void OnDisable()
    {
        navigateAction.Disable();
        placeCardAction.Disable();
        undoAction.Disable();
    }

    void Start()
    {
        if (deckOfCards.Count > 0)
        {
            currentCard = deckOfCards[0]; // Set the first card to place
        }

        if (puzzleCompleteImage != null)
        {
            puzzleCompleteImage.gameObject.SetActive(false);
        }

        UpdateCodeDisplay();
        HighlightCurrentCompartment();
    }

    void Update()
    {

        // Navigate compartments
        if (navigateAction.triggered)
        {
            Vector2 navigateInput = navigateAction.ReadValue<Vector2>();

            if (navigateInput.x > 0) // Right direction
            {
                NavigateCompartments(1);
            }
            else if (navigateInput.x < 0) // Left direction
            {
                NavigateCompartments(-1);
            }
        }

        // Handle card placement
        if (placeCardAction.triggered)
        {
            PlaceCard();
        }

        // Highlight current compartment
        HighlightCurrentCompartment();
        HighlightSpecialCard();
    }

    void NavigateCompartments(int direction)
    {
        currentCompartmentIndex += direction;

        // Wrap around the compartments
        if (currentCompartmentIndex < 0)
        {
            currentCompartmentIndex = compartments.Count - 1;
        }
        else if (currentCompartmentIndex >= compartments.Count)
        {
            currentCompartmentIndex = 0;
        }
    }

    void HighlightCurrentCompartment()
    {
        foreach (GameObject compartment in compartments)
        {
            compartment.GetComponent<Renderer>().material.color = Color.white;
        }

        compartments[currentCompartmentIndex].GetComponent<Renderer>().material.color = Color.yellow;
    }

    void HighlightSpecialCard()
    {
        if (currentCard != null)
        {
            // Check if current card is in the special cards list
            if (specialCards.Contains(currentCard))
            {
                // Highlight the special card in green
                currentCard.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                // Reset color for non-special cards
                currentCard.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }


    void PlaceCard()
    {
        if (currentCard != null)
        {
            // Place the card in the current compartment
            currentCard.transform.position = compartments[currentCompartmentIndex].transform.position;

            // Check if the card is special
            int cardIndex = specialCards.IndexOf(currentCard);
            if (cardIndex != -1 && !foundSpecialNumbers.Contains(specialNumbers[cardIndex]))
            {
                foundSpecialNumbers.Add(specialNumbers[cardIndex]);
                UpdateCodeDisplay();
            }

            // Remove the card from the deck and assign the next card
            deckOfCards.Remove(currentCard);
            AssignNextCard();

            feedbackText.text = $"Card placed in {compartments[currentCompartmentIndex].name}";
        }
        else
        {
            feedbackText.text = "No card to place!";
        }

        CheckPuzzleComplete();
    }

    void AssignNextCard()
    {
        if (deckOfCards.Count > 0)
        {
            currentCard = deckOfCards[0];
            HighlightCurrentCompartment();
        }
        else
        {
            currentCard = null;
            feedbackText.text = "All cards placed!";
            puzzleCompleteImage.gameObject.SetActive(true);
        }
    }

    void UpdateCodeDisplay()
    {
        string codeText = "Special Numbers: ";
        foreach (int number in foundSpecialNumbers)
        {
            codeText += number + " ";
        }

        codeDisplayText.text = codeText.Trim();
    }

    void CheckPuzzleComplete()
    {
        if (foundSpecialNumbers.Count == specialNumbers.Count)
        {
            puzzleCompleteImage.gameObject.SetActive(true); // Show the puzzle complete text
        }
    }

}
