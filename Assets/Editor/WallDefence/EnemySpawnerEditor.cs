using Common.Data;
using Editor.Common;
using Game.Models;
using Sirenix.OdinInspector;

namespace Editor.WallDefence
{
    [HideReferenceObjectPicker]
    public class EnemySpawnerEditor : BaseModelEditor<EnemySpawnerModel>
    {
        public EnemySpawnerEditor()
        {
        }

        public EnemySpawnerEditor(EnemySpawnerModel model, GameData gameData)
        {
            model ??= new EnemySpawnerModel();
            Model = model; 
        }

        [ShowInInspector]
        public string Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }

        [ShowInInspector]
        public float MaxSpawnTimeout
        {
            get => Model.MaxSpawnTimeout;
            set => Model.MaxSpawnTimeout = value;
        }

        [ShowInInspector]
        public float MinSpawnTimeout
        {
            get => Model.MinSpawnTimeout;
            set => Model.MinSpawnTimeout = value;
        }
    }
}