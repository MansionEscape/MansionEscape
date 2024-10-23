using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Script tracks mouse position and using mouse down and up 
// allows us to then drag and drop books within the bookshelf puzzle.
//Attach this script to the book objects.
public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition; // Creates a new Vector3 object called mouseposition

    public InputDevice mouse = Mouse.current;

    private Vector3 GetMousePos()
    {
        // transforms the poisition from world space to screen space and uses that to move the object.
        return Camera.main.WorldToScreenPoint(transform.position);
    }

     
    private void OnMouseDown()
    {
        mousePosition = (Input.mousePosition - GetMousePos());
    }

    // When the mouse is moving whilst the button is down
    private void OnMouseDrag()
    {
        // transforms the poisition from world space to screen space and uses that to move the object.
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

  
}
