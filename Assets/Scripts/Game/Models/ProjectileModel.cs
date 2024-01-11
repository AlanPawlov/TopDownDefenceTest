using System;

namespace Game.Models
{
    [Serializable]
    public class ProjectileModel : BaseModel
    {
        public string ResourcePath;
        public float BulletSpeed;
    }
}