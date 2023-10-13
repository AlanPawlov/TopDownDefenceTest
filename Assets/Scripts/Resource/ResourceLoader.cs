using System.Threading.Tasks;
using UnityEngine;

namespace Resource
{
    public class ResourceLoader : IResourceLoader
    {
        public async Task<T> Load<T>(string resource)  where T : UnityEngine.Object
        {
            var prefab = Resources.Load<T>(resource);
            var result = Object.Instantiate<T>(prefab);
            return result;
        }
        
        public void Release()
        {
            throw new System.NotImplementedException();
        }
    }
}