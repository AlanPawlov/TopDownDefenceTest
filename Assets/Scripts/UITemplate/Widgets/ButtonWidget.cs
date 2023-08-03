using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class ButtonWidget : BaseUIElement
    {
        [SerializeField] protected Button _button;
        [SerializeField] protected Image _targetImage;
        [SerializeField] protected Image _lockImage;
        [SerializeField] protected GameObject _glowVFX;
        [SerializeField] protected Image _targetImageBackground;
        [SerializeField] private GameObject _highlightVFX;
        [SerializeField] private GameObject _questMarker;
        [SerializeField] protected TMP_Text _buttonText;
        [SerializeField] private Image _inactiveTint;
        [SerializeField] private AudioClip _clickSFX;
        private Sprite _originSprite;

        public bool Interactable
        {
            get => _button.interactable;
            set => _button.interactable = value;
        }

        public override async Task Init()
        {
            await base.Init();
            RectTransform.anchorMin = Vector2.zero;
            RectTransform.anchorMax = Vector2.one;
            RectTransform.offsetMax = Vector2.zero;
            RectTransform.offsetMin = Vector2.zero;
            _originSprite = _targetImage.sprite;
        }

        public override void Uninit()
        {
            if (_highlightVFX != null)
            {
                _highlightVFX.SetActive(false);
            }

            if (_questMarker != null)
            {
                _questMarker.SetActive(false);
            }

            if (_inactiveTint != null)
            {
                _inactiveTint.gameObject.SetActive(false);
            }

            if (_buttonText != null)
            {
                _buttonText.text = default;
            }

            _targetImage.sprite = _originSprite;
            _button.interactable = true;
            _button.onClick.RemoveAllListeners();
            base.Uninit();
        }


        public void SetData(Sprite sprite, Action action)
        {
            _button.interactable = true;
            _targetImage.sprite = sprite;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action());
        }

        public void SetData(Action action)
        {
            _button.interactable = true;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action());
        }

        public void SetData(Sprite sprite)
        {
            _button.interactable = false;
            _targetImage.sprite = sprite;
        }

        public void SetData(string newText)
        {
            if (_buttonText == null)
            {
                return;
            }

            _buttonText.text = newText;
        }

        public bool Locked
        {
            get => _lockImage.gameObject.activeSelf;
            set => _lockImage?.gameObject.SetActive(value);
        }

        public bool Glow
        {
            get => _glowVFX.activeSelf;
            set => _glowVFX?.SetActive(value);
        }

        public Sprite ImageBackground
        {
            get => _targetImageBackground.sprite;
            set => _targetImageBackground.sprite = value;
        }

        public bool VisualInactive
        {
            get
            {
                if (_inactiveTint == null)
                {
                    return false;
                }

                return _inactiveTint.gameObject.activeSelf;
            }
            set
            {
                if (_inactiveTint == null)
                {
                    return;
                }

                _inactiveTint.gameObject.SetActive(value);
            }
        }

        public void Callback(Sprite value)
        {
            _targetImage.sprite = value;
        }

        public void Click()
        {
            _button.onClick.Invoke();
        }

        public Vector3 GetScreenPosition()
        {
            return transform.position;
        }
    }
}