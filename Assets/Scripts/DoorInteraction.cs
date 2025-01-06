//using System.Collections;
//using System.Collections.Generic;
//using System.Security.Cryptography.X509Certificates;
//using UnityEngine;

//public class DoorInteraction : MonoBehaviour
//{
//    public MainController controller;
//    public InventoryManager inventory;
//    public string doorName;
//    public string keyRequired;
//    public int unlockedLevel;
//    public string objective;
//    private Animator animator;

//    public bool doorUnlocked;

//    public Material originalMaterial;
//    public Material lockedMaterial;
//    public Renderer objectRenderer;

//    private bool IsPlayerNearby;

//    public PlayerManager player;
//    // Start is called before the first frame update
//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
//        controller = GameObject.Find("MainGameController").GetComponent<MainController>();
//        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

//        if (player.data.currentLevel >= unlockedLevel)
//        {
//            doorUnlocked = true;
//            animator.SetBool("isUnlocked", true);
//        }
//        else
//        {
//            doorUnlocked = false;
//            animator.SetBool("isUnlocked", false);
//        }

//        objectRenderer = GetComponent<Renderer>();

//        if (objectRenderer != null)
//        {
//            originalMaterial = objectRenderer.material;
//        }

//    }

//    public void Unlock()
//    {
//        foreach (var item in player.data.items)
//        {
//            if (keyRequired == item.itemName)
//            {
//                objectRenderer.material = originalMaterial;
//                inventory.Remove(item);
//                doorUnlocked = true;
//                animator.Play("DoorOpenInwards", 0, 0.0f);
//                controller.instructionBox.SetActive(false);
//                controller.instructionText.text = "";
//                controller.UpdateObjective(objective);


//            }
//            else
//            {
//                controller.instructionBox.SetActive(true);
//                controller.instructionText.text = "Door Locked. Correct Key Required";
//            }
//        }
//    }


//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DoorInteraction : MonoBehaviour
//{
//    public MainController controller;
//    public InventoryManager inventory;
//    public string doorName;
//    public string keyRequired;
//    public int unlockedLevel;
//    public string objective;
//    private Animator animator;

//    public bool doorUnlocked;

//    public Material originalMaterial;
//    public Material lockedMaterial;
//    private List<Renderer> objectRenderers = new List<Renderer>();

//    private bool IsPlayerNearby;

//    public PlayerManager player;

//    // Start is called before the first frame update
//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
//        controller = GameObject.Find("MainGameController").GetComponent<MainController>();
//        inventory = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

//        if (player.data.currentLevel >= unlockedLevel)
//        {
//            doorUnlocked = true;
//            animator.SetBool("isUnlocked", true);
//        }
//        else
//        {
//            doorUnlocked = false;
//            animator.SetBool("isUnlocked", false);
//        }

//        // Collect all Renderer components in this GameObject and its children.
//        objectRenderers.AddRange(GetComponentsInChildren<Renderer>());

//        if (objectRenderers.Count > 0)
//        {
//            originalMaterial = objectRenderers[0].material;
//        }
//    }

//    public void Unlock()
//    {
//        foreach (var item in player.data.items)
//        {
//            if (keyRequired == item.itemName)
//            {
//                // Change all child renderers to the original material.
//                foreach (Renderer renderer in objectRenderers)
//                {
//                    renderer.material = originalMaterial;
//                }

//                inventory.Remove(item);
//                doorUnlocked = true;
//                animator.Play("DoorOpenInwards", 0, 0.0f);
//                controller.instructionBox.SetActive(false);
//                controller.instructionText.text = "";
//                controller.UpdateObjective(objective);
//            }
//            else
//            {
//                controller.instructionBox.SetActive(true);
//                controller.instructionText.text = "Door Locked. Correct Key Required";
//            }
//        }
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject mainControl;
    public MainController controller;

    public GameObject inventoryControl;
    public InventoryManager inventory;
    public string doorName;
    public string keyRequired;
    public int unlockedLevel;
    public string objective;
    private Animator animator;

    public bool doorUnlocked;

    public Material originalMaterial;
    public Material lockedMaterial;
    private List<Renderer> objectRenderers = new List<Renderer>();

    public GameObject playerControl;
    public PlayerManager player;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();
        mainControl = GameObject.FindWithTag("MainGameController");
        controller = mainControl.GetComponent<MainController>();

        inventoryControl = GameObject.FindWithTag("InventoryManager");
        inventory = inventoryControl.GetComponent<InventoryManager>();

        if (player.data.currentLevel >= unlockedLevel)
        {
            doorUnlocked = true;
            if (animator != null) animator.SetBool("isUnlocked", true);
        }
        else
        {
            doorUnlocked = false;
            if (animator != null) animator.SetBool("isUnlocked", false);
        }

        // Collect all Renderer components in this GameObject and its children
        objectRenderers.AddRange(GetComponentsInChildren<Renderer>());

        if (objectRenderers.Count > 0)
        {
            originalMaterial = objectRenderers[0].material;
        }
    }

    public void Unlock()
    {
        // Prevent unlocking an already unlocked door
        if (doorUnlocked) return; 

        if (CompareTag("AutoOpenDoor"))
        {
            OpenDoor(); 
            return;
        }

        foreach (var item in player.data.items)
        {
            if (keyRequired == item.itemName)
            {
                // Change all child renderers to the original material
                foreach (Renderer renderer in objectRenderers)
                {
                    renderer.material = originalMaterial;
                }

                inventory.Remove(item);
                doorUnlocked = true;
                OpenDoor();

                controller.instructionBox.SetActive(false);
                controller.instructionText.text = "";
                controller.UpdateObjective(objective);

                return;
            }
        }

        controller.instructionBox.SetActive(true);
        controller.instructionText.text = "Door Locked. Correct Key Required";
    }

    private void OpenDoor()
    {
        doorUnlocked = true;
        if (animator != null)
        {
            animator.Play("DoorOpenInwards", 0, 0.0f);
        }
    }
}
