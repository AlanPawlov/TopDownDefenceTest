using TMPro;
using UnityEngine;

namespace CommonTemplate.UITemplate.DefaultWindows
{
    public class LoadingWindow : InterfaceWindow
    {
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _progress;

        public void SetData(string description, float progress)
        {
            _description.text = description;
            _progress.text = $"{progress}%";
        }

        public override void Uninit()
        {
            _description.text = default;
            _progress.text = default;
            base.Uninit();
        }
    }
}