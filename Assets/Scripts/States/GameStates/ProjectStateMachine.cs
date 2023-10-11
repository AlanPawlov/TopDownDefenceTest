using System;
using System.Collections.Generic;
using Services;
using UI;

namespace States.GameStates
{
    public class ProjectStateMachine : IGameStateMachine
    {
        protected Dictionary<Type, IState> _allStates;
        protected IState _curState;

        public ProjectStateMachine(SceneLoader sceneLoader, IProgressService progressService, UIManager uiManager)
        {
            _allStates = new Dictionary<Type, IState>
            {
                [typeof(LoadProgressState)] = new LoadProgressState(this, progressService, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(MenuState)] = new MenuState(this, uiManager),
                [typeof(GameState)] = new GameState(this)
            };
        }

        public void StartState<TState>() where TState : class, IState
        {
            var state = SwitchState<TState>();
            state.StartState();
        }

        public void StartState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = SwitchState<TState>();
            state.StartState(payload);
        }

        public TState SwitchState<TState>() where TState : class, IState
        {
            _curState?.StopState();
            var state = GetState<TState>();
            _curState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _allStates[typeof(TState)] as TState;
        }
    }
}