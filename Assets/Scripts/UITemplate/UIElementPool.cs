using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIElementPool : IDisposable
    {
        private readonly DiContainer _container;
        private Dictionary<string, List<BaseUIElement>> _pool;
        private Transform _poolHolder;

        public Transform PoolHolder
        {
            get
            {
                if (_poolHolder == null)
                {
                    var newGo = new GameObject();
                    newGo.name = "PoolHolder";
                    newGo.SetActive(false);
                    newGo.transform.position = Vector3.back * 1000;
                    _poolHolder = newGo.transform;
                }

                return _poolHolder;
            }
        }

        public UIElementPool(DiContainer container)
        {
            _container = container;
            _pool = new Dictionary<string, List<BaseUIElement>>();
        }

        public BaseUIElement LoadFromPool<T>(string resourceName, Transform parent) where T : BaseUIElement
        {
            BaseUIElement obj = null;
            if (_pool.TryGetValue(resourceName, out var list))
            {
                if (list.Count > 0)
                {
                    var index = list.Count - 1;
                    obj = list[index];
                    list.RemoveAt(index);
                    obj.transform.SetParent(parent);
                    obj.gameObject.SetActive(true);

                    if (obj.gameObject == null)
                        Debug.LogError($"{resourceName} GO is null");
                }
                else
                    return null;
                
                //TODO:Почему то здесь не падает исключение, потом прийти и разобраться
                obj.ResetParentWindow();
                var transform = obj.transform;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;
                if (obj.RectTransform.anchorMin != obj.RectTransform.anchorMax)
                {
                    obj.RectTransform.sizeDelta = Vector2.zero;
                }

                _container.Inject(obj);
            }

            return obj;
        }

        public void RemoveToPool(BaseUIElement element)
        {
            if (!_pool.TryGetValue(element.ResourceName, out var list))
            {
                list = new List<BaseUIElement>();
                _pool.Add(element.ResourceName, list);
            }

            list.Add(element);
            element.transform.SetParent(PoolHolder);
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}