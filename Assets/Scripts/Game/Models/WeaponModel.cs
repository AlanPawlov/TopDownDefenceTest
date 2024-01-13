using System;
using Common.Data;

namespace Game.Models
{
    [Serializable]
    public class WeaponModel : BaseModel
    {
        public float Range;
        public float AttackSpeed;
        public int Damage;
        public string ProjectileId;
    }
}