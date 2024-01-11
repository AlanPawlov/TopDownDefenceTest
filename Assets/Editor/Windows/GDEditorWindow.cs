using System;
using System.Collections.Generic;
using Common.Data;
using Editor.Common;
using Editor.Pages.Characters;
using Editor.Pages.Weapon;
using Editor.WallDefence;
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
        private EnemySpawnerEditorPage _enemySpawnerEditor;
        private WallDefenceRulesEditorPage _wallDefenceRules;
        private WallEditorPage _wallEditor;

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
            _enemySpawnerEditor = AddPage<EnemySpawnerEditorPage>();
            _wallEditor = AddPage<WallEditorPage>();
            _wallDefenceRules = AddPage<WallDefenceRulesEditorPage>();
        }

        private void FillTree()
        {
            _tree.Selection.SupportsMultiSelect = false;
            _tree.Add("Characters/CharacterEditor", _characterEditor);
            _tree.Add("Weapon/WeaponEditor", _weaponEditor);
            _tree.Add("Weapon/ProjectileEditor", _projectileEditor);
            _tree.Add("Gamemodes/WallDefence/WallDefenceEditor", _wallDefenceRules);
            _tree.Add("Gamemodes/WallDefence/Spawners", _enemySpawnerEditor);
            _tree.Add("Gamemodes/WallDefence/Walls", _wallEditor);
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