using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Data;
using Common.Factory;
using Common.Resource;
using Common.Utils;
using Game.Models;
using Game.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Character
{
    public class CharacterFactory : BaseFactory<GameObject>
    {
        private readonly Dictionary<string, CharacterModel> _characterModels;
        private readonly Dictionary<string, WeaponModel> _weaponModels;
        private readonly Dictionary<string, ProjectileModel> _projectileModels;

        public CharacterFactory(DiContainer diContainer, IResourceLoader resourceLoader, GameData gameData) : base(
            diContainer,
            resourceLoader)
        {
            _characterModels = gameData.Characters;
            _weaponModels = gameData.Weapons;
            _projectileModels = gameData.Projectiles;
        }

        public async Task<global::Game.Character.Character> Create(CharacterType characterType, string id,
            Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var model = _characterModels[id];
            var weaponModel = _weaponModels[model.WeaponId];
            var prefab = await base.Create(model.CharacterPath.CollapseAddressablePath(), position, rotation);
            var result = Object.Instantiate(prefab.GetComponent<global::Game.Character.Character>());
            _diContainer.Inject(result);
            var transform = result.transform;
            transform.position = position;
            transform.rotation = rotation;
            result.ResourceName = $"{characterType}_{id}";
            var weapon = SetWeapon(characterType, result, weaponModel);
            result.SetupBaseStats(weapon, model);
            result.SetupView(_resourceLoader);
            return result;
        }

        private IWeapon SetWeapon(CharacterType characterType, global::Game.Character.Character result, WeaponModel weaponModel)
        {
            IWeapon weapon = null;
            switch (characterType)
            {
                case CharacterType.Enemy:
                    weapon = new SelfDestructWeapon(result, weaponModel);
                    break;
                case CharacterType.Player:
                    weapon = new ProjectileWeapon(result, weaponModel, _projectileModels[weaponModel.ProjectileId]);
                    break;
            }

            _diContainer.Inject(weapon);
            return weapon;
        }
    }
}