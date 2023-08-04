using System.Threading.Tasks;
using DefaultNamespace;
using Resource;
using UITemplate;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIFactory : BaseFactory<BaseUIElement>
    {
        public UIFactory(DiContainer diContainer, IResourceLoader resourceLoader) : base(diContainer, resourceLoader)
        {
        }

        public async Task<T> Create<T>(string resourceName, Transform parent) where T : BaseUIElement
        {
            var obj = await _resourceLoader.Load<BaseUIElement>(resourceName);
            obj.transform.parent = parent;
            obj.ResourceName = resourceName;
        
            obj.ResetParentWindow();
            var transform = obj.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            if (obj.RectTransform.anchorMin != obj.RectTransform.anchorMax)
            {
                obj.RectTransform.sizeDelta = Vector2.zero;
            }

            _diContainer.Inject(obj);
            await obj.Init();
            return (T)obj;
        }
    }
}