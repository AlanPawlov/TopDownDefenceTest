using System.Collections.Generic;
using System.Linq;
using Data;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;

namespace Editor.Pages.Weapon
{
    [HideReferenceObjectPicker]
    public class WeaponEditor : BaseModelEditor<WeaponModel>
    {
        private readonly GameData _gameData;
        private string _title => Model.Id;
        private Dictionary<string, ProjectileModel> _projectiles => _gameData.Projectiles;
        private string[] _allProjectileIds => _projectiles.Select(l => l.Value).Select(t => t.Id).ToArray();

        public WeaponEditor()
        {
        }

        public WeaponEditor(WeaponModel model, GameData gameData)
        {
            _gameData = gameData;
            model ??= new WeaponModel();
            Model = model;
        }

        [ShowInInspector]
        [TitleGroup("@_title", Alignment = TitleAlignments.Centered)]
        [HorizontalGroup("@_title/Group")]
        [VerticalGroup("@_title/Group/Left")]
        public string Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public int Damage
        {
            get => Model.Damage;
            set => Model.Damage = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public float Range
        {
            get => Model.Range;
            set => Model.Range = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public float AttackSpeed
        {
            get => Model.AttackSpeed;
            set => Model.AttackSpeed = value;
        }

        [ShowInInspector]
        [ValueDropdown(nameof(_allProjectileIds), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        [VerticalGroup("@_title/Group/Right")]
        public string ProjectileId
        {
            get => Model.ProjectileId;
            set => Model.ProjectileId = value;
        }
    }
}