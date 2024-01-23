using System.Collections.Generic;
using CommonTemplate.Data;
using Editor.Common;
using Game.Models;

namespace Editor.Pages.Weapon
{
    public class ProjectileEditorPage : ListEditorPage<ProjectileEditor, ProjectileModel>
    {
        protected override Dictionary<string, ProjectileModel> Models => GameData.Projectiles;

        public ProjectileEditorPage(GameData gameData)
        {
            GameData = gameData;
            LoadData();
        }
    }
}