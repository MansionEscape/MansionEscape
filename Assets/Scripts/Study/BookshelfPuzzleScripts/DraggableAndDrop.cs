using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDropBooks : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPosition;

    private Vector3 currentScreenPosition;
    private Camera camera;

    private bool isDragging = false;
    private GameObject selectedObject;

    public Transform[] bookSnapPoints; // Array of snap points corresponding to each book
    public float snapDistance = 0.5f; // Distance within which snapping occurs

    private int correctPlacementCounter = 0; // Counter for correctly placed books
    public int totalBooks = 16; // Total number of books

    // Colors for selection and deselection
    public Color selectedColor = Color.yellow;
    private Color originalColor;

    public void Awake()
    {
        camera = Camera.main;

        // Enable input actions
        press.Enable();
        screenPosition.Enable();

        // Capture the screen position input
        screenPosition.performed += context =>
        {
            currentScreenPosition = context.ReadValue<Vector2>();
        };

        // On left click performed
        press.performed += _ =>
        {
            OnLeftClick();
        };
    }

    private void OnLeftClick()
    {
        if (!isDragging)
        {
            if (IsClickedOn()) // Object clicked on
            {
                isDragging = true; // Start dragging

                // Change the object's color to the selected color
                Renderer objectRenderer = selectedObject.GetComponent<Renderer>();
                if (objectRenderer != null)
                {
                    originalColor = objectRenderer.material.color;
                    objectRenderer.material.color = selectedColor;
                }
            }
        }
        else // If currently dragging, stop dragging and attempt to snap
        {
            isDragging = false;

            // Revert the object's color to its original color
            Renderer objectRenderer = selectedObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor;
            }

            SnapObjectToDesignatedPoint();
            selectedObject = null; // Deselect the object
        }
    }

    private void Update()
    {
        if (isDragging && selectedObject != null)
        {
            // Move the object to the mouse position, keeping the initial Z level
            Vector3 worldPosition = camera.ScreenToWorldPoint(new Vector3(currentScreenPosition.x, currentScreenPosition.y, camera.WorldToScreenPoint(selectedObject.transform.position).z));
            worldPosition.z = selectedObject.transform.position.z; // Maintain the original Z level
            selectedObject.transform.position = worldPosition;
        }
    }

    private void SnapObjectToDesignatedPoint()
    {
        string selectedName = selectedObject.name;

        Transform snapPoint = GetSnapPoint(selectedName);

        if (snapPoint != null)
        {
            float distance = Vector3.Distance(selectedObject.transform.position, snapPoint.position);

            if (distance <= snapDistance)
            {
                selectedObject.transform.position = snapPoint.position;
                selectedObject.GetComponent<Collider>().enabled = false;

                correctPlacementCounter += 1;

                // Check if all books are placed correctly
                if (correctPlacementCounter == totalBooks)
                {
                    Debug.Log("All books are in the right place!");
                }
            }
        }
    }

    private Transform GetSnapPoint(string itemName)
    {
        switch (itemName)
        {
            case "BookN":
                return bookSnapPoints[0];
            case "BookT":
                return bookSnapPoints[1];
            case "BookE":
                return bookSnapPoints[2];
            case "BookR":
                return bookSnapPoints[3];
            case "BookC":
                return bookSnapPoints[4];
            case "BookO":
                return bookSnapPoints[5];
            case "BookE2":
                return bookSnapPoints[6];
            case "BookB":
                return bookSnapPoints[7];
            case "BookA":
                return bookSnapPoints[8];
            case "BookC2":
                return bookSnapPoints[9];
            case "BookK":
                return bookSnapPoints[10];
            case "Book-":
                return bookSnapPoints[11];
            case "BookW":
                return bookSnapPoints[12];
            case "BookR2":
                return bookSnapPoints[13];
            case "BookD":
                return bookSnapPoints[14];
            case "BookS":
                return bookSnapPoints[15];
            default:
                return null;
        }
    }

    private bool IsClickedOn()
    {
        Ray ray = camera.ScreenPointToRay(currentScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("drag"))
            {
                selectedObject = hit.collider.gameObject;
                return true;
            }
        }
        return false;
    }
}