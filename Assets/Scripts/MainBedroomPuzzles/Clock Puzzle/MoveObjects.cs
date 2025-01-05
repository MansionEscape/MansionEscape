using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MoveObjects : MonoBehaviour
{
    public PuzzleController puzzle;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 originalPos;

    public GameObject targetHand;

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
        if (inTriggerZone)
        {
            Renderer.material = highlightMaterial;
        }

    }
    private void OnMouseOver()
    {
        Renderer.material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        Renderer.material = originalMaterial;
    }
    void OnMouseDown()
    { 
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }

    void OnMouseDrag()
    {
        Renderer.material = originalMaterial;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;


    }

    void OnMouseUp()
    {
        if(inTriggerZone)
        {
            SetInPlace();
            targetHand.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            transform.position = originalPos;
            

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
    //public GameObject targethand;

    //public Vector3 handDefaultLocation;
    //public Quaternion handDefaultRotation;

    //public Quaternion movementRotation;
    //public Vector3 movementPosition;

    //public bool mouseOverObject;

    //// Start is called before the first frame update
    //void Start()
    //{

    //    handDefaultLocation = transform.position;
    //    handDefaultRotation = transform.rotation;


    //}



    //public void OnTriggerEnter(Collider other)
    //{
    //    mouseOverObject = true;
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    mouseOverObject = false;

    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    transform.position = targethand.transform.position;
    //    transform.rotation = targethand.transform.rotation;

    //    transform.position = Input.mousePosition;

    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{

    //    transform.position = handDefaultLocation;
    //    transform.rotation = handDefaultRotation;
    //}
}
