using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = nameof(GameSetting),menuName = "Data/GameSetting")]
    public class GameSetting : ScriptableObject
    {
        public string EnemyCharacterId;
        public string PalyerCharacterId;
        public string EnemySpawnerId;
        public string WallId;
        public int MinKillToWin;
        public int MaxKillToWin;
    }
}