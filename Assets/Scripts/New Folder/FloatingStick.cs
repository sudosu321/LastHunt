using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.EventSystems;

public class FloatingStick : OnScreenStick, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startPos;
    private RectTransform rectTransform;

    protected override void OnEnable()
    {
        base.OnEnable();
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    public new void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );
        rectTransform.anchoredPosition = localPoint;
        base.OnPointerDown(eventData);
    }

    // ← NO override keyword, implementing IPointerUpHandler directly
    public new void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        rectTransform.anchoredPosition = startPos;
    }
}