using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeerBarrelInteraction : MonoBehaviour
{
    public PlayerManager player; 
    public MainController controller; 
    public Item mugItem; 
    public Item beerItem; 
    public AudioClip pourSound; 
    private AudioSource audioSource; 
    private bool isPlayerNearby = false; 

    public Material highlightMaterial; 
    private Material originalMaterial;
    private Renderer barrelRenderer;

    public InputActionReference interact;
    private bool wasPressed;

    private void Start()
    {
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        controller = GameObject.Find("MainGameController").GetComponent<MainController>();
        audioSource = gameObject.AddComponent<AudioSource>();

        barrelRenderer = GetComponent<Renderer>();
        if (barrelRenderer != null)
        {
            originalMaterial = barrelRenderer.material;
        }
    }

    private void Update()
    {
        wasPressed = interact.action.WasPressedThisFrame();
        if (isPlayerNearby && wasPressed)
        {
            if (PlayerHasItem(mugItem))
            {
                Interact();
            }
            else
            {
                controller.instructionBox.SetActive(true);
                controller.instructionText.text = "You need a mug to get beer.";
            }
        }
    }

    private void Interact()
    {
        // Add beer to the inventory
        InventoryManager.Instance.ObjectPickedUp(beerItem);

        // Play the sound effect
        if (pourSound != null)
        {
            audioSource.PlayOneShot(pourSound);
        }

        controller.instructionBox.SetActive(false);
        controller.instructionText.text = "You got beer!";
        Debug.Log("Beer collected!");
    }

    private bool PlayerHasItem(Item requiredItem)
    {
        foreach (var item in player.data.items)
        {
            if (item == requiredItem)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            controller.instructionBox.SetActive(true);
            controller.instructionText.text = "Press 'E' to interact with the beer barrel.";

            // Highlight the barrel
            if (barrelRenderer != null && highlightMaterial != null)
            {
                barrelRenderer.material = highlightMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            controller.instructionBox.SetActive(false);
            controller.instructionText.text = "";

            // Revert barrel material to original
            if (barrelRenderer != null && originalMaterial != null)
            {
                barrelRenderer.material = originalMaterial;
            }
        }
    }
}
