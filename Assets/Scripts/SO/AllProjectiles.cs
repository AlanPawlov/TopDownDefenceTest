using System.Collections.Generic;
using Models;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(AllProjectiles), menuName = "Data/AllProjectiles")]
    public class AllProjectiles : ScriptableObject
    {
        public List<ProjectileModel> Projectiles;
    }
}