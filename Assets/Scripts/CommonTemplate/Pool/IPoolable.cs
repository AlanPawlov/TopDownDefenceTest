using System.Threading.Tasks;
using UnityEngine;

namespace CommonTemplate.Pool
{
    public interface IPoolable
    {
        public Transform Transform { get; }
        public string ResourceName { get; set; }
        public Task Init();
        public void Uninit();
    }
}