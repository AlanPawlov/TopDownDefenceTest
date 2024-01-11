using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Common.UITemplate
{
    public class UIManager : IDisposable
    {
        private Transform _sceneCanvas;
        private Transform _globalCanvas;
        private InterfaceWindow _exclusiveWindow;
        private List<InterfaceWindow> _additionalWindows;
        private List<InterfaceWindow> _windowsToUninit;
        private UIFactory _uiFactory;
        private UIElementPool _uiPool;

        [Inject]
        public void Construct(UIElementPool uiPool, UIFactory uiFactory, MainCanvas globalCanvas)
        {
            _uiPool = uiPool;
            _uiFactory = uiFactory;
            _globalCanvas = globalCanvas.transform;
            _additionalWindows = new List<InterfaceWindow>();
            _windowsToUninit = new List<InterfaceWindow>();
        }

        public async Task<T> CreateElement<T>(string resourceName, Transform parent) where T : BaseUIElement
        {
            if (string.IsNullOrEmpty(resourceName) || parent == null)
            {
                Debug.LogError($"UI element error resourceName: {resourceName} parent: {parent}");
            }

            var obj = _uiPool.LoadFromPool<T>(resourceName, parent);

            if (obj == null)
            {
                obj = await _uiFactory.Create<T>(resourceName, parent);
                return (T)obj;
            }

            await obj.Init();
            return (T)obj;
        }

        public async Task<T> CreateWindow<T>(string resourceName, WindowBehavior behavior) where T : InterfaceWindow
        {
            var window = await CreateElement<T>(resourceName, SceneCanvas);
            window.ShowWindow(true);

            if (behavior == WindowBehavior.Exclusive)
            {
                _windowsToUninit.AddRange(_additionalWindows);

                _additionalWindows.Clear();

                if (_exclusiveWindow != null)
                {
                    _windowsToUninit.Add(_exclusiveWindow);
                }

                _exclusiveWindow = window;
            }
            else if (behavior == WindowBehavior.Additional)
            {
                _additionalWindows.Add(window);
            }

            return window;
        }

        public async Task<T> CreateGlobalWindow<T>(string resourceName) where T : InterfaceWindow
        {
            var window = await CreateElement<T>(resourceName, GlobalCanvas);
            window.ShowWindow(true);
            return window;
        }

        public InterfaceWindow FocusedWindow
        {
            get
            {
                for (var i = _additionalWindows.Count - 1; i >= 0; i--)
                {
                    if (!_additionalWindows[i].Active)
                    {
                        continue;
                    }

                    return _additionalWindows[i];
                }

                return _exclusiveWindow;
            }
        }

        public Transform SceneCanvas
        {
            get
            {
                if (_sceneCanvas != null)
                {
                    return _sceneCanvas;
                }

//TODO: Эту штуку надо ui инсталлером спавнить и потом инжектить нуждающимся
                var sceneCanvas = UnityEngine.Object.FindObjectOfType<SceneCanvas>();
                _sceneCanvas = sceneCanvas.transform;

                return _sceneCanvas;
            }
        }

        public Transform GlobalCanvas
        {
            get
            {
                if (_globalCanvas != null)
                {
                    return _globalCanvas;
                }

                var globalCanvas = UnityEngine.Object.FindObjectOfType<MainCanvas>();
                _globalCanvas = globalCanvas.transform;

                return _globalCanvas;
            }
        }

        private void Update()
        {
            if (_windowsToUninit.Count == 0)
            {
                return;
            }

            foreach (var window in _windowsToUninit)
            {
                if (!window.Active)
                {
                    continue;
                }

                window.Uninit();
            }

            _windowsToUninit.Clear();
        }

        public void Uninit()
        {
            _windowsToUninit.AddRange(_additionalWindows);
            _additionalWindows.Clear();

            if (_exclusiveWindow != null)
            {
                _windowsToUninit.Add(_exclusiveWindow);
            }

            foreach (var window in _windowsToUninit)
            {
                if (!window.Active)
                {
                    continue;
                }

                window.Uninit();
            }
        }

        public void UninitAll()
        {
            _exclusiveWindow?.Uninit();
            foreach (var window in _additionalWindows)
            {
                window.Uninit();
            }
        }

        public void Dispose()
        {
            UninitAll();
        }

        public void RemoveToPool(BaseUIElement element)
        {
            _uiPool.RemoveToPool(element);
        }
    }
}