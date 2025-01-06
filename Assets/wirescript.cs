using UnityEngine;
using UnityEngine.EventSystems;

public class WireScript : MonoBehaviour, IDragHandler
{

    [SerializeField] private RectTransform movingPart;  // Reference to the 'moving' GameObject

    private void Awake()
    {
        if (movingPart == null)
        {
            Debug.LogError("Moving part is not assigned in WireScript. Please assign it in the Inspector.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (movingPart != null)
        {
            // Update the position of the moving part to follow the mouse
            movingPart.anchoredPosition += eventData.delta;
        }

        
    }
}