using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Services
{
    public class MatchController : IStateSwitcher
    {
        private BaseState _curState;
        private List<BaseState> _allStates;
        private int _curDeath;

        public MatchController(UIManager uiManager, PlayerSpawner playerSpawner, EnemySpawner enemySpawner)
        {
            _allStates = new List<BaseState>()
            {
                new PrepareMatchState(this, playerSpawner),
                new PlayMatchState(this, enemySpawner, uiManager),
                new EndMatchState(this, playerSpawner, enemySpawner, uiManager)
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
    }
}