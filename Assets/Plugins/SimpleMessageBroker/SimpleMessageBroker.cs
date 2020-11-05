using System.Reflection;
using System;
using System.Collections.Generic;

namespace AhmKam
{
    public class SimpleMessageBroker
    {
        private static readonly Dictionary<string, Dictionary<int, SubscriberInfo>> messageBrokerMap =
        new Dictionary<string, Dictionary<int, SubscriberInfo>>();

        public static void Subscribe<T>(Action<T> callback)
        {
            string key = typeof(T).ToString();
            if (!messageBrokerMap.ContainsKey(key))
            {
                messageBrokerMap.Add(key, new Dictionary<int, SubscriberInfo>());
            }

            int callbackHashCode = callback.GetHashCode();
            if (!messageBrokerMap[key].ContainsKey(callbackHashCode))
            {
                var subData = new SubscriberInfo()
                {
                    target = callback.Target,
                    methodCallback = callback.Method
                };
                messageBrokerMap[key].Add(callbackHashCode, subData);
            }
        }

        public static void Publish<T>(T data)
        {
            string key = typeof(T).ToString();
            if (messageBrokerMap.ContainsKey(key))
            {
                Dictionary<int, SubscriberInfo> subscribers = messageBrokerMap[key];
                foreach (var item in subscribers.Values)
                {
                    item.methodCallback.Invoke(item.target, new object[] { data });
                }
            }
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).ToString();
            if (messageBrokerMap.ContainsKey(key))
            {
                int callbackHashCode = callback.GetHashCode();
                messageBrokerMap[key].Remove(callbackHashCode);
            }
        }
    }

    internal class SubscriberInfo
    {
        public object target;
        public MethodInfo methodCallback;
    }
}
