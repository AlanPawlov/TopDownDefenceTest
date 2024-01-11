using System;

namespace Game.Models
{
    [Serializable]
    public class WallDefenceRulesModel : BaseModel
    {
        public string EnemyCharacterId;
        public string PalyerCharacterId;
        public string EnemySpawnerId;
        public string WallId;
        public int MinKillToWin;
        public int MaxKillToWin;
    }
}