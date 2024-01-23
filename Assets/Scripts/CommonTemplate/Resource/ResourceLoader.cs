using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonTemplate.Resource
{
    public class ResourceLoader : IResourceLoader
    {
        private Dictionary<string, Object> _cache;

        public ResourceLoader()
        {
            _cache = new Dictionary<string, Object>();
        }

        public async Task<T> Load<T>(string resource) where T : UnityEngine.Object
        {
            if (_cache.TryGetValue(resource, out Object cached))
                return cached as T;

            var result = Resources.Load<T>(resource);
            _cache.Add(resource, result);
            return result;
        }

        public void Release()
        {
            foreach (var item in _cache.Values)
            {
                Resources.UnloadAsset(item);
            }

            _cache.Clear();
        }
    }
}