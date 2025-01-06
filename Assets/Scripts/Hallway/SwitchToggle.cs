using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchToggle : MonoBehaviour
{
    private Animator switchAnimator;       // Animator for the switch
    public GameObject Light;              // Reference to the Light GameObject

    private Camera mainCamera;            // Reference to the main camera
    public InputActionAsset inputActions; // Reference to the Input Action Asset
    private InputAction mouseClickAction; // Mouse click action

    private void Awake()
    {
        // Get Animator component attached to this GameObject
        switchAnimator = GetComponent<Animator>();

        // Ensure Light starts in the correct state
        if (!switchAnimator.GetBool("IsOn"))
        {
            Light.SetActive(false);
        }

        // Get main camera
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Find the Mouse Left Click action in the Input Action Asset
        var actionMap = inputActions.FindActionMap("Mouse");
        mouseClickAction = actionMap.FindAction("MouseLeftClick");

        // Subscribe to the mouse click action
        mouseClickAction.performed += OnMouseClick;
        mouseClickAction.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from the mouse click action
        mouseClickAction.performed -= OnMouseClick;
        mouseClickAction.Disable();
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        // Cast a ray from the mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Check if the clicked object is this switch
            if (hit.collider.gameObject == gameObject)
            {
                ToggleSwitch();
            }
        }
    }

    private void ToggleSwitch()
    {
        // Retrieve current value of IsOn parameter
        bool isOn = switchAnimator.GetBool("IsOn");

        // Toggle IsOn parameter
        switchAnimator.SetBool("IsOn", !isOn);

        // Toggle Light based on IsOn value
        Light.SetActive(!isOn);
    }
}
