using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndRotate : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool isLocked = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked)
        {
            Debug.Log($"{name} is locked and cannot be dragged.");
            return;
        }

        Debug.Log($"Begin dragging {name}");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        Debug.Log($"Dragging {name}");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isLocked) return;

        Debug.Log($"End dragging {name}");
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLocked) return;

        Debug.Log($"Rotating {name}");
        rectTransform.Rotate(0, 0, 90);
    }

    public void LockObject()
    {
        Debug.Log($"{name} is now locked.");
        isLocked = true;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
