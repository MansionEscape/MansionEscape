using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{
    private GameObject selectedObject;
    //public Texture secondaryTexture;
    public Transform[] snapPoints;
    public float snapDistance = 0.5f;

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
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
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

                SnapObjectToDesignatedPoint();
                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if (selectedObject != null)
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

                }
            }

            //if (secondaryTexture != null)
            //{
            //    selectedObject.GetComponent<Renderer>().material.SetTexture("_DetailAlbedoMap", secondaryTexture);
            //}
        }
    }

    private bool IsRotationCorrect(Quaternion objectRotation, Quaternion targetRotation)
    {
        return Quaternion.Angle(objectRotation, targetRotation) < 5f; 
    }

    private Transform GetSnapPoint(string puzzlePieceName)
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


    private RaycastHit CastRay()
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
