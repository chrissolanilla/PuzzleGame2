using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeHandle : MonoBehaviour, IDragHandler
{
    public RectTransform target;

    public void OnDrag(PointerEventData eventData)
    {
        if (target == null) return;

        Vector2 sizeDelta = target.sizeDelta;
        sizeDelta += new Vector2(eventData.delta.x, -eventData.delta.y);
        sizeDelta.x = Mathf.Max(50, sizeDelta.x);
        sizeDelta.y = Mathf.Max(50, sizeDelta.y);
        target.sizeDelta = sizeDelta;
    }
}
