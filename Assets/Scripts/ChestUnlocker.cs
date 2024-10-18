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
    
    public void OnEnterButtonClicked()
    {
        // get the value from the input field
        string enteredCode = inputField.text;

        // check if entered code is correct
        if (enteredCode == correctCode )
        {
            feedbackText.text = "Chest unlocked!";
            feedbackText.color = Color.green;        // set feeback text color to green
            Debug.Log("chest unlocked successfully.");
        }
        else
        {
            feedbackText.text = "Incorrect code. Try again!";
            feedbackText.color = Color.red;        // set feeback text color to red
            Debug.Log("Incorrect code entered.");
        }
    }

}
