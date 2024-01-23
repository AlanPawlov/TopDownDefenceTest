using System.Collections.Generic;
using CommonTemplate.Data;
using Editor.Common;
using Game.Models;

namespace Editor.Pages.Weapon
{
    public class WeaponEditorPage : ListEditorPage<WeaponEditor, WeaponModel>
    {
        protected override Dictionary<string, WeaponModel> Models => GameData.Weapons;

        public WeaponEditorPage(GameData gameData)
        {
            GameData = gameData;
            LoadData();
        }
    }
}