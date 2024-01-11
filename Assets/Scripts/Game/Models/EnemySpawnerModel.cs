using System;

namespace Game.Models
{
    [Serializable]
    public class EnemySpawnerModel : BaseModel
    {
        public float MaxSpawnTimeout;
        public float MinSpawnTimeout;
    }
}