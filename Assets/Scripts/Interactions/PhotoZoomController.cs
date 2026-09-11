using UnityEngine;
using UnityEngine.EventSystems;

public class PhotoZoomController :
    MonoBehaviour,
    IScrollHandler,
    IBeginDragHandler,
    IDragHandler
{
    [Header("References")]
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform zoomContent;

    [Header("Zoom")]
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 4f;
    [SerializeField] private float zoomStep = 0.3f;

    [Header("Drag")]
    [SerializeField] private float dragSpeed = 1f;

    private float currentScale = 1f;
    private Vector2 previousPointerPosition;

    private void OnEnable()
    {
        ResetZoom();
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (viewport == null || zoomContent == null)
        {
            return;
        }

        float scroll = eventData.scrollDelta.y;

        if (Mathf.Approximately(scroll, 0f))
        {
            return;
        }

        float newScale = Mathf.Clamp(
            currentScale + scroll * zoomStep,
            minScale,
            maxScale
        );

        if (Mathf.Approximately(newScale, currentScale))
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            viewport,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 pointerPosition
        );

        float scaleRatio = newScale / currentScale;

        Vector2 currentPosition = zoomContent.anchoredPosition;

        Vector2 newPosition =
            pointerPosition -
            (pointerPosition - currentPosition) * scaleRatio;

        currentScale = newScale;

        zoomContent.localScale =
            Vector3.one * currentScale;

        zoomContent.anchoredPosition = newPosition;

        ClampPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousPointerPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentScale <= minScale)
        {
            return;
        }

        Vector2 delta =
            eventData.position - previousPointerPosition;

        zoomContent.anchoredPosition +=
            delta * dragSpeed;

        previousPointerPosition =
            eventData.position;

        ClampPosition();
    }

    public void ResetZoom()
    {
        currentScale = minScale;

        if (zoomContent == null)
        {
            return;
        }

        zoomContent.localScale =
            Vector3.one * currentScale;

        zoomContent.anchoredPosition =
            Vector2.zero;
    }

    private void ClampPosition()
    {
        if (viewport == null || zoomContent == null)
        {
            return;
        }

        float scaledWidth =
            zoomContent.rect.width * currentScale;

        float scaledHeight =
            zoomContent.rect.height * currentScale;

        float maxX = Mathf.Max(
            0f,
            (scaledWidth - viewport.rect.width) / 2f
        );

        float maxY = Mathf.Max(
            0f,
            (scaledHeight - viewport.rect.height) / 2f
        );

        Vector2 position =
            zoomContent.anchoredPosition;

        position.x =
            Mathf.Clamp(position.x, -maxX, maxX);

        position.y =
            Mathf.Clamp(position.y, -maxY, maxY);

        zoomContent.anchoredPosition =
            position;
    }
}