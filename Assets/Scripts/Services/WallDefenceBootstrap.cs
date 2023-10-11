using States.MatchStates;
using UnityEngine;
using Zenject;

namespace Services
{
    public class WallDefenceBootstrap : MonoBehaviour
    {
        private MatchStateMachine _stateMachine;
        public MatchStateMachine StateMachine => _stateMachine;
        
        [Inject]
        public void Construct(MatchStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}