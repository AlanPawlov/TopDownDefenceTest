using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Editor.Common;
using Game.Models;
using Sirenix.OdinInspector;

namespace Editor.WallDefence
{
    [HideReferenceObjectPicker]
    public class WallDefenceRulesEditor : BaseModelEditor<WallDefenceRulesModel>
    {
        private GameData _gameData;
        private string[] _walls => _gameData.Walls.Select(l => l.Value).Select(t => t.Id).ToArray();
        private string[] _characters => _gameData.Characters.Select(l => l.Value).Select(t => t.Id).ToArray();
        private string[] _spawners => _gameData.EnemySpawners.Select(l => l.Value).Select(t => t.Id).ToArray();

        public WallDefenceRulesEditor()
        {
        }

        public WallDefenceRulesEditor(WallDefenceRulesModel model, GameData gameData)
        {
            _gameData = gameData;
            model ??= new WallDefenceRulesModel();
            Model = model;
        }

        [ShowInInspector]
        public string Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }

        [ShowInInspector]
        [ValueDropdown(nameof(_characters), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        public string EnemyCharacterId
        {
            get => Model.EnemyCharacterId;
            set => Model.EnemyCharacterId = value;
        }

        [ShowInInspector]
        [ValueDropdown(nameof(_characters), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        public string PalyerCharacterId
        {
            get => Model.PalyerCharacterId;
            set => Model.PalyerCharacterId = value;
        }

        [ShowInInspector]
        [ValueDropdown(nameof(_spawners), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        public string EnemySpawnerId
        {
            get => Model.EnemySpawnerId;
            set => Model.EnemySpawnerId = value;
        }

        [ShowInInspector]
        [ValueDropdown(nameof(_walls), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        public string WallId
        {
            get => Model.WallId;
            set => Model.WallId = value;
        }

        [ShowInInspector]
        public int MinKillToWin
        {
            get => Model.MinKillToWin;
            set => Model.MinKillToWin = value;
        }

        [ShowInInspector]
        public int MaxKillToWin
        {
            get => Model.MaxKillToWin;
            set => Model.MaxKillToWin = value;
        }
    }
}