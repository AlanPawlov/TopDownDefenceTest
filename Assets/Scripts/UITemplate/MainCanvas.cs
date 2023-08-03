using UnityEngine;

namespace UI
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _canvasRect;

        public static MainCanvas Instance;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy( gameObject );
                return;
            }
            Instance = this;
            DontDestroyOnLoad( gameObject );
        }

        public RectTransform MainCanvasTransform => _canvasRect;
    }
}