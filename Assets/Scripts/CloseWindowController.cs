using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    public GameObject inventoryWindow;  // the window object to close

    // this method is called when the close button is clicked
    public void CloseInventory()
    {
        if (inventoryWindow != null)
        {
            inventoryWindow.SetActive(false);   // hide the inventory window 
        }
        else
        {
            Debug.LogWarning("Window to close is not assigned!");
        }
    }
}
