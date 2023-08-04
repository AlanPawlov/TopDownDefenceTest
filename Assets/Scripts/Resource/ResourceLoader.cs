using System.Threading.Tasks;
using UnityEngine;

namespace Resource
{
    public class ResourceLoader : IResourceLoader
    {
        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> Load<T>(string resource)  where T : UnityEngine.Object
        {
            var prefab = Resources.Load<T>(resource);
            var result = Object.Instantiate<T>(prefab);
            return result;
        }
        
        public void Release(string resource)
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseAll()
        {
            throw new System.NotImplementedException();
        }
    }
}