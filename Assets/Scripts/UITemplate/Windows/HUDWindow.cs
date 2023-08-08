using System.Threading.Tasks;
using Events;
using Events.Handlers;
using UI.Widgets;
using UnityEngine;

namespace UI.Windows
{
    public class HUDWindow : InterfaceWindow, IWallDamageHandler, IKillCharacterHandler
    {
        [SerializeField] private Transform _healthCounterContainer;
        [SerializeField] private Transform _enemyCounterContainer;
        private LabelWidget _healthCounter;
        private LabelWidget _enemyCounter;
        private int _remainingEnemies = 5;
        private int _maxEnemies = 5;

        public override async Task Init()
        {
            base.Init();
            _healthCounter =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabelWidget, _healthCounterContainer);
            _enemyCounter =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabelWidget, _enemyCounterContainer);
            EventBus.Subscribe(this);
        }

        private void UpdateHealthCounter(int health)
        {
            _healthCounter?.SetData($"Health:{health}");
        }

        private void UpdateEnemyCounter()
        {
            _remainingEnemies--;
            _enemyCounter?.SetData($"Enemies:{_remainingEnemies}");
        }

        public void HandleWallDamage(int health)
        {
            UpdateHealthCounter(health);
        }

        public void HandleKillCharacter()
        {
            UpdateEnemyCounter();
        }

        public override void Uninit()
        {
            EventBus.Unsubscribe(this);
            _remainingEnemies = _maxEnemies;
            _healthCounter = null;
            _enemyCounter = null;
            base.Uninit();
        }
    }
}