using UnityEngine;

namespace CommonTemplate.UITemplate
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvasRect;
        public RectTransform MainCanvasTransform => _canvasRect;

        public void Start()
        {
            Debug.Log($"Create {this}");
        }

        private void OnDestroy()
        {
            Debug.Log($"Destroy {this}");
        }
    }
}