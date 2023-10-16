using System.Collections.Generic;
using Models;
using Utils;

namespace Data
{
    public class GameData
    {
        private Dictionary<string, CharacterModel> _characters;
        private Dictionary<string, WeaponModel> _weapons;
        private Dictionary<string, ProjectileModel> _projectiles;
        private Dictionary<string, EnemySpawnerModel> _enemySpawners;
        private Dictionary<string, WallModel> _walls;
        private Dictionary<string, WallDefenceRulesModel> _wallDefenceRules;
        public Dictionary<string, CharacterModel> Characters => _characters;
        public Dictionary<string, WeaponModel> Weapons => _weapons;
        public Dictionary<string, ProjectileModel> Projectiles => _projectiles;
        public Dictionary<string, EnemySpawnerModel> EnemySpawners => _enemySpawners;
        public Dictionary<string, WallModel> Walls => _walls;
        public Dictionary<string, WallDefenceRulesModel> WallDefenceRules => _wallDefenceRules;

        public void Init()
        {
            _characters = GetModels<CharacterModel>();
            _weapons = GetModels<WeaponModel>();
            _projectiles = GetModels<ProjectileModel>();
            _enemySpawners = GetModels<EnemySpawnerModel>();
            _walls = GetModels<WallModel>();
            _wallDefenceRules = GetModels<WallDefenceRulesModel>();
        }

        private Dictionary<string, T> GetModels<T>() where T : BaseModel
        {
            var jsonData = TextUtils.GetTextFromLocalStorage<T>();
            return TextUtils.FillDictionary<T>(jsonData);
        }
    }
}