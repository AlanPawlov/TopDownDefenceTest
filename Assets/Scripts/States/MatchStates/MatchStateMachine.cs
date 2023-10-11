using System;
using System.Collections.Generic;
using Models;
using Services;
using SO;
using States.GameStates;
using UI;

namespace States.MatchStates
{
    public class MatchStateMachine : ProjectStateMachine, IDisposable
    {
        public MatchStateMachine(UIManager uiManager, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            GameSetting gameSetting, Dictionary<string, WallModel> wallModels, PlayerMovementService playerMovement,
            SceneLoader sceneLoader, IProgressService progressService) : base(sceneLoader,
            progressService, uiManager)
        {
            _allStates = new Dictionary<Type, IState>
            {
                [typeof(InitMatchState)] = new InitMatchState(this, playerSpawner),
                [typeof(LoopMatchState)] = new LoopMatchState(this, enemySpawner, uiManager, gameSetting,
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