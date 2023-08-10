using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class LabelWidget : BaseUIElement
    {
        [SerializeField] protected TMP_Text Text;

        public void SetData(string text)
        {
            Text.text = text;
        }

        public override void Uninit()
        {
            Text.text = string.Empty;
            base.Uninit();
        }
    }
}