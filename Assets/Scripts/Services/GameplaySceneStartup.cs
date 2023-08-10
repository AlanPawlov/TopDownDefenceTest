using Services.MatchStates;
using UnityEngine;
using Zenject;

namespace Services
{
    public class GameplaySceneStartup : MonoBehaviour, IInitializable
    {
        private MatchController _matchController;
        [SerializeField] private Collider2D[] _screenBorders;

        [Inject]
        public void Construct(MatchController matchController)
        {
            _matchController = matchController;
        }

        public void Initialize()
        {
            _matchController.Start();
            SetupCameraBorder();
        }

        private void SetupCameraBorder()
        {
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            var targetAnngle = new Vector3(0, 0, 90);

            _screenBorders[0].transform.position = new Vector3(leftBorder - _screenBorders[0].bounds.size.x / 2, 0, 0);
            _screenBorders[1].transform.position = new Vector3(rightBorder + _screenBorders[1].bounds.size.x / 2, 0, 0);
            _screenBorders[2].transform.position = new Vector3(0, topBorder + _screenBorders[0].bounds.size.x / 2, 0);
            _screenBorders[3].transform.position =
                new Vector3(0, bottomBorder - _screenBorders[0].bounds.size.x / 2, 0);

            _screenBorders[2].transform.rotation = Quaternion.Euler(targetAnngle);
            _screenBorders[3].transform.rotation = Quaternion.Euler(targetAnngle);
        }

        private void OnDestroy()
        {
            _matchController.Dispose();
            _matchController = null;
        }
    }
}