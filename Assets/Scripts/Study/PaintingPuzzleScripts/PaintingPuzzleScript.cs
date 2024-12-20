using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public static class GameState
{
    public static bool isPuzzleCompleted = false;
}


public class PaintingPuzzleScript : MonoBehaviour
{
    // Serializes input action fields for editing in the inspector
    [SerializeField] private InputAction press, screenPosition;

    // Current screen position vector
    private Vector3 currentScreenPosition;

    // Camera object for raycasting
    private Camera camera;

    // To check if the object is being dragged or not
    private bool isDragging = false;

    private GameObject selectedObject;
    public Text scoreText;
    public TMP_Text puzzleCompleted;
    private int counter = 0;
    public Texture secondaryTexture;
    public Transform[] snapPoints;
    public float snapDistance = 0.5f;

    // Get the world position based on the screen position
    private Vector3 worldPosition
    {
        get
        {
            float z = camera.WorldToScreenPoint(transform.position).z;
            return camera.ScreenToWorldPoint(currentScreenPosition + new Vector3(0, 0, z));
        }
    }

    // Checks if the mouse is clicking on an object
    private bool isClickedOn
    {
        get
        {
            Ray ray = camera.ScreenPointToRay(currentScreenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                selectedObject = hit.collider.CompareTag("drag") ? hit.collider.gameObject : null;
                return selectedObject != null;
            }
            return false;
        }
    }

    // Awake function to initialize camera and inputs
    public void Awake()
    {
        camera = Camera.main;

        // Enable input actions
        press.Enable(); // The left button
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

    // Handling left click
    private void OnLeftClick()
    {
        if (!isDragging)
        {
            if (isClickedOn) // Object clicked on
            {
                isDragging = true; // Start dragging
            }
        }
        else // If currently dragging, stop dragging and attempt to snap
        {
            isDragging = false;
            SnapObjectToDesignatedPoint();
            selectedObject = null; // Deselect the object
        }
    }

    // Update method to move the object when it's being dragged
    private void Update()
    {
        if (isDragging && selectedObject != null)
        {
            Vector3 offset = worldPosition;
            selectedObject.transform.position = offset;
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

                counter += 1;
                scoreText.text = "Score: " + counter;

                // Check if all pieces are placed
                if (counter == 4)
                {
                    puzzleCompleted.text = "Puzzle Completed!";
                    GameState.isPuzzleCompleted = true;
                }
            }
        }
    }

    // Check if the rotation of the object matches the target rotation
    private bool IsRotationCorrect(Quaternion objectRotation, Quaternion targetRotation)
    {
        return Quaternion.Angle(objectRotation, targetRotation) < 5f;
    }

    // Get the corresponding snap point based on the puzzle piece name
    private Transform GetSnapPoint(string paintingName)
    {
        switch (paintingName)
        {
            case "Painting 1":
                return snapPoints[0];
            case "Painting 2":
                return snapPoints[1];
            case "Painting 3":
                return snapPoints[2];
            case "Painting 4":
                return snapPoints[3];
            default:
                return null;
        }
    }
}
