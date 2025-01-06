using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragAndDropAnywhere : MonoBehaviour
{
    private Vector3 offset;  // The offset between the mouse position and the object's position

    // This method is called when the user clicks on the object
    void OnMouseDown()
    {
        // Calculate the offset from the mouse position to the object's position
        offset = transform.position - MouseWorldPosition();

        // Disable the collider to prevent other interactions while dragging (if needed)
        transform.GetComponent<Collider>().enabled = false;
    }

    // This method is called as the user drags the object
    void OnMouseDrag()
    {
        // Update the position of the object to follow the mouse
        transform.position = MouseWorldPosition() + offset;
    }

    // This method is called when the mouse is released
    void OnMouseUp()
    {
        // Re-enable the collider after the drag is completed
        transform.GetComponent<Collider>().enabled = true;

    }

    // This function gets the mouse position in world space
    Vector3 MouseWorldPosition()
    {
        // Get the mouse position in screen space
        var mouseScreenPos = Input.mousePosition;

        // Set the z distance of the mouse to the current object's z position to keep the object at the same depth
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        // Convert the screen position to world space
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
