using System.Threading.Tasks;

namespace Resource
{
    public interface IResourceLoader
    {
        void Init();
        Task<T> Load<T>(string resource) where T : UnityEngine.Object;
        void Release(string resource);
        void ReleaseAll();
    }
}