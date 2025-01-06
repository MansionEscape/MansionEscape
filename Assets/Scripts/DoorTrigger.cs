//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem.XR;

//public class DoorTrigger : MonoBehaviour
//{
//    public GameObject targetDoor;
//    public DoorInteraction door;

//    private bool IsPlayerNearby;
//    // Start is called before the first frame update
//    void Start()
//    {
//        door = targetDoor.GetComponent<DoorInteraction>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (IsPlayerNearby && !door.doorUnlocked && Input.GetKeyDown(KeyCode.E))
//        {
//            door.Unlock();
//        }
//    }
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player") && !door.doorUnlocked)
//        {
//            IsPlayerNearby = true;
//            door.controller.instructionBox.SetActive(true);
//            door.controller.instructionText.text = "Unlock the " + door.doorName;

//            if (door.objectRenderer != null && door.lockedMaterial != null)
//            {
//                door.objectRenderer.material = door.lockedMaterial;
//            }

//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player") && !door.doorUnlocked)
//        {
//            IsPlayerNearby = false;
//            door.controller.instructionBox.SetActive(false);
//            door.controller.instructionText.text = "";

//            if (door.objectRenderer != null && door.originalMaterial != null)
//            {
//                door.objectRenderer.material = door.originalMaterial;
//            }
//        }

//    }
//}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DoorTrigger : MonoBehaviour
//{
//    public GameObject targetDoor;
//    public DoorInteraction door;

//    private bool IsPlayerNearby;

//    // Start is called before the first frame update
//    void Start()
//    {
//        door = targetDoor.GetComponent<DoorInteraction>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (IsPlayerNearby && !door.doorUnlocked && Input.GetKeyDown(KeyCode.E))
//        {
//            door.Unlock();
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player") && !door.doorUnlocked)
//        {
//            IsPlayerNearby = true;
//            door.controller.instructionBox.SetActive(true);
//            door.controller.instructionText.text = "Unlock the " + door.doorName;

//            if (door.lockedMaterial != null)
//            {
//                // Change all child renderers to the locked material
//                foreach (Renderer renderer in door.GetComponentsInChildren<Renderer>())
//                {
//                    renderer.material = door.lockedMaterial;
//                }
//            }
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player") && !door.doorUnlocked)
//        {
//            IsPlayerNearby = false;
//            door.controller.instructionBox.SetActive(false);
//            door.controller.instructionText.text = "";

//            if (door.originalMaterial != null)
//            {
//                // Change all child renderers back to the original material
//                foreach (Renderer renderer in door.GetComponentsInChildren<Renderer>())
//                {
//                    renderer.material = door.originalMaterial;
//                }
//            }
//        }
//    }
//}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorTrigger : MonoBehaviour
{
    public GameObject targetDoor; // Reference to the actual door GameObject
    private DoorInteraction door; // Reference to the DoorInteraction script

    public InputActionReference interact;
    private bool wasPressed;

    private bool IsPlayerNearby;

    void Start()
    {
        
        wasPressed = false;
        // Ensure we fetch the DoorInteraction script from the target door
        if (targetDoor != null)
        {
            door = targetDoor.GetComponent<DoorInteraction>();
        }
        else
        {
            Debug.LogError("Target door is not assigned in the DoorTrigger script.");
        }
    }

    void Update()
    {
        wasPressed = interact.action.WasPressedThisFrame();

        if (door == null || !IsPlayerNearby) return;

        // If the door has the AutoOpenDoor tag, unlock it automatically
        if (door.CompareTag("AutoOpenDoor") && !door.doorUnlocked)
        {
            door.Unlock();
        }
        // Otherwise, check for manual unlock input
        else if (!door.doorUnlocked && wasPressed)
        {
            door.Unlock();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = true;

            if (door != null)
            {
                if (door.CompareTag("AutoOpenDoor") && !door.doorUnlocked)
                {
                    // Automatically unlock auto-open doors
                    door.Unlock();
                }
                else if (!door.doorUnlocked)
                {
                    // Display instruction for locked doors
                    door.controller.instructionBox.SetActive(true);
                    door.controller.instructionText.text = "Unlock the " + door.doorName;

                    // Change the material of locked doors
                    if (door.lockedMaterial != null)
                    {
                        foreach (Renderer renderer in door.GetComponentsInChildren<Renderer>())
                        {
                            renderer.material = door.lockedMaterial;
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = false;

            if (door != null && !door.CompareTag("AutoOpenDoor") && !door.doorUnlocked)
            {
                // Hide instructions for locked doors
                door.controller.instructionBox.SetActive(false);
                door.controller.instructionText.text = "";

                // Revert material to the original
                if (door.originalMaterial != null)
                {
                    foreach (Renderer renderer in door.GetComponentsInChildren<Renderer>())
                    {
                        renderer.material = door.originalMaterial;
                    }
                }
            }
        }
    }
}
