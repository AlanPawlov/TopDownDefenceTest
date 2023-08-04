using System.Threading.Tasks;
using Resource;
using UITemplate;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class CharacterFactory : BaseFactory<Character>
    {
        public CharacterFactory(DiContainer diContainer, IResourceLoader resourceLoader) : base(diContainer, resourceLoader)
        {
        }

        public override async Task<Character> Create(string resource, Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var result = await base.Create(resource, position, rotation);
            var transform = result.transform;
            transform.position = position;
            transform.rotation = rotation;
            return result;
        }
    }
}
