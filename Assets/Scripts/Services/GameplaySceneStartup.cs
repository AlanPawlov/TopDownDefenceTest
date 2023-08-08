using UnityEngine;
using Zenject;

namespace Services
{
    public class GameplaySceneStartup : MonoBehaviour, IInitializable
    {
        private MatchController _matchController;

        [Inject]
        public void Construct(MatchController matchController)
        {
            _matchController = matchController;
        }

        public void Initialize()
        {
            _matchController.Start();
        }
    }
}