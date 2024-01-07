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
    public class ProjectileFactory : BaseFactory<GameObject>
    {
        private readonly Dictionary<string, WeaponModel> _weaponModels;

        public ProjectileFactory(DiContainer diContainer, IResourceLoader resourceLoader,
            Dictionary<string, WeaponModel> weaponModels) : base(diContainer, resourceLoader)
        {
            _weaponModels = weaponModels;
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