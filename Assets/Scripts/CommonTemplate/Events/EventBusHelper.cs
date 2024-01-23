using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonTemplate.Events
{
    public static class EventBusHelper
    {
        private static Dictionary<Type, List<Type>> _cashedSubscriberTypes =
            new Dictionary<Type, List<Type>>();

        public static List<Type> GetSubscriberTypes(IGlobalSubscriber globalSubscriber)
        {
            var type = globalSubscriber.GetType();
            if (_cashedSubscriberTypes.ContainsKey(type))
            {
                return _cashedSubscriberTypes[type];
            }

            var subscriberTypes = type
                .GetInterfaces()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(IGlobalSubscriber)))
                .ToList();

            _cashedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }
    }
}