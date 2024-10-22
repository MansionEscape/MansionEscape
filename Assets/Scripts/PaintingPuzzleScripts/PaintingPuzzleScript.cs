using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzleScript : MonoBehaviour
{
    private GameObject selectedObject;
    //public Texture secondaryTexture;
    public Transform[] snapPoints;
    public float snapDistance = 0.5f;

    // To track which puzzle pieces are locked in place
    private HashSet<GameObject> lockedPieces = new HashSet<GameObject>();

    private Dictionary<string, Quaternion> correctRotations = new Dictionary<string, Quaternion>
    {
        { "Puzzle Piece 1", Quaternion.Euler(0, 0, 0) },
        { "Puzzle Piece 2", Quaternion.Euler(0, 0, 0) },
        { "Puzzle Piece 3", Quaternion.Euler(0, 0, 0) },
        { "Puzzle Piece 4", Quaternion.Euler(0, 0, 0) }
    };

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRayForPaintingPuzzle();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag") || lockedPieces.Contains(hit.collider.gameObject))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);

                SnapObjectToPuzzlePoint();
                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if (selectedObject != null && !lockedPieces.Contains(selectedObject))
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);

            if (Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
    }

    private void SnapObjectToPuzzlePoint()
    {
        string selectedName = selectedObject.name;

        if (correctRotations.ContainsKey(selectedName) && IsRotationCorrectForPuzzle(selectedObject.transform.rotation, correctRotations[selectedName]))
        {
            Transform snapPoint = GetPaintingPuzzleSnapPoint(selectedName);

            if (snapPoint != null)
            {
                float distance = Vector3.Distance(selectedObject.transform.position, snapPoint.position);

                if (distance <= snapDistance)
                {
                    // Snap the puzzle piece into position
                    selectedObject.transform.position = snapPoint.position;
                    selectedObject.transform.rotation = correctRotations[selectedName];  // Align the rotation perfectly
                    LockPuzzlePiece(selectedObject);  // Lock the piece in place
                }
            }
        }
    }

    private void LockPuzzlePiece(GameObject puzzlePiece)
    {
        // Lock the puzzle piece by disabling its collider and preventing further interaction
        puzzlePiece.GetComponent<Collider>().enabled = false;

        // Optionally disable the Rigidbody if it exists (assuming physics isn't needed anymore)
        Rigidbody rb = puzzlePiece.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // Prevents further movement due to physics
        }

        // Add the puzzle piece to the list of locked pieces
        lockedPieces.Add(puzzlePiece);
    }

    private bool IsRotationCorrectForPuzzle(Quaternion objectRotation, Quaternion targetRotation)
    {
        return Quaternion.Angle(objectRotation, targetRotation) < 5f;
    }

    private Transform GetPaintingPuzzleSnapPoint(string puzzlePieceName)
    {
        switch (puzzlePieceName)
        {
            case "Puzzle Piece 1":
                return snapPoints[0]; // Position 1
            case "Puzzle Piece 2":
                return snapPoints[1]; // Position 2
            case "Puzzle Piece 3":
                return snapPoints[2]; // Position 3
            case "Puzzle Piece 4":
                return snapPoints[3]; // Position 4
            default:
                return null; // No matching snap point
        }
    }

    private RaycastHit CastRayForPaintingPuzzle()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
