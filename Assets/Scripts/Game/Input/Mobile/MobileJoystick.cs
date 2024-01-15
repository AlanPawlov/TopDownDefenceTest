using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Input.Mobile
{
    public class MobileJoystick : MonoBehaviour, IDraggable
    {
        [SerializeField]
        private RectTransform _thumbTransform;
        [SerializeField]
        private RectTransform _joystickTransform;
        [SerializeField]
        private float _valueMultiplier = 1f;
        [SerializeField]
        private float _movementAreaRadius = 75f;
        [SerializeField]
        private DragHelper _dragHelper;
        private Vector2 _pointerInitialPosition;
        private float _overMovementAreaRadius;
        private float _movementAreaRadiusSqr;
        private Vector2 _value;
        public event Action<Vector2> OnJoystickValueChanged;
        public Vector2 Value => _value;

        private void Awake()
        {
            _overMovementAreaRadius = 1f / _movementAreaRadius;
            _movementAreaRadiusSqr = _movementAreaRadius * _movementAreaRadius;
            _thumbTransform.localPosition = Vector3.zero;
            _dragHelper.Listener = this;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, eventData.position, eventData.pressEventCamera, out _pointerInitialPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, eventData.position, eventData.pressEventCamera, out pointerPos);
            Vector2 direction = pointerPos - _pointerInitialPosition;
            if (direction.sqrMagnitude > _movementAreaRadiusSqr)
            {
                Vector2 directionNormalized = direction.normalized * _movementAreaRadius;
                direction = directionNormalized;
            }

            _value = direction * _overMovementAreaRadius * _valueMultiplier;
            _thumbTransform.localPosition = direction;
            OnJoystickValueChanged?.Invoke(_value);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _value = Vector2.zero;
            _thumbTransform.localPosition = Vector3.zero;
            OnJoystickValueChanged?.Invoke(_value);
        }
    }
}