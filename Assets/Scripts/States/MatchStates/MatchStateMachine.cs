using System;
using System.Collections.Generic;
using Models;
using Resource;
using Services;
using States.GameStates;
using UI;

namespace States.MatchStates
{
    public class MatchStateMachine : ProjectStateMachine, IDisposable
    {
        public MatchStateMachine(UIManager uiManager, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            Dictionary<string,WallDefenceRulesModel> setting, Dictionary<string, WallModel> wallModels, PlayerMovementService playerMovement,
            SceneLoader sceneLoader, IProgressService progressService, IResourceLoader resourceLoader) : base(
            sceneLoader, progressService, uiManager)
        {
            _allStates = new Dictionary<Type, IState>
            {
                [typeof(InitMatchState)] = new InitMatchState(this, playerSpawner, resourceLoader),
                [typeof(LoopMatchState)] = new LoopMatchState(this, enemySpawner, uiManager, setting,
                    wallModels, playerMovement),
                [typeof(EndMatchState)] = new EndMatchState(this, playerSpawner, enemySpawner)
            };
        }

        public void Dispose()
        {
            foreach (var state in _allStates)
            {
                var target = state.Value as BaseMatchState;
                target?.Dispose();
            }
        }
    }
}