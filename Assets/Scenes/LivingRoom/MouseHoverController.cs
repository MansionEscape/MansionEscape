using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class MouseHoverController : MonoBehaviour
{
    public Camera playerCamera;           // The camera the player is using to view the scene
    public float rayDistance = 10f;       // Distance of the ray cast
    public LayerMask interactableLayer;   // Layer to specify which objects are interactable

    private InteractiveObjectControl currentHighlightedObject = null; // Store the current highlighted object
    private Vector2 mousePosition;        // Store the mouse position

    // Reference to the Input Action Asset
    public InputActionAsset inputActions;
    private InputAction mouseMoveAction;
    private InputAction mouseLeftClickAction;

    private void OnEnable()
    {
        // Assign the actions
        var actionMap = inputActions.FindActionMap("Mouse");
        mouseMoveAction = actionMap.FindAction("MouseMove");
        mouseLeftClickAction = actionMap.FindAction("MouseLeftClick");

        // Enable the actions and subscribe to events
        mouseMoveAction.performed += OnMouseMove;
        mouseLeftClickAction.performed += OnMouseClick;

        mouseMoveAction.Enable();
        mouseLeftClickAction.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from events and disable actions
        mouseMoveAction.performed -= OnMouseMove;
        mouseLeftClickAction.performed -= OnMouseClick;

        mouseMoveAction.Disable();
        mouseLeftClickAction.Disable();
    }

    private void Update()
    {
        // Cast a ray from the camera's position to where the mouse is pointing
        Ray ray = playerCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Check if the ray hits an interactable object
        if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
        {
            // Get the InteractiveObjectControl component of the hit object
            InteractiveObjectControl highlightedObject = hit.collider.GetComponent<InteractiveObjectControl>();

            // If we are hitting a new object, update the highlight
            if (highlightedObject != null && highlightedObject != currentHighlightedObject)
            {
                // Reset the previous highlight if needed
                if (currentHighlightedObject != null)
                {
                    currentHighlightedObject.HighlightObject(false);
                }

                // Set the new object as highlighted
                currentHighlightedObject = highlightedObject;
                currentHighlightedObject.HighlightObject(true);
            }
        }
        else
        {
            // If no object is hit, reset the previous highlight
            if (currentHighlightedObject != null)
            {
                currentHighlightedObject.HighlightObject(false);
                currentHighlightedObject = null;
            }
        }
    }

    // Callback for mouse movement
    private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    // Callback for mouse clicks
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && currentHighlightedObject != null)
        {
            currentHighlightedObject.OnClick(); // Trigger object's click behavior
        }
    }
}
