using System;
using UnityEngine;

namespace CommonTemplate
{
    public class UpdateSender : MonoBehaviour
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        private static UpdateSender _instance;

        private void Awake()
        {
            if(_instance != null)
            {
                Destroy( gameObject );
                return;
            }
            _instance = this;
            DontDestroyOnLoad( gameObject );
        }
        
        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            OnUpdate = null;
            OnFixedUpdate = null;
        }
    }
}