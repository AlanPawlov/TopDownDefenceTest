using System.Collections.Generic;
using CommonTemplate.Data;
using Editor.Common;
using Game.Models;

namespace Editor.WallDefence
{
    public class WallEditorPage : ListEditorPage<WallEditor, WallModel>
    {
        protected override Dictionary<string, WallModel> Models => GameData.Walls;

        public WallEditorPage(GameData gameData)
        {
            GameData = gameData;
            LoadData();
        }
    }
}