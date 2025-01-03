using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragDropDining : MonoBehaviour
{
    Vector3 offset;

    // Define drop area tags for different drop targets
    public string destinationTag;
    public PlacementManager placementManager;

    // Define offsets for the objects, configurable in Inspector
    public List<OffsetMapping> objectOffsets;

    // Dictionary for quick lookup of offsets
    private Dictionary<string, Vector3> offsetsDictionary = new Dictionary<string, Vector3>();

    void Start()
    {
        // Initialize the offsets dictionary
        foreach (var mapping in objectOffsets)
        {
            offsetsDictionary[mapping.objectTag] = mapping.offset;
        }
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            {
                Vector3 objectOffset = Vector3.zero;

                // Get the offset for this object's tag
                if (offsetsDictionary.TryGetValue(gameObject.tag, out objectOffset))
                {
                    transform.position = hitInfo.transform.position + objectOffset;
                }
                else
                {
                    transform.position = hitInfo.transform.position; // Default to center
                }

                if (placementManager != null)
                {
                    placementManager.RegisterPlacement(gameObject.tag);
                }
            }
        }

        transform.GetComponent<Collider>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    [System.Serializable]
    public class OffsetMapping
    {
        public string objectTag;
        public Vector3 offset;
    }
}
