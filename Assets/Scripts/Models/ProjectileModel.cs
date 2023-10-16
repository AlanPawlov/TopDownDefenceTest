using System;

namespace Models
{
    [Serializable]
    public class ProjectileModel : BaseModel
    {
        public string ResourcePath;
        public float BulletSpeed;
    }
}