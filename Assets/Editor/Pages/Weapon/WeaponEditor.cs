using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Editor.Common;
using Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

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

            if (!string.IsNullOrEmpty(Model.ProjectileId))
            {
                _prefabPath =
                    AssetDatabase.LoadAssetAtPath<GameObject>(_gameData.Projectiles[Model.ProjectileId].ResourcePath);
            }
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
        [OnValueChanged(nameof(OnProjectileChanged))]
        [LabelWidth(150)]
        [VerticalGroup("@_title/Group/Right")]
        public string ProjectileId
        {
            get => Model.ProjectileId;
            set => Model.ProjectileId = value;
        }

        [ShowInInspector]
        [PreviewField(60, ObjectFieldAlignment.Center)]
        [HideLabel]
        [VerticalGroup("@_title/Group/Right")]
        public GameObject View
        {
            get => _prefabPath;
        }

        private GameObject _prefabPath;

        private void OnProjectileChanged(string newValue)
        {
            _prefabPath = AssetDatabase.LoadAssetAtPath<GameObject>(_gameData.Projectiles[newValue].ResourcePath);
        }
    }
}