using System.Collections.Generic;
using Common.Data;
using Editor.Common;
using Models;

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