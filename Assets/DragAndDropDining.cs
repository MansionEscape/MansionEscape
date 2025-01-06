using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DragAndDropDining : MonoBehaviour
{
    [SerializeField] private InputAction press, screenPosition;

    public GameObject playerControl;
    public PlayerManager player;

    private Vector3 currentScreenPosition;
    private Camera camera;

    private bool isDragging = false;
    private GameObject selectedObject;

    public TMP_Text puzzleCompleted;
    private int counter = 0; // Count for completed items (plates + glasses + spoons)
    public Transform[] plateSnapPoints;
    public Transform[] glassSnapPoints;
    public Transform[] spoonSnapPoints;
    public float snapDistance = 0.5f;

    // Key-related fields
    public GameObject keyPrefab; // Assign your key prefab here in the Inspector
    public Transform keySpawnPoint; // Position where the key will appear
    private GameObject spawnedKey; // Reference to the spawned key
    private bool puzzleComplete = false; // Tracks if the puzzle is complete

    // Inventory
    public InventoryManager inventoryManager;
    public Item keyItem; // Reference to the key item to add to inventory

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
        if (puzzleComplete && IsClickedOnKey()) // Key clicked
        {
            AddKeyToInventory();
            Destroy(spawnedKey); // Remove the key from the scene after pickup
            player.data.ObjectivePuzzleTwoComplete = true;
            player.UpdatePlayer();
            puzzleCompleted.text = "Loading Mansion...";
            StartCoroutine(LoadMansion());
            return;
        }

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

    public IEnumerator LoadMansion()
    {
        yield return new WaitForSeconds(2);
        player.LoadPlayerGame();
        SceneManager.LoadScene("Mansion");
    }

    private void Update()
    {
        if (isDragging && selectedObject != null)
        {
            // Move the object to the mouse position, keeping the initial Y level
            Vector3 newPosition = worldPosition;
            newPosition.y = selectedObject.transform.position.y; // Maintain the original Y level
            selectedObject.transform.position = newPosition;
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

                // Check if all items (plates + glasses + spoons) are placed
                if (counter == 12) // 4 plates + 4 glasses + 4 spoons
                {
                    CompletePuzzle();
                }
            }
        }
    }

    private void CompletePuzzle()
    {
        puzzleCompleted.text = "Puzzle Completed! Look, a Key!";
        puzzleComplete = true;

        // Show the key (make it visible)
        if (spawnedKey != null)
        {
            spawnedKey.SetActive(true);
            
        }
    }

    private Transform GetSnapPoint(string itemName)
    {
        switch (itemName)
        {
            // Plates
            case "Plate 1":
                return plateSnapPoints[0];
            case "Plate 2":
                return plateSnapPoints[1];
            case "Plate 3":
                return plateSnapPoints[2];
            case "Plate 4":
                return plateSnapPoints[3];

            // Glasses
            case "Glass 1":
                return glassSnapPoints[0];
            case "Glass 2":
                return glassSnapPoints[1];
            case "Glass 3":
                return glassSnapPoints[2];
            case "Glass 4":
                return glassSnapPoints[3];

            // Spoons
            case "Spoon 1":
                return spoonSnapPoints[0];
            case "Spoon 2":
                return spoonSnapPoints[1];
            case "Spoon 3":
                return spoonSnapPoints[2];
            case "Spoon 4":
                return spoonSnapPoints[3];

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

    private bool IsClickedOnKey()
    {
        Ray ray = camera.ScreenPointToRay(currentScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider != null && hit.collider.gameObject == spawnedKey;
        }
        return false;
    }

    private void AddKeyToInventory()
    {
        if (playerControl != null && keyItem != null)
        {
            player.AddItemFromPuzzle(keyItem);
            Debug.Log("Key added to inventory!");
        }
    }

    private Vector3 worldPosition
    {
        get
        {
            float z = camera.WorldToScreenPoint(transform.position).z;
            return camera.ScreenToWorldPoint(new Vector3(currentScreenPosition.x, currentScreenPosition.y, z));
        }
    }

    private void Start()
    {
        playerControl = GameObject.FindWithTag("PlayerManager");
        player = playerControl.GetComponent<PlayerManager>();

        // Spawn the key initially but make it invisible
        if (keyPrefab != null && keySpawnPoint != null)
        {
            spawnedKey = Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
            spawnedKey.SetActive(false); // Hide the key until the puzzle is completed
        }
    }
}
