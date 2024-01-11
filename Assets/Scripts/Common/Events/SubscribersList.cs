using System.Collections.Generic;

namespace Common.Events
{
    public class SubscribersList<TSubscriber> where TSubscriber : class
    {
        public bool Executing;
        private bool _needsCleanUp = false;
        public readonly List<TSubscriber> List = new List<TSubscriber>();
        private List<TSubscriber> _needCleanElements = new List<TSubscriber>();
        
        public void Add(TSubscriber subscriber)
        {
            List.Add(subscriber);
        }

        public void Remove(TSubscriber subscriber)
        {
            if (Executing)
            {
                var i = List.IndexOf(subscriber);
                if (i >= 0)
                {
                    _needsCleanUp = true;
                    _needCleanElements.Add(subscriber);
                }
            }
            else
            {
                List.Remove(subscriber);
            }
        }

        public void Cleanup()
        {
            if (!_needsCleanUp)
            {
                return;
            }

            foreach (var item in _needCleanElements)
            {
                List.Remove(item);
            }
            _needCleanElements.Clear();
            _needsCleanUp = false;
        }
    }
}