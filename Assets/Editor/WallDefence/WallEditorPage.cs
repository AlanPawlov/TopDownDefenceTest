using System.Collections.Generic;
using Data;
using Editor.Common;
using Models;

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