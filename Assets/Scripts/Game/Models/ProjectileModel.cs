using System;
using CommonTemplate.Data;

namespace Game.Models
{
    [Serializable]
    public class ProjectileModel : BaseModel
    {
        public string ResourcePath;
        public float BulletSpeed;
    }
}