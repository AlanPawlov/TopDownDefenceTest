using System;
using System.Collections.Generic;
using Zenject;

namespace States.GameStates
{
    public class ProjectStateMachine : IGameStateMachine, IInitializable
    {
        protected readonly StateFactory StateFactory;
        protected Dictionary<Type, IState> AllStates;
        protected IState CurState;

        public ProjectStateMachine(StateFactory stateFactory)
        {
            StateFactory = stateFactory;
        }

        public void StartState<TState>() where TState : class, IState
        {
            var state = SwitchState<TState>();
            state.EnterState();
        }

        public void StartState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = SwitchState<TState>();
            state.StartState(payload);
        }

        private TState SwitchState<TState>() where TState : class, IState
        {
            CurState?.ExitState();
            var state = GetState<TState>();
            CurState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return AllStates[typeof(TState)] as TState;
        }

        public virtual void Initialize()
        {
            AllStates = new Dictionary<Type, IState>
            {
                [typeof(LoadProgressState)] = StateFactory.CreateState<LoadProgressState>(),
                [typeof(LoadMenuState)] = StateFactory.CreateState<LoadMenuState>(),
                [typeof(LoadLevelState)] = StateFactory.CreateState<LoadLevelState>(),
                [typeof(MenuState)] = StateFactory.CreateState<MenuState>(),
                [typeof(GameState)] = StateFactory.CreateState<GameState>(),
                [typeof(BootstrapState)] = StateFactory.CreateState<BootstrapState>()
            };
        }
    }
}