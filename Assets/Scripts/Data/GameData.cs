using System.Collections.Generic;
using Models;
using Utils;

namespace Data
{
    public class GameData
    {
        private Dictionary<string, CharacterModel> _characters;
        public Dictionary<string, CharacterModel> Characters => _characters;

        public void Init()
        {
            _characters = GetModels<CharacterModel>();
        }

        private Dictionary<string, T> GetModels<T>() where T : BaseModel
        {
            var jsonData = TextUtils.GetTextFromLocalStorage<T>();
            return TextUtils.FillDictionary<T>(jsonData);
        }
    }
}