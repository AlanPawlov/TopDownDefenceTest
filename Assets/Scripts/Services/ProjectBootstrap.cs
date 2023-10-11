using States.GameStates;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ProjectBootstrap : MonoBehaviour, IInitializable
    {
        private ProjectStateMachine _stateMachine;

        [Inject]
        public void Construct(ProjectStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _stateMachine.StartState<LoadProgressState>();
        }
    }
}