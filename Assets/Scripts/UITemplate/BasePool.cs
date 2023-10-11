using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI
{
    public class BasePool : IDisposable
    {
        private readonly DiContainer _container;
        private Dictionary<string, List<IPoolable>> _pool;
        private Transform _poolHolder;

        public Transform PoolHolder
        {
            get
            {
                if (_poolHolder == null)
                {
                    var newGo = new GameObject();
                    newGo.name = $"POOL_{GetType()}";
                    newGo.SetActive(false);
                    newGo.transform.position = Vector3.back * 1000;
                    _poolHolder = newGo.transform;
                }

                return _poolHolder;
            }
        }

        public BasePool(DiContainer container)
        {
            _container = container;
            _pool = new Dictionary<string, List<IPoolable>>();
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        public virtual IPoolable LoadFromPool<T>(string resourceName, Transform parent = null)
        {
            IPoolable obj = null;
            if (_pool.TryGetValue(resourceName, out var list))
            {
                if (list.Count > 0)
                {
                    var index = list.Count - 1;
                    obj = list[index];
                    list.RemoveAt(index);
                    if (obj.Transform.gameObject == null)
                        Debug.LogError($"{resourceName} GO is null");
                    obj.Transform.SetParent(parent);
                    obj.Transform.gameObject.SetActive(true);
                }
                else
                    return null;

                _container.Inject(obj);
            }

            return obj;
        }

        public void RemoveToPool(IPoolable element)
        {
            if (!_pool.TryGetValue(element.ResourceName, out var list))
            {
                list = new List<IPoolable>();
                _pool.Add(element.ResourceName, list);
            }

            list.Add(element);
            element.Transform.SetParent(PoolHolder);
        }

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            Clear();
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public virtual void Dispose()
        {
            Clear();
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }
    }
}