using UnityEngine;
using UnityEngine.UI;

public class ZoomController : MonoBehaviour
{
    public Camera mainCamera;      // Reference to the main camera
    public Transform painting;    // Reference to the painting (if needed for bounds)
    public Button zoomInButton;    // zoom in button
    public Button zoomOutButton;    // zoom out button
    public float panSpeed = 5f;    // Speed of panning
    public float panBoundaryY = 0.7f; // Vertical boundary for panning (Y-axis)
    public float panBoundaryZ = 0.85f; // Horizontal boundary for panning (Z-axis)
    private bool isZoomedIn = false; // Whether the camera is zoomed in or out
    private float zoomedOutX = 12f;  // Camera X position when zoomed out
    private float zoomedInX = 13.2f;   // Camera X position when zoomed in
    

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

        // update visibility of buttons
        UpdateButtonStates();
        
    }

    // Update button visibility based on zoom state
    private void UpdateButtonStates()
    {
        zoomInButton.gameObject.SetActive(!isZoomedIn); // Show zoom-in button if not zoomed in
        zoomOutButton.gameObject.SetActive(isZoomedIn); // Show zoom-out button if zoomed in
    }

    // Method to handle panning using arrow keys
    private void HandlePanning()
    {
        if (!isZoomedIn) return; // Only allow panning when zoomed in

        float vertical = Input.GetAxis("Vertical"); // Arrow keys for up/down (Y-axis)
        float horizontal = Input.GetAxis("Horizontal"); // Arrow keys for left/right (Z-axis)

        // Flip horizontal input for correct behavior
        horizontal = -horizontal;

        // Calculate new position with boundaries
        Vector3 newPosition = mainCamera.transform.position + new Vector3(0, vertical, horizontal) * panSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, painting.position.y - panBoundaryY, painting.position.y + panBoundaryY); // Limit Y movement
        newPosition.z = Mathf.Clamp(newPosition.z, painting.position.z - panBoundaryZ, painting.position.z + panBoundaryZ); // Limit Z movement

        // Apply the new position to the camera
        mainCamera.transform.position = newPosition;
    }
}
