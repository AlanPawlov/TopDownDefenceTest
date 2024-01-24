using CommonTemplate.Data;
using Editor.Common;
using Game.Models;
using Sirenix.OdinInspector;

namespace Editor.WallDefence
{
    [HideReferenceObjectPicker]
    public class WallEditor : BaseModelEditor<WallModel>
    {
        public WallEditor()
        {
        }

        public WallEditor(WallModel model, GameData gameData)
        {
            model ??= new WallModel();
            Model = model;
        }

        [ShowInInspector]
        public string Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }

        [ShowInInspector]
        public int WallHealth
        {
            get => Model.Health;
            set => Model.Health = value;
        }
    }
}