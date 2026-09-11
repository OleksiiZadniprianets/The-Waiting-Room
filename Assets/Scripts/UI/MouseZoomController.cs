using UnityEngine;

public class MouseZoomController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform zoomContent;
    [SerializeField] private Canvas canvas;

    [Header("Zoom Settings")]
    [SerializeField] private float minimumZoom = 1f;
    [SerializeField] private float maximumZoom = 2.5f;
    [SerializeField] private float zoomStep = 0.25f;
    [SerializeField] private float zoomSmoothness = 12f;

    private float currentZoom = 1f;
    private float targetZoom = 1f;

    private Vector2 currentPosition;
    private Vector2 targetPosition;
    private Camera canvasCamera;

    private void Awake()
    {
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            canvasCamera = canvas.worldCamera;
        }

        currentZoom = zoomContent.localScale.x;
        targetZoom = currentZoom;

        currentPosition = zoomContent.anchoredPosition;
        targetPosition = currentPosition;
    }

    private void Update()
    {
        ReadZoomInput();
        ApplySmoothZoom();
    }

    private void ReadZoomInput()
    {
        Vector2 mousePosition = Input.mousePosition;

        if (!RectTransformUtility.RectangleContainsScreenPoint(
                viewport,
                mousePosition,
                canvasCamera))
        {
            return;
        }

        float scrollValue = Input.mouseScrollDelta.y;

        if (Mathf.Approximately(scrollValue, 0f))
        {
            return;
        }

        float previousTargetZoom = targetZoom;

        targetZoom = Mathf.Clamp(
            targetZoom + Mathf.Sign(scrollValue) * zoomStep,
            minimumZoom,
            maximumZoom);

        if (Mathf.Approximately(previousTargetZoom, targetZoom))
        {
            return;
        }

        CalculateTargetPosition(
            mousePosition,
            previousTargetZoom,
            targetZoom);
    }

    private void CalculateTargetPosition(
        Vector2 mouseScreenPosition,
        float previousZoom,
        float newZoom)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                viewport,
                mouseScreenPosition,
                canvasCamera,
                out Vector2 cursorPositionInViewport))
        {
            return;
        }

        float zoomRatio = newZoom / previousZoom;

        targetPosition =
            cursorPositionInViewport -
            (cursorPositionInViewport - targetPosition) * zoomRatio;

        targetPosition = ClampPosition(
            targetPosition,
            newZoom);
    }

    private void ApplySmoothZoom()
    {
        float interpolation =
            1f - Mathf.Exp(-zoomSmoothness * Time.unscaledDeltaTime);

        currentZoom = Mathf.Lerp(
            currentZoom,
            targetZoom,
            interpolation);

        currentPosition = Vector2.Lerp(
            currentPosition,
            targetPosition,
            interpolation);

        zoomContent.localScale =
            Vector3.one * currentZoom;

        zoomContent.anchoredPosition =
            ClampPosition(currentPosition, currentZoom);
    }

    private Vector2 ClampPosition(
        Vector2 position,
        float zoom)
    {
        Rect viewportRect = viewport.rect;
        Rect contentRect = zoomContent.rect;

        float scaledWidth = contentRect.width * zoom;
        float scaledHeight = contentRect.height * zoom;

        float maximumX = Mathf.Max(
            0f,
            (scaledWidth - viewportRect.width) * 0.5f);

        float maximumY = Mathf.Max(
            0f,
            (scaledHeight - viewportRect.height) * 0.5f);

        position.x = Mathf.Clamp(
            position.x,
            -maximumX,
            maximumX);

        position.y = Mathf.Clamp(
            position.y,
            -maximumY,
            maximumY);

        return position;
    }
}