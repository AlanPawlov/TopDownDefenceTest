using System.Threading.Tasks;
using Common.Factory;
using Common.Resource;
using UnityEngine;
using Zenject;

namespace Common.UITemplate
{
    public class UIFactory : BaseFactory<BaseUIElement>
    {
        public UIFactory(DiContainer diContainer, IResourceLoader resourceLoader) : base(diContainer, resourceLoader)
        {
        }

        public async Task<T> Create<T>(string resourceName, Transform parent) where T : BaseUIElement
        {
            var prefab = await _resourceLoader.Load<GameObject>(resourceName);
            var obj = Object.Instantiate(prefab.GetComponent<BaseUIElement>());
            obj.transform.parent = parent;
            obj.ResourceName = resourceName;
        
            obj.ResetParentWindow();
            var transform = obj.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            if (obj.RectTransform.anchorMin != obj.RectTransform.anchorMax)
                obj.RectTransform.sizeDelta = Vector2.zero;

            _diContainer.Inject(obj);
            await obj.Init();
            return (T)obj;
        }
    }
}