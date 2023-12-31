using UnityEngine;

namespace UI
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvasRect;
        public RectTransform MainCanvasTransform => _canvasRect;
    }
}