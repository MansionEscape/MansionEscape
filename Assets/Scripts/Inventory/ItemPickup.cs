using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography.X509Certificates;

public class ItemPickup : MonoBehaviour
{
    public PlayerManager player;
    public MainController controller;
    public Item item;  // collectable item
    private bool isPlayerNearby = false;  // track is player is within range

    public Material highlightMaterial; // material used for highlighting
    private Material originalMaterial; // original material of the object
    private Renderer objectRenderer;   // renderer component of the object

    private void Start()
    {
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        controller = GameObject.Find("MainGameController").GetComponent<MainController>();

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

        if (player.data.items != null)
        {
            foreach (var playerItem in player.data.items)
            {
                if(playerItem == item)
                {
                    Destroy(gameObject);
                }
            }

            
        }
    }

    private void Update()
    {
        // check if player is nearby and presses 'E' key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {

            Pickup();

            if (player.data.currentLevel == item.level)
            {
                controller.UpdateObjective(item.objective);
            }
        }
    }

    void Pickup()
    {
        InventoryManager.Instance.ObjectPickedUp(item);  // add item to inventory
        DestroyImmediate(gameObject,true);  // destroy item from scene
        Debug.Log("Item collected: " + item.itemName);
        controller.instructionBox.SetActive(false);
        controller.instructionText.text = "";
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
            controller.instructionBox.SetActive(true);
            controller.instructionText.text = "Pickup The " + item.itemName;
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
            controller.instructionBox.SetActive(false);
            controller.instructionText.text = "";
            isPlayerNearby = false;

            // revert objects material to original material
            if (objectRenderer != null && originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }
        }
    }

}
