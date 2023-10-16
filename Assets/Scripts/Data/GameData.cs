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
        public Dictionary<string, CharacterModel> Characters => _characters;
        public Dictionary<string, WeaponModel> Weapons => _weapons;
        public Dictionary<string, ProjectileModel> Projectiles => _projectiles;

        public void Init()
        {
            _characters = GetModels<CharacterModel>();
            _weapons = GetModels<WeaponModel>();
            _projectiles = GetModels<ProjectileModel>();
        }

        private Dictionary<string, T> GetModels<T>() where T : BaseModel
        {
            var jsonData = TextUtils.GetTextFromLocalStorage<T>();
            return TextUtils.FillDictionary<T>(jsonData);
        }
    }
}