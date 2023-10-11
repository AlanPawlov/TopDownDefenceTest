using Services;
using States.MatchStates;
using UnityEngine;

namespace States.GameStates
{
    public class GameState : IState
    {
        public GameState(ProjectStateMachine stateMachine)
        {
        }

        public void StopState()
        {
        }

        public void StartState()
        {
            var wallDefenceBootstrap = Object.FindObjectOfType<WallDefenceBootstrap>();
            wallDefenceBootstrap.StateMachine.StartState<InitMatchState>();
        }
    }
}