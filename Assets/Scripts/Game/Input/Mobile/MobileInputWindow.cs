using Common.UITemplate;
using UnityEngine;
using Zenject;

namespace Game.Input.Mobile
{
    public class MobileInputWindow : InterfaceWindow
    {
        [SerializeField]
        private MobileJoystick _movementJoystick;
        private IInputService _inputService;
        
        public Vector2 Movement => _movementJoystick.Value;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
    }
}