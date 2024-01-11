using System.Threading.Tasks;

namespace Common.Resource
{
    public interface IResourceLoader
    {
        Task<T> Load<T>(string resource) where T : UnityEngine.Object;
        void Release();
    }
}