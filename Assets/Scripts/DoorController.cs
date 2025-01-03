using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private Animator myDoor = null;
    public GameObject player; // Player object
    public GameObject door; // Door object
    public GameObject gameCompleteMenu; // game complete menu UI panel to display when door unlocked
    public GameObject inventoryWindow; // Reference to the InventoryWindow GameObject
    public Transform inventoryGrid; // inventory grid - background object under inventoryWindow
    public Material lockedMaterial; // Highlight material when the door is locked
    public Material unlockedMaterial; // Highlight material when the door is unlocked
    public Material defaultMaterial; // Material for door when player is not near
    public TMP_Text objectText; // Text for interaction prompts
    public string lockedMessage; // Custom prompt message to display when door is locked
    public string unlockedMessage; // Custom message to display when door is unlocked
    public bool unlocked;

    private void Start()
    {

        gameCompleteMenu.SetActive(false); // hide game complete menu panel
        unlocked = false;
        objectText.text = string.Empty;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (CheckForKey())
            {
                Debug.Log("Key found in inventory.");
                door.GetComponent<Renderer>().material = unlockedMaterial; // set colour of door to unlocked 
                objectText.text = unlockedMessage; // Display the custom message prompt
                myDoor.Play("DoorOpen", 0, 0.0f); // play door open animation
                gameCompleteMenu.SetActive(true); // display game complete menu
            }
            else
            {
                Debug.Log("Key not found in inventory.");
                door.GetComponent<Renderer>().material = lockedMaterial; // set colour of door to locked
                objectText.text = lockedMessage; // Display the custom message prompt
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            door.GetComponent<Renderer>().material = defaultMaterial;
            objectText.text = string.Empty;

            if (unlocked)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
                // display game complete panel
            }
            
        }
    }

    bool CheckForKey()
    {
        // check if key found in inventory items
        foreach (Transform slot in inventoryGrid)
        {
            TMP_Text slotText = slot.GetComponentInChildren<TMP_Text>();

            if (slotText != null && slotText.text == "Key")
            {
                Debug.Log("key found");
                unlocked = true;
                return true; // terminate loop after finding key
            }
        }

        // If the loop completes without finding the key, return false
        Debug.Log("Key not found in inventory.");
        return false;
    }
}


