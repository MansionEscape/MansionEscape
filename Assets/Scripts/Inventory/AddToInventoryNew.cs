using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddToInventoryNew : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName; // Optional: For debugging or future use
        public Sprite itemImage;
    }

    public List<InventoryItem> items = new List<InventoryItem>();
    public Transform inventoryGrid;

    public void AddItem(InventoryItem newItem)
    {
        items.Add(newItem);

        foreach (Transform slot in inventoryGrid)
        {
            Image slotImage = slot.GetComponentInChildren<Image>();

            if (slotImage != null && slotImage.sprite == null)
            {
                // Set the image of the next empty slot to the item's image
                slotImage.sprite = newItem.itemImage;
                slotImage.color = Color.white; // Ensure the slot is visible
                Debug.Log("Item added to inventory: " + newItem.itemName);
                return; // Exit the loop after adding the item
            }
        }

        // If no empty slot is found, log a message in the console
        Debug.Log("Inventory full, cannot add more items.");
    }
}
