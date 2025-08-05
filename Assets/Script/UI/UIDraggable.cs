using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIDraggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform == null)
            return;
        rectTransform.anchoredPosition = Mouse.current.position.ReadValue() - offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (rectTransform == null)
            return;
        offset = Mouse.current.position.ReadValue() - rectTransform.anchoredPosition;
    }
}
