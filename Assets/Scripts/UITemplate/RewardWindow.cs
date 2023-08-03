using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RewardWindow : InterfaceWindow
    {
        [SerializeField]
        private TMP_Text _headerText;
        [SerializeField]
        private TMP_Text _rewardsText;
        [SerializeField]
        private Transform _rewardPanelPivot;
        [SerializeField]
        private Transform _buttonOkPivot;
    
        public override async Task Init()
        {
            base.Init();
            // var okButton = CreateChild<WidgetButton>( UIResourcesMap.GreenButtonWidget, _buttonOkPivot );
            // var okText = Managers.GetLocalizationManager().GetLocalizationString( Constants.UIStrings.CommonOk, Constants.UIStrings.CommonOk );
            // okButton.SetData( Close );
            // okButton.SetData( okText );
        }

        public void SetData( string headerText)
        {
            _headerText.text = headerText;
            // var rewardText = Managers.GetLocalizationManager().GetLocalizationString( Constants.UIStrings.CommonRewards, Constants.UIStrings.CommonRewards );
            // _rewardsText.text = rewardText;
            // var rewardsPanel = CreateChild<WidgetRewardsPanel>( UIResourcesMap.RewardsPanel, _rewardPanelPivot );
            // rewardsPanel.SetRewards( rewards, true, true, showBack: false );
        }

        public override void Uninit()
        {
            _headerText.text = default;
            _rewardsText.text = default;
            base.Uninit();
        }
    }
}