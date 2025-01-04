using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public MainController controller;
    public InventoryManager inventory;
    public string doorName;
    public string keyRequired;
    public int unlockedLevel;
    public string objective;
    private Animator animator;

    public bool doorUnlocked;

    public Material originalMaterial;
    public Material lockedMaterial;
    public Renderer objectRenderer;

    private bool IsPlayerNearby;

    public PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        controller = GameObject.Find("MainGameController").GetComponent<MainController>();
        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        if (player.data.currentLevel >= unlockedLevel)
        {
            doorUnlocked = true;
            animator.SetBool("isUnlocked", true);
        }
        else
        {
            doorUnlocked = false;
            animator.SetBool("isUnlocked", false);
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

    }

    public void Unlock()
    {
        foreach (var item in player.data.items)
        {
            if (keyRequired == item.itemName)
            {
                objectRenderer.material = originalMaterial;
                inventory.Remove(item);
                doorUnlocked = true;
                animator.Play("DoorOpen", 0, 0.0f);
                controller.instructionBox.SetActive(false);
                controller.instructionText.text = "";
                controller.UpdateObjective(objective);
                

            }
            else
            {
                controller.instructionBox.SetActive(true);
                controller.instructionText.text = "Door Locked. Correct Key Required";
            }
        }
    }

    
}
