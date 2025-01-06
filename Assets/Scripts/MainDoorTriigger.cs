using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainDoorTriigger : MonoBehaviour
{
    public GameObject targetDoor; // Reference to the actual door GameObject
    private DoorInteraction door; // Reference to the DoorInteraction script

    public GameObject playerControl;
    public PlayerManager player;

    public GameObject complete;
    public GameObject Menu;
    public GameObject Pause;
    public GameObject Reset;

    public InputActionReference interact;
    private bool wasPressed;

    private bool IsPlayerNearby;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();

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

    // Update is called once per frame
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

    public void RunEndGame()
    {
        Menu.SetActive(false);
        Pause.SetActive(false);
        Reset.SetActive(false);
        complete.SetActive(true);
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        player.UpdatePlayer();
        Destroy(this.player);
        SceneManager.LoadScene("StartMenu");
    }

    private void OnTriggerEnter(Collider other)
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
    private void OnTriggerExit(Collider other)
    {
        
    }
}
