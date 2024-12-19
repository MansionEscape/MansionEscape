using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrabbableScript : MonoBehaviour
{
    // Serializes input action fields for editing in the inspector
    [SerializeField] private InputAction press, press2, screenPosition;

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

    // Correct rotation values for each puzzle piece
    private Dictionary<string, Quaternion> correctRotations = new Dictionary<string, Quaternion>
    {
        { "Puzzle Piece 1", Quaternion.Euler(0, 0, 0) },
        { "Puzzle Piece 2", Quaternion.Euler(0, 0, 0) },
        { "Puzzle Piece 3", Quaternion.Euler(0, 0, 0) },
        { "Puzzle Piece 4", Quaternion.Euler(0, 0, 0) }
    };

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
        press2.Enable(); // The right button
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

        // On right click performed
        press2.performed += _ => 
        { 
            if (selectedObject != null) RotateSelectedObject(); 
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

    // Function to rotate the selected object when right click is pressed
    private void RotateSelectedObject()
    {
        selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
            selectedObject.transform.rotation.eulerAngles.x,
            selectedObject.transform.rotation.eulerAngles.y + 90f,
            selectedObject.transform.rotation.eulerAngles.z));
    }

    // Snaps the object to its given snap point if it's close enough
    private void SnapObjectToDesignatedPoint()
    {
        string selectedName = selectedObject.name;

        if (correctRotations.ContainsKey(selectedName) && IsRotationCorrect(selectedObject.transform.rotation, correctRotations[selectedName]))
        {
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

                    // Set the secondary texture when the piece is snapped
                    selectedObject.GetComponent<Renderer>().material.SetTexture("_DetailAlbedoMap", secondaryTexture);

                    // Check if all pieces are placed
                    if (counter == 4)
                    {
                        puzzleCompleted.text = "Puzzle Completed!";
                    }
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
    private Transform GetSnapPoint(string puzzlePieceName)
    {
        switch (puzzlePieceName)
        {
            case "Puzzle Piece 1":
                return snapPoints[0];
            case "Puzzle Piece 2":
                return snapPoints[1];
            case "Puzzle Piece 3":
                return snapPoints[2];
            case "Puzzle Piece 4":
                return snapPoints[3];
            default:
                return null;
        }
    }
}
