using Common.UITemplate;
using UnityEngine;

namespace Game.Input.Mobile
{
    public class MobileInputService : InputService
    {
        private readonly UIManager _uiManager;
        private MobileInputWindow _mobileInputWindow;
        private bool _isInited;

        public MobileInputService(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public override async void Init()
        {
            _mobileInputWindow =
                await _uiManager.CreateGlobalWindow<MobileInputWindow>(UIResourceMap.WindowMap.MobileInputWindow);
            _isInited = true;
        }

        public override Vector2 Movement
        {
            get
            {
                if (!_isInited)
                    return Vector2.zero;

                return _mobileInputWindow.Movement;
            }
        }
    }
}