using System.Collections.Generic;
using System.Threading.Tasks;
using Character;
using Interfaces;
using Models;
using Resource;
using UITemplate;
using UnityEngine;
using Weapon;
using Zenject;

namespace Factories
{
    public class CharacterFactory : BaseFactory<Character.Character>
    {
        private readonly Dictionary<string, CharacterModel> _characterModels;
        private readonly Dictionary<string, WeaponModel> _weaponModels;
        private readonly Dictionary<string,ProjectileModel> _projectileModels;

        public CharacterFactory(DiContainer diContainer, IResourceLoader resourceLoader,
            Dictionary<string, CharacterModel> characterModels, Dictionary<string, WeaponModel> weaponModels,
            Dictionary<string, ProjectileModel> projectileModels) : base(diContainer,
            resourceLoader)
        {
            _characterModels = characterModels;
            _weaponModels = weaponModels;
            _projectileModels = projectileModels;
        }

        public override async Task<Character.Character> Create(string resource, Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var result = await base.Create(resource, position, rotation);
            var transform = result.transform;
            transform.position = position;
            transform.rotation = rotation;
            return result;
        }

        public async Task<Character.Character> Create(CharacterType characterType, string id,
            Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var model = _characterModels[id];
            var weaponModel = _weaponModels[model.WeaponId];
            var result = await Create(model.CharacterPath, position, rotation);
            result.ResourceName = $"{characterType}_{id}";

            IWeapon weapon = null;
            switch (characterType)
            {
                case CharacterType.Enemy:
                    weapon = new SelfDestructWeapon(result, weaponModel);
                    break;
                case CharacterType.Player:
                    weapon = new ProjectileWeapon(result, weaponModel,_projectileModels[weaponModel.ProjectileId]);
                    break;
            }

            _diContainer.Inject(weapon);
            result.Setup(weapon, model);
            return result;
        }
    }
}