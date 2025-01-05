using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChestInteraction : MonoBehaviour
{
    public Animator chestAnimator; // Animator for the chest
    public GameObject keyPrefab; // Assign your key prefab here
    public Transform keySpawnPoint; // Position where the key will appear
    public AudioClip chestOpenSound; // Optional chest opening sound
    private AudioSource audioSource;

    private bool isPlayerNearby = false; // To track if the player is near the chest
    private bool isOpened = false; // Ensure the chest opens only once

    public InputActionReference interact; // Input action for interacting (e.g., E key)

    private void Start()
    {
        // Ensure audio source is initialized
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the player presses the interaction key
        if (isPlayerNearby && interact.action.WasPressedThisFrame() && !isOpened)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        // Play the chest opening animation
        if (chestAnimator != null)
        {
            Debug.Log("Playing chest opening animation.");
            chestAnimator.Play("chestOpen", 0, 0f); // Directly play the "chestOpen" animation
        }
        else
        {
            Debug.LogError("Chest Animator is not assigned.");
        }

        // Play the chest opening sound effect
        if (chestOpenSound != null)
        {
            audioSource.PlayOneShot(chestOpenSound);
        }

        // Spawn the key inside the chest
        if (keyPrefab != null && keySpawnPoint != null)
        {
            Debug.Log("Spawning key at the chest.");
            Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("KeyPrefab or KeySpawnPoint is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the chest. Press 'E' to open.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the chest area.");
        }
    }
}
