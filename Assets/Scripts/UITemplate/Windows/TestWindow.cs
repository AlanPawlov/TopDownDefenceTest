using System.Threading.Tasks;
using UI.Widgets;
using UnityEngine;

namespace UI.Windows
{
    public class TestWindow : InterfaceWindow
    {
        [SerializeField] private Transform _testButtonContainer;
        private ButtonWidget _test;

        public override async Task Init()
        {
            base.Init();
            _test = await CreateChild<ButtonWidget>("Widgets/TestButton", _testButtonContainer);
            _test.SetData("Тестовая кнопка");
            _test.SetData(() => Uninit());
        }

        public override void Uninit()
        {
            _test = null;
            base.Uninit();
        }
    }
}