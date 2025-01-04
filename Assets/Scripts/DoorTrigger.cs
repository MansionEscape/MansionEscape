using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DoorTrigger : MonoBehaviour
{
    public GameObject targetDoor;
    public DoorInteraction door;

    private bool IsPlayerNearby;
    // Start is called before the first frame update
    void Start()
    {
        door = targetDoor.GetComponent<DoorInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerNearby && !door.doorUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            door.Unlock();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !door.doorUnlocked)
        {
            IsPlayerNearby = true;
            door.controller.instructionBox.SetActive(true);
            door.controller.instructionText.text = "Unlock the " + door.doorName;

            if (door.objectRenderer != null && door.lockedMaterial != null)
            {
                door.objectRenderer.material = door.lockedMaterial;
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !door.doorUnlocked)
        {
            IsPlayerNearby = false;
            door.controller.instructionBox.SetActive(false);
            door.controller.instructionText.text = "";

            if (door.objectRenderer != null && door.originalMaterial != null)
            {
                door.objectRenderer.material = door.originalMaterial;
            }
        }

    }
}
