using System;
using System.Collections.Generic;
using Data;
using Editor.Common;
using Editor.Pages.Characters;
using Editor.Pages.Weapon;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.Windows
{
    public class GDEditorWindow : OdinMenuEditorWindow
    {
        private List<BaseEditorPage> _allPages;
        private GameData _gameData;
        private OdinMenuTree _tree;
        private CharacterEditorPage _characterEditor;
        private WeaponEditorPage _weaponEditor;
        private ProjectileEditorPage _projectileEditor;

        [MenuItem("GDEditorWindow/Main _%#T")]
        public static void OpenWindow()
        {
            GetWindow<GDEditorWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            _tree = new OdinMenuTree();
            InitPages();
            FillTree();
            return _tree;
        }

        private void InitPages()
        {
            _gameData = new GameData();
            _gameData.Init();

            _allPages = new List<BaseEditorPage>();
            _characterEditor = AddPage<CharacterEditorPage>();
            _weaponEditor = AddPage<WeaponEditorPage>();
            _projectileEditor = AddPage<ProjectileEditorPage>();
        }

        private void FillTree()
        {
            _tree.Selection.SupportsMultiSelect = false;
            _tree.Add("Characters/CharacterEditor", _characterEditor);
            _tree.Add("Weapon/WeaponEditor", _weaponEditor);
            _tree.Add("Weapon/ProjectileEditor", _projectileEditor);
        }

        private T AddPage<T>() where T : BaseEditorPage
        {
            try
            {
                var constructor = typeof(T).GetConstructor(new Type[] { _gameData.GetType() });
                var result = (T)constructor.Invoke(new object[] { _gameData });
                _allPages.Add(result);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError($"Adding page {typeof(T)} error\n{e}");
                throw;
            }
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            var condition = Event.current.type == EventType.KeyUp
                            && Event.current.modifiers == EventModifiers.Control
                            && Event.current.keyCode == KeyCode.S;

            if (condition)
                SaveAll();
        }

        protected override void DrawMenu()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save All", new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(40) }))
                SaveAll();

            if (GUILayout.Button("Load All",
                    new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(40) }))
                ForceMenuTreeRebuild();

            GUILayout.EndHorizontal();
            base.DrawMenu();
        }

        private void SaveAll()
        {
            Debug.Log("Save configs start");
            foreach (var page in _allPages)
            {
                page.SaveData();
            }

            InitPages();
            Debug.Log("Save configs complete");
        }

        protected override void OnDestroy()
        {
            _allPages.Clear();
            _allPages = null;
            _gameData = null;
            base.OnDestroy();
        }
    }
}