using System.Threading.Tasks;
using CommonTemplate.Resource;
using UnityEngine;
using Zenject;

namespace CommonTemplate.Factory
{
    public abstract class BaseFactory<T> where T : Object
    {
        protected DiContainer _diContainer;
        protected IResourceLoader _resourceLoader;

        protected BaseFactory(DiContainer diContainer, IResourceLoader resourceLoader)
        {
            _diContainer = diContainer;
            _resourceLoader = resourceLoader;
        }

        public virtual async Task<T> Create(string resource, Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var obj = await _resourceLoader.Load<T>(resource);
            return obj;
        }
    }
}