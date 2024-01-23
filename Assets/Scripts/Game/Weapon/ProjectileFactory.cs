using System.Collections.Generic;
using System.Threading.Tasks;
using CommonTemplate.Data;
using CommonTemplate.Factory;
using CommonTemplate.Resource;
using Game.Models;
using UnityEngine;
using Zenject;

namespace Game.Weapon
{
    public class ProjectileFactory : BaseFactory<GameObject>
    {
        private readonly Dictionary<string, WeaponModel> _weaponModels;

        public ProjectileFactory(DiContainer diContainer, IResourceLoader resourceLoader, GameData gameData) : base(
            diContainer, resourceLoader)
        {
            _weaponModels = gameData.Weapons;
        }

        public async Task<Projectile> Create(string resource, Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var prefab = await base.Create(resource, position, rotation);
            var result = Object.Instantiate(prefab.GetComponent<Projectile>());
            _diContainer.Inject(result);
            result.ResourceName = resource;
            var transform = result.transform;
            transform.position = position;
            transform.rotation = rotation;
            return result;
        }
    }
}