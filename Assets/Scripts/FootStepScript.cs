using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource;
    public AudioClip footstepClip;        
    public float stepInterval = 0.3f;     

    private InputAction movementAction;   
    private float stepTimer = 0f;         

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        movementAction = playerInput.actions["Movement"];
    }

    private void Update()
    {
        // Check if the movement action was pressed this frame
        if (movementAction.WasPressedThisFrame())
        {
            PlayFootstepSound();
        }

        Vector2 movementInput = movementAction.ReadValue<Vector2>();
        bool isCurrentlyMoving = movementInput.magnitude > 0.1f;

        if (isCurrentlyMoving)
        {

            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {

            stepTimer = 0f;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepClip != null && footstepAudioSource != null)
        {
            footstepAudioSource.PlayOneShot(footstepClip);
        }
    }
}
