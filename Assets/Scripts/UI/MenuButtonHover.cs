using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonHover :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Scale")]
    [SerializeField] private float hoverScale = 1.06f;
    [SerializeField] private float pressedScale = 0.98f;
    [SerializeField] private float animationSpeed = 12f;

    private Vector3 normalScale;
    private Vector3 targetScale;

    private bool isHovered;

    private void Awake()
    {
        normalScale = transform.localScale;
        targetScale = normalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            animationSpeed * Time.unscaledDeltaTime);
    }

    public void OnPointerEnter(
        PointerEventData eventData)
    {
        isHovered = true;

        targetScale =
            normalScale * hoverScale;
    }

    public void OnPointerExit(
        PointerEventData eventData)
    {
        isHovered = false;

        targetScale = normalScale;
    }

    public void OnPointerDown(
        PointerEventData eventData)
    {
        targetScale =
            normalScale * pressedScale;
    }

    public void OnPointerUp(
        PointerEventData eventData)
    {
        targetScale = isHovered
            ? normalScale * hoverScale
            : normalScale;
    }
}