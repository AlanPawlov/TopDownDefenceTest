using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Utils;
using Editor.Common;
using Editor.Utils;
using Game.Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor.Pages.Characters
{
    [HideReferenceObjectPicker]
    public class CharacterEditor : BaseModelEditor<CharacterModel>
    {
        private readonly GameData _gameData;
        private string _title => Model.Id;
        private Dictionary<string, WeaponModel> _weapons => _gameData.Weapons;
        private string[] _allWeaponIds => _weapons.Select(l => l.Value).Select(t => t.Id).ToArray();

        public CharacterEditor()
        {
        }

        public CharacterEditor(CharacterModel model, GameData gameData)
        {
            _gameData = gameData;
            model ??= new CharacterModel();
            Model = model;
            _animatorOverrideController =
                AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(Model.AnimatorController);
            _characterView = AssetDatabase.LoadAssetAtPath<Sprite>(Model.CharacterView);
            _prefabPath = AssetDatabase.LoadAssetAtPath<GameObject>(Model.CharacterPath);
        }

        [ShowInInspector]
        [TitleGroup("@_title", Alignment = TitleAlignments.Centered)]
        [HorizontalGroup("@_title/Group")]
        [VerticalGroup("@_title/Group/Left")]
        [LabelWidth(150)]
        public string Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public int Health
        {
            get => Model.Health;
            set => Model.Health = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public float MaxSpeed
        {
            get => Model.MaxSpeed;
            set => Model.MaxSpeed = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public float MinSpeed
        {
            get => Model.MinSpeed;
            set => Model.MinSpeed = value;
        }

        [ShowInInspector]
        [VerticalGroup("@_title/Group/Left")]
        public AnimatorOverrideController AnimatorController
        {
            get => _animatorOverrideController;
            set
            {
                _animatorOverrideController = value;
                if (!value)
                {
                    Model.AnimatorController = null;
                    return;
                }

                var path = AssetDatabase.GetAssetPath(value);
                var address = path.CollapseAddressablePath();
                EditorUtils.AddToAddressablesGroup(path, address);
                Model.AnimatorController = path;
            }
        }

        private AnimatorOverrideController _animatorOverrideController;

        [ShowInInspector]
        [ValueDropdown(nameof(_allWeaponIds), IsUniqueList = true, DropdownWidth = 250, SortDropdownItems = true)]
        [VerticalGroup("@_title/Group/Left")]
        public string WeaponId
        {
            get => Model.WeaponId;
            set => Model.WeaponId = value;
        }

        [ShowInInspector]
        [PreviewField(60)]
        [VerticalGroup("@_title/Group/Right")]
        public GameObject CharacterPrefab
        {
            get => _prefabPath;
            set
            {
                _prefabPath = value;
                if (!value)
                {
                    Model.CharacterView = null;
                    return;
                }

                var path = AssetDatabase.GetAssetPath(value);
                path.AddAsAddressables();
                Model.CharacterPath = path;
            }
        }

        private GameObject _prefabPath;

        [ShowInInspector]
        [PreviewField(60)]
        [VerticalGroup("@_title/Group/Right")]
        public Sprite CharacterView
        {
            get => _characterView;
            set
            {
                _characterView = value;
                if (!value)
                {
                    Model.CharacterView = null;
                    return;
                }

                var path = AssetDatabase.GetAssetPath(value);
                var address = path.CollapseAddressablePath();
                EditorUtils.AddToAddressablesGroup(path, address);
                Model.CharacterView = path;
            }
        }

        private Sprite _characterView;
    }
}