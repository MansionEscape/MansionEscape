using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    public Transform inventoryGrid;

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        
        foreach (Transform slot in inventoryGrid)
        {
            TMP_Text slotText = slot.GetComponentInChildren<TMP_Text>();

            if (string.IsNullOrEmpty(slotText.text))
            {
                // update the text of the next empty slot to the new item name
                slotText.text = itemName;
                Debug.Log("Item added to inventory: " + itemName);
                return; // terminate loop after adding item
            }
        }

        // if no empty slot found, log message in console
        Debug.Log("Inventory full, cannot add more items.");
    }

    
}
