using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject correctObject;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null && droppedObject == correctObject)
        {
            Debug.Log($"{droppedObject.name} dropped on {name}");

            // Snap the dropped object to the drop target position
            RectTransform droppedRectTransform = droppedObject.GetComponent<RectTransform>();
            RectTransform dropAreaRectTransform = GetComponent<RectTransform>();

            droppedRectTransform.anchoredPosition = dropAreaRectTransform.anchoredPosition;

            // Lock the object
            DragAndRotate draggable = droppedObject.GetComponent<DragAndRotate>();
            if (draggable != null)
            {
                draggable.LockObject();
            }
        }
        else
        {
            Debug.Log($"Incorrect object dropped on {name}");
        }
    }
}
