using System.Collections.Generic;
using Models;
using SO;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameDataInstaller : MonoInstaller
    {
        [SerializeField] private AllWeapons _weapons;
        [SerializeField] private AllCharacters _characters;
        [SerializeField] private AllEnemySpawners _spawners;
        [SerializeField] private AllWalls _walls;
        [SerializeField] private AllProjectiles _projectiles;
        [SerializeField] private GameSetting _setting;
        public override void InstallBindings()
        {
            BindWeaponModels();
            BindCharacterModels();
            BindEnemySpawners();
            BindWallModels();
            BindProjectileModels();
            BindSetting();
        }

        private void BindProjectileModels()
        {
            var projectiles = new Dictionary<string, ProjectileModel>();
            foreach (var item in _projectiles.Projectiles)
            {
                projectiles.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, ProjectileModel>>()
                .FromInstance(projectiles)
                .AsSingle();
        }

        private void BindWallModels()
        {
            var walls = new Dictionary<string, WallModel>();
            foreach (var item in _walls.Walls)
            {
                walls.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, WallModel>>()
                .FromInstance((walls))
                .AsSingle();
        }

        private void BindEnemySpawners()
        {
            var spawners = new Dictionary<string, EnemySpawnerModel>();
            foreach (var item in _spawners.EnemySpawners)
            {
                spawners.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, EnemySpawnerModel>>()
                .FromInstance(spawners)
                .AsSingle();
        }

        private void BindCharacterModels()
        {
            var character = new Dictionary<string, CharacterModel>();
            foreach (var item in _characters.Characters)
            {
                character.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, CharacterModel>>()
                .FromInstance(character)
                .AsSingle();
        }

        private void BindWeaponModels()
        {
            var weapons = new Dictionary<string, WeaponModel>();
            foreach (var item in _weapons.Weapons)
            {
                weapons.Add(item.Id, item);
            }

            Container
                .Bind<Dictionary<string, WeaponModel>>()
                .FromInstance(weapons)
                .AsSingle();
        }

        private void BindSetting()
        {
            Container
                .Bind<GameSetting>()
                .FromInstance(_setting)
                .AsSingle();
        }
    }
}