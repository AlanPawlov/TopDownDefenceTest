using System.Threading.Tasks;
using UnityEngine;

namespace Common.Resource
{
    public class ResourceLoader : IResourceLoader
    {
        public async Task<T> Load<T>(string resource)  where T : UnityEngine.Object
        {
            var result = Resources.Load<T>(resource);
            return result;
        }
        
        public void Release()
        {
            throw new System.NotImplementedException();
        }
    }
}