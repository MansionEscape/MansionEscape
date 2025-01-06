using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class MoveObjects : MonoBehaviour
{
    public PuzzleController puzzle;
    //Serialises input action fields that we can edit in the inspector
    [SerializeField] private InputAction press, screenPosition;

    //new vector 3 for the current screen position
    private Vector3 currentScreenPosition;

    // Camera object for point to view, raycast etc
    private Camera camera;

    //To check if the object is being dragged or not
    private bool beingDragged;
 

    public GameObject targetHand;

    private Vector3 originalPos;

    public Renderer Renderer;

    public string hand;

    public bool inTriggerZone;

    public Material originalMaterial;
    public Material highlightMaterial;
    public Material targetMaterial;

    void Start()
    {
        originalPos = transform.position;


        Renderer = GetComponent<Renderer>();


        Debug.Log("OriginalPos: " + originalPos);

    }

    void Update()
    {
        if (inTriggerZone && !beingDragged)
        {
            SetInPlace();
            targetHand.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = originalPos;


        }

    }    //Read only function that gets the world position.
    private Vector3 worldPosition
    {
        get
        {
            //returns the z position of the object position
            float z = camera.WorldToScreenPoint(transform.position).z;
            //returns it as part of the camera to world point with a new vector linking the z position
            return camera.ScreenToWorldPoint(currentScreenPosition + new Vector3(0, 0, z));
        }
    }

    //read only bool that checks if the object has been clicked on.
    private bool isClickedOn
    {
        get
        {
            Ray ray = camera.ScreenPointToRay(currentScreenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    //Before start is called set the objects and enable the actions
    public void Awake()
    {
        camera = Camera.main;
        press.Enable();
        screenPosition.Enable();
        screenPosition.performed += context => { currentScreenPosition = context.ReadValue<Vector2>(); };
        press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };
        press.canceled += _ => { beingDragged = false; };
    }

    //When object is being dragged
    private IEnumerator Drag()
    {

        beingDragged = true;

        Vector3 offset = transform.position - worldPosition;

        while (beingDragged)
        {
            transform.position = worldPosition + offset;
            yield return null;
        }
    }

    public void SetInPlace()
    {
        if (hand == "small")
        {
            puzzle.smallHandInPlace = true;
        }
        else if (hand == "large")
        {
            puzzle.largeHandInPlace = true;
        }
    }
}

