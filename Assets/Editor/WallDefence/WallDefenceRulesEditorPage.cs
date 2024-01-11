using System.Collections.Generic;
using Common.Data;
using Editor.Common;
using Models;

namespace Editor.WallDefence
{
    public class WallDefenceRulesEditorPage : ListEditorPage<WallDefenceRulesEditor, WallDefenceRulesModel>
    {
        protected override Dictionary<string, WallDefenceRulesModel> Models => GameData.WallDefenceRules;

        public WallDefenceRulesEditorPage(GameData gameData)
        {
            GameData = gameData;
            LoadData();
        }
    }
}