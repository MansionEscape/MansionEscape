using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HiddenShapesManager : MonoBehaviour
{
    // Reference to the Input Action Asset
    public InputActionAsset inputActions;
    private InputAction mouseLeftClickAction;

    // Dictionary to map letters to shapes
    private Dictionary<GameObject, GameObject> letterToShapeMap;

    [SerializeField]
    private GameObject heartShape, triangleShape, circleShape; // Shapes
    [SerializeField]
    private GameObject heartLetter, triangleLetter, circleLetter; // Letters

    private void OnEnable()
    {
        // Assign mouse left-click action
        var actionMap = inputActions.FindActionMap("Mouse");
        mouseLeftClickAction = actionMap.FindAction("MouseLeftClick");
        mouseLeftClickAction.performed += OnMouseClick;
        mouseLeftClickAction.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe and disable actions
        mouseLeftClickAction.performed -= OnMouseClick;
        mouseLeftClickAction.Disable();
    }

    private void Start()
    {
        // Initialize the letter-to-shape mapping
        letterToShapeMap = new Dictionary<GameObject, GameObject>
        {
            { heartLetter, heartShape },
            { triangleLetter, triangleShape },
            { circleLetter, circleShape }
        };
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object is a shape or letter
            GameObject clickedObject = hit.collider.gameObject;

            // If a shape is clicked, hide it
            if (letterToShapeMap.ContainsValue(clickedObject))
            {
                clickedObject.SetActive(false);
            }

            // If a letter is clicked, toggle the corresponding shape's visibility
            if (letterToShapeMap.ContainsKey(clickedObject))
            {
                GameObject associatedShape = letterToShapeMap[clickedObject];
                associatedShape.SetActive(true);
            }
        }
    }
}
