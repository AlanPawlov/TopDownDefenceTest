using System.Collections.Generic;
using Models;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(AllWeapons), menuName = "Data/AllWeapons")]
    public class AllWeapons : ScriptableObject
    {
        public List<WeaponModel> Weapons;
    }
}