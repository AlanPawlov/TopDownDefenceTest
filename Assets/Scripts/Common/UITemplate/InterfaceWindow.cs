using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UITemplate
{
    public class InterfaceWindow : BaseUIElement
    {
        [SerializeField] protected Button closeButton;
        [SerializeField] protected RectTransform _selfRect;

        public override async Task Init()
        {
            await base.Init();
            closeButton?.onClick.AddListener(Close);
        }

        public override void Uninit()
        {
            closeButton?.onClick.RemoveAllListeners();
            base.Uninit();
        }


        public virtual void Close()
        {
            Uninit();
        }

        public virtual void ShowWindow(bool state)
        {
            transform.SetAsLastSibling();
            _selfRect.offsetMax = Vector2.zero;
        }
    }
}