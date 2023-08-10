using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using SO;
using UI;
using UnityEngine;

namespace Services.MatchStates
{
    public class MatchController : IStateSwitcher, IDisposable
    {
        private BaseState _curState;
        private List<BaseState> _allStates;

        public MatchController(UIManager uiManager, PlayerSpawner playerSpawner, EnemySpawner enemySpawner,
            GameSetting gameSetting, Dictionary<string, WallModel> wallModels, PlayerMovementService playerMovement)
        {
            _allStates = new List<BaseState>()
            {
                new PrepareMatchState(this, playerSpawner),
                new PlayMatchState(this, enemySpawner, uiManager, gameSetting, wallModels, playerMovement),
                new EndMatchState(this, playerSpawner, enemySpawner, playerMovement)
            };
        }

        public void Start()
        {
            _curState = _allStates[0];
            _curState.StartState();
        }

        public void SwitchState<T>() where T : BaseState
        {
            var state = _allStates.FirstOrDefault(s => s is T);

            if (state == null)
            {
                Debug.LogError($"ERROR: No contains state {typeof(T)}");
                return;
            }

            _curState.StopState();
            _curState = state;
            _curState.StartState();
        }

        public void Dispose()
        {
            foreach (var state in _allStates)
            {
                state.Dispose();
            }
        }
    }
}