using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;  // collectable item
    private bool isPlayerNearby = false;  // track is player is within range

    public Material highlightMaterial; // material used for highlighting
    private Material originalMaterial; // original material of the object
    private Renderer objectRenderer;   // renderer component of the object

    private void Start()
    {
        // get the object's renderer and store its original material
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    private void Update()
    {
        // check if player is nearby and presses 'E' key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(item);  // add item to inventory
        Destroy(gameObject);  // destroy item from scene
        Debug.Log("Item collected: " + item.itemName);
    }

    //private void OnMouseDown()
    //{
    //    Pickup();
    //}

    private void OnTriggerEnter(Collider other)
    {
        // check if player enters trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Press 'E' to pickup item."); // replace this with the UI player text

            // change objects material to highlight material
            if (objectRenderer != null && highlightMaterial != null)
            {
                objectRenderer.material = highlightMaterial;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // check if player exits trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // revert objects material to original material
            if (objectRenderer != null && originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }
        }
    }

}
