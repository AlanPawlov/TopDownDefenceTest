using CommonTemplate.Pool;
using UnityEngine;
using Zenject;
using IPoolable = CommonTemplate.Pool.IPoolable;

namespace CommonTemplate.UITemplate
{
    public class UIElementPool : BasePool
    {
        public UIElementPool(DiContainer container) : base(container)
        {
        }

        public override IPoolable LoadFromPool<T>(string resourceName, Transform parent)
        {
            BaseUIElement obj = (BaseUIElement)base.LoadFromPool<T>(resourceName, parent);

            if (obj != null)
            {
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
            }

            return obj;
        }
    }
}