using System.Collections.Generic;
using CommonTemplate.Data;
using Editor.Common;
using Game.Models;

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