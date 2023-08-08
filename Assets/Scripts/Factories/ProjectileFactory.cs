using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Resource;
using UITemplate;
using UnityEngine;
using Weapon;
using Zenject;

namespace Factories
{
    public class ProjectileFactory : BaseFactory<Projectile>
    {
        private readonly Dictionary<string, WeaponModel> _weaponModels;

        public ProjectileFactory(DiContainer diContainer, IResourceLoader resourceLoader,
            Dictionary<string, WeaponModel> weaponModels) : base(diContainer, resourceLoader)
        {
            _weaponModels = weaponModels;
        }

        public override async Task<Projectile> Create(string resource, Vector3 position = new Vector3(),
            Quaternion rotation = new Quaternion())
        {
            var result = await base.Create(resource, position, rotation);
            result.ResourceName = resource;
            var transform = result.transform;
            transform.position = position;
            transform.rotation = rotation;
            return result;
        }
    }
}