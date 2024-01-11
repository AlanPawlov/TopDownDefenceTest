using Common.Data;
using Editor.Common;
using Editor.Utils;
using Game.Models;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Editor.Pages.Weapon
{
    [HideReferenceObjectPicker]
    public class ProjectileEditor : BaseModelEditor<ProjectileModel>
    {
        private readonly GameData _gameData;
        private string _title => Model.Id;

        public ProjectileEditor()
        {
        }

        public ProjectileEditor(ProjectileModel model, GameData gameData)
        {
            _gameData = gameData;
            model ??= new ProjectileModel();
            Model = model;
            _prefabPath = AssetDatabase.LoadAssetAtPath<GameObject>(Model.ResourcePath);
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
        [VerticalGroup("@_title/Group/Right")]
        public float Range
        {
            get => Model.BulletSpeed;
            set => Model.BulletSpeed = value;
        }

        [ShowInInspector]
        [PreviewField(90, ObjectFieldAlignment.Left)]
        [HorizontalGroup("@_title/Prefab")]
        public GameObject Prefab
        {
            get => _prefabPath;
            set
            {
                _prefabPath = value;
                if (!value)
                {
                    Model.ResourcePath = null;
                    return;
                }

                var path = AssetDatabase.GetAssetPath(value);
                path.AddAsAddressables();
                Model.ResourcePath = path;
            }
        }

        private GameObject _prefabPath;
    }
}