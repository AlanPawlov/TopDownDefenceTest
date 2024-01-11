using System;
using System.Collections.Generic;
using Common.States;
using Common.States.GameStates;

namespace Game.SceneStates
{
    public class SceneStateMachine : ProjectStateMachine, IDisposable
    {
        public SceneStateMachine(StateFactory stateFactory) : base(stateFactory)
        {
        }

        public override void Initialize()
        {
            AllStates = new Dictionary<Type, IState>
            {
                [typeof(InitSceneState)] = StateFactory.CreateState<InitSceneState>(),
                [typeof(SceneLoopState)] = StateFactory.CreateState<SceneLoopState>(),
                [typeof(EndSceneState)] = StateFactory.CreateState<EndSceneState>()
            };
            StartState<InitSceneState>();
        }

        public void Dispose()
        {
            foreach (var state in AllStates)
            {
                var target = state.Value as BaseSceneState;
                target?.Dispose();
            }
        }
    }
}