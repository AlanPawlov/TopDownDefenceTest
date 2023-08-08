using System.Threading.Tasks;
using Interfaces;
using Resource;
using UITemplate;
using UnityEngine;
using Weapon;
using Zenject;

namespace Factories
{
    public class CharacterFactory : BaseFactory<Character>
    {
        public CharacterFactory(DiContainer diContainer, IResourceLoader resourceLoader) : base(diContainer,
            resourceLoader)
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

        public async Task<Character> Create(CharacterType characterType, string resource,
            Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var result = await Create(resource, position, rotation);
            result.ResourceName = $"{characterType}_{resource}";
            IWeapon weapon = null;
            switch (characterType)
            {
                case CharacterType.Enemy:
                    weapon = new SelfDestructWeapon(result);
                    break;
                case CharacterType.Player:
                    weapon = new ProjectileWeapon(result);
                    break;
            }

            _diContainer.Inject(weapon);
            result.Setup(weapon);
            return result;
        }
    }
}