namespace Editor.Common
{
    public class BaseModelEditor<T>
    {
        protected T Model;

        public T GetModel()
        {
            return Model;
        }
    }
}