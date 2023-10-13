using Data;

namespace Editor.Common
{
    public class BaseEditorPage
    {
        protected GameData GameData;
        protected bool DataExist { get; set; } = false;

        public BaseEditorPage()
        {
        }

        public BaseEditorPage(GameData gameData)
        {
            GameData = gameData;
        }

        public virtual void LoadData()
        {
            DataExist = true;
        }

        public virtual void SaveData()
        {
        }
    }
}