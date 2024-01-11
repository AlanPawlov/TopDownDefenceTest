using System.Threading.Tasks;
using Common.Events;
using Common.Events.Handlers;
using Common.UITemplate;
using Common.UITemplate.DefaultWidgets;
using UnityEngine;

namespace Game.UI.Windows
{
    public class HUDWindow : InterfaceWindow, IWallDamageHandler, IKillCharacterHandler
    {
        [SerializeField] private Transform _healthCounterContainer;
        [SerializeField] private Transform _enemyCounterContainer;
        private LabelWidget _healthCounter;
        private LabelWidget _enemyCounter;
        private int _remainingEnemies;

        public override async Task Init()
        {
            base.Init();
            _healthCounter =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabelWidget, _healthCounterContainer);
            _enemyCounter =
                await CreateChild<LabelWidget>(UIResourceMap.WidgetMap.DefaultLabelWidget, _enemyCounterContainer);
            EventBus.Subscribe(this);
        }

        public void SetData(int targetKill, int wallHealth)
        {
            _remainingEnemies = targetKill;
            UpdateEnemyCounter(targetKill);
            UpdateHealthCounter(wallHealth);
        }

        private void UpdateHealthCounter(int health)
        {
            _healthCounter?.SetData($"Health:{health}");
        }

        private void UpdateEnemyCounter(int remainingEnemies)
        {
            _enemyCounter?.SetData($"Enemies:{remainingEnemies}");
        }

        public void HandleWallDamage(int health)
        {
            UpdateHealthCounter(health);
        }

        public void HandleKillCharacter()
        {
            _remainingEnemies--;
            UpdateEnemyCounter(_remainingEnemies);
        }

        public override void Uninit()
        {
            EventBus.Unsubscribe(this);
            _healthCounter = null;
            _enemyCounter = null;
            _remainingEnemies = default;
            base.Uninit();
        }
    }
}