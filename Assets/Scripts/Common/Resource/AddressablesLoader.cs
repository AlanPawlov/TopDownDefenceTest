using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Common.Resource
{
    public class AddressablesLoader : IResourceLoader
    {
        private Dictionary<string, AsyncOperationHandle> _cache;
        private Dictionary<string, List<AsyncOperationHandle>> _handles;

        public AddressablesLoader()
        {
            _cache = new Dictionary<string, AsyncOperationHandle>();
            _handles = new Dictionary<string, List<AsyncOperationHandle>>();
            Addressables.InitializeAsync();
        }
        
        public async Task<T> Load<T>(string resource) where T : Object
        {
            if (_cache.TryGetValue(resource, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            var handle = Addressables.LoadAssetAsync<T>(resource);
            var result = await RunWithCacheOnComplete(handle, cacheKey: resource);
            return result;
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle => { _cache[cacheKey] = completeHandle; };
            AddHandle<T>(cacheKey, handle);
            var result = await handle.Task;
            return result;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        public void Release()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }

            _cache.Clear();
            _handles.Clear();
        }
    }
}