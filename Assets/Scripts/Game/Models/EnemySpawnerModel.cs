using System;
using CommonTemplate.Data;

namespace Game.Models
{
    [Serializable]
    public class EnemySpawnerModel : BaseModel
    {
        public float MaxSpawnTimeout;
        public float MinSpawnTimeout;
    }
}