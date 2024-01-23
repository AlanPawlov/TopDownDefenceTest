using System.Collections.Generic;
using CommonTemplate.Data;
using Editor.Common;
using Game.Models;

namespace Editor.WallDefence
{
    public class EnemySpawnerEditorPage : ListEditorPage<EnemySpawnerEditor, EnemySpawnerModel>
    {
        protected override Dictionary<string, EnemySpawnerModel> Models => GameData.EnemySpawners;

        public EnemySpawnerEditorPage(GameData gameData)
        {
            GameData = gameData;
            LoadData();
        }
    }
}