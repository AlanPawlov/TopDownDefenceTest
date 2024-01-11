using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using IPoolable = Common.Pool.IPoolable;

namespace Common.UITemplate
{
    public class BaseUIElement : MonoBehaviour, IPoolable
    {
        private List<BaseUIElement> _childUIElements;
        private RectTransform _rectTransform;
        private InterfaceWindow _parentWindow;
        protected UIManager _uiManager;

        public bool IsDirectChild;
        public bool Active { get; set; }
        public Transform Transform => _rectTransform;
        public string ResourceName { get; set; }


        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        [Inject]
        public void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        protected async Task<T> CreateChild<T>(string resourceName, Transform parent) where T : BaseUIElement
        {
            var child = await _uiManager.CreateElement<T>(resourceName, parent);
            _childUIElements.Add(child);
            return child;
        }

        protected void AddChild<T>(T child) where T : BaseUIElement
        {
            child.IsDirectChild = true;
            child.Init();

            _childUIElements.Add(child);
        }

        public void ResetParentWindow()
        {
            var parentsComponents = GetComponentsInParent<InterfaceWindow>(true);

            if (parentsComponents.Length > 0)
            {
                _parentWindow = parentsComponents[0];
            }
        }


        protected void RemoveChild<T>(T child) where T : BaseUIElement
        {
            if (child == null)
            {
                return;
            }

            child.Uninit();
            _childUIElements.Remove(child);
        }

        public virtual async Task Init()
        {
            Active = true;
            _childUIElements = new List<BaseUIElement>();
        }


        public virtual void Uninit()
        {
            if (!Active)
            {
                return;
            }

            Active = false;

            try
            {
                foreach (var child in _childUIElements)
                {
                    child.Uninit();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }


            _childUIElements = null;
            _parentWindow = null;

            if (!IsDirectChild)
            {
                gameObject.SetActive(false);
                _uiManager.RemoveToPool(this);
                ReleaseResources();
            }
        }

        public void ReleaseResources()
        {
            //_resourceLoader.ReleaseResource( this );
        }
    }
}