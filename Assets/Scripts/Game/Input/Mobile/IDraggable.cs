using UnityEngine.EventSystems;

namespace Game.Input.Mobile
{
    public interface IDraggable
    {
        void OnPointerDown(PointerEventData eventData);
        void OnDrag(PointerEventData eventData);
        void OnPointerUp(PointerEventData eventData);
    }
}