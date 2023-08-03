using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class LabelWidget : BaseUIElement
    {
        [SerializeField] protected Image BackgroundImage;
        [SerializeField] protected TMP_Text Text;

        public void SetData(string text, Sprite sprite = null)
        {
            Text.text = text;
            //BackgroundImage.sprite = sprite;
        }

        public override void Uninit()
        {
            Text.text = string.Empty;
            //BackgroundImage.sprite = default;
            base.Uninit();
        }
    }
}