using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
    public Camera mainCamera;          // Reference to the main camera
    public Transform painting;         // Reference to the painting (for bounds)
    public Button zoomInButton;        // Zoom-in button
    public Button zoomOutButton;       // Zoom-out button
    public float panSpeed = 5f;        // Speed of panning
    public float panBoundaryY = 0.7f;  // Vertical boundary for panning (Y-axis)
    public float panBoundaryZ = 0.85f; // Horizontal boundary for panning (Z-axis)

    private bool isZoomedIn = false;   // Whether the camera is zoomed in
    private float zoomedOutX = 12f;    // Camera X position when zoomed out
    private float zoomedInX = 13.2f;   // Camera X position when zoomed in

    private Vector2 movementInput;     // Stores input for panning

    // Reference to the Input Action Asset
    public InputActionAsset inputActions;  // Drag your InputActionAsset here in the inspector

    private InputAction movementAction;   // Movement action (for panning)

    private void OnEnable()
    {
        // Find the "Player" action map and assign the "Movement" action
        var actionMap = inputActions.FindActionMap("Player");
        movementAction = actionMap.FindAction("Movement");

        // Enable the movement action
        movementAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the movement action
        movementAction.Disable();
    }

    void Update()
    {
        HandlePanning();
    }

    // Method to toggle zoom
    public void ToggleZoom(bool zoomIn)
    {
        if (zoomIn)
        {
            mainCamera.transform.position = new Vector3(zoomedInX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            isZoomedIn = true;
        }
        else
        {
            mainCamera.transform.position = new Vector3(zoomedOutX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            isZoomedIn = false;
        }

        // Update visibility of buttons
        UpdateButtonStates();
    }

    // Update button visibility based on zoom state
    private void UpdateButtonStates()
    {
        zoomInButton.gameObject.SetActive(!isZoomedIn); // Show zoom-in button if not zoomed in
        zoomOutButton.gameObject.SetActive(isZoomedIn); // Show zoom-out button if zoomed in
    }

    // Handle panning based on input
    private void HandlePanning()
    {
        if (!isZoomedIn) return; // Only allow panning when zoomed in

        // Read movement input from the action
        movementInput = movementAction.ReadValue<Vector2>();

        // Calculate new position with boundaries
        Vector3 newPosition = mainCamera.transform.position +
                              new Vector3(0, movementInput.y, -movementInput.x) * panSpeed * Time.deltaTime;

        newPosition.y = Mathf.Clamp(newPosition.y, painting.position.y - panBoundaryY, painting.position.y + panBoundaryY); // Limit Y movement
        newPosition.z = Mathf.Clamp(newPosition.z, painting.position.z - panBoundaryZ, painting.position.z + panBoundaryZ); // Limit Z movement

        // Apply the new position to the camera
        mainCamera.transform.position = newPosition;
    }
}
