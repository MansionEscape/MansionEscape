using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChestUnlocker : MonoBehaviour
{
    public TMP_InputField inputField; // input field where player entered the code
    public GameObject chestPanel; // panel to close after code is entered correctly
    public string correctCode; // correct code 
    public TMP_Text feedbackText; // to display when code is incorrect
    public GameObject chestContentsPanel; // // panel displaying chest contents (e.g. key, items)

    public void Start()
    {
        chestContentsPanel.SetActive(false);
    }

    public void OnEnterButtonClicked()
    {
        // get the value from the input field
        string enteredCode = inputField.text;

        // check if entered code is correct
        if (enteredCode == correctCode )
        {
            feedbackText.text = "Chest unlocked!";
            Debug.Log("chest unlocked successfully.");
            chestPanel.SetActive(false);               // close chest panel
            chestContentsPanel.SetActive(true);        // display chest contents panel
            
        }
        else
        {
            feedbackText.text = "Incorrect code. Try again!";
            feedbackText.color = Color.red;             // set feeback text color to red
            Debug.Log("Incorrect code entered.");
        }
    }

}
