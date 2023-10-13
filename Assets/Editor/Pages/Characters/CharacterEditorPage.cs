using System.Collections.Generic;
using System.Linq;
using Data;
using Editor.Common;
using Editor.Utils;
using Models;

namespace Editor.Pages.Characters
{
    public class CharacterEditorPage : ListEditorPage<CharacterEditor, CharacterModel>
    {
        protected override Dictionary<string, CharacterModel> Models => GameData.Characters;

        public CharacterEditorPage(GameData gameData)
        {
            GameData = gameData;
            LoadData();
        }

        public override void SaveData()
        {
            var models = ChildElements.Select(u => u.GetModel()).ToList();
            EditorUtils.Save(models);
        }
    }
}