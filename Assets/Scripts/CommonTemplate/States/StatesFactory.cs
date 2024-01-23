using Zenject;

namespace CommonTemplate.States
{
    public class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public T CreateState<T>() where T : IState
        {
            return _container.Resolve<T>();
        }
    }
}