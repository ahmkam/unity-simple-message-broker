using System.Reflection;
using System;
using System.Collections.Generic;

namespace AhmKam
{
    public class SimpleMessageBroker
    {
        private static readonly Dictionary<string, Dictionary<string, Dictionary<int, SubscriberInfo>>> messageBrokerMap =
        new Dictionary<string, Dictionary<string, Dictionary<int, SubscriberInfo>>>();
        private const string NoParamsKey = "#no-params-key";

        public static void Subscribe(string id, Action callback)
        {
            string typeKey = NoParamsKey;
            if (!messageBrokerMap.ContainsKey(typeKey))
            {
                messageBrokerMap.Add(typeKey, new Dictionary<string, Dictionary<int, SubscriberInfo>>());
            }

            if (!messageBrokerMap[typeKey].ContainsKey(id))
            {
                messageBrokerMap[typeKey].Add(id, new Dictionary<int, SubscriberInfo>());
            }

            int callbackHashCode = callback.GetHashCode();
            if (!messageBrokerMap[typeKey][id].ContainsKey(callbackHashCode))
            {
                var subData = new SubscriberInfo()
                {
                    target = callback.Target,
                    methodCallback = callback.Method
                };
                messageBrokerMap[typeKey][id].Add(callbackHashCode, subData);
            }
        }

        public static void Subscribe<T>(string id, Action<T> callback) => SubscribeInteral<T>(id, callback);

        public static void Subscribe<T>(Action<T> callback) => SubscribeInteral<T>(String.Empty, callback);

        private static void SubscribeInteral<T>(string id, Action<T> callback)
        {
            string typeKey = typeof(T).ToString();
            if (!messageBrokerMap.ContainsKey(typeKey))
            {
                messageBrokerMap.Add(typeKey, new Dictionary<string, Dictionary<int, SubscriberInfo>>());
            }

            if (!messageBrokerMap[typeKey].ContainsKey(id))
            {
                messageBrokerMap[typeKey].Add(id, new Dictionary<int, SubscriberInfo>());
            }

            int callbackHashCode = callback.GetHashCode();
            if (!messageBrokerMap[typeKey][id].ContainsKey(callbackHashCode))
            {
                var subData = new SubscriberInfo()
                {
                    target = callback.Target,
                    methodCallback = callback.Method
                };

                messageBrokerMap[typeKey][id].Add(callbackHashCode, subData);
            }
        }

        public static void Publish(string id)
        {
            string typeKey = NoParamsKey;
            if (messageBrokerMap.ContainsKey(typeKey))
            {
                if (messageBrokerMap[typeKey].ContainsKey(id))
                {
                    Dictionary<int, SubscriberInfo> subscribers = messageBrokerMap[typeKey][id];
                    foreach (var item in subscribers.Values)
                    {
                        item.methodCallback.Invoke(item.target, null);
                    }
                }
            }
        }

        public static void Publish<T>(string id, T data) => PublishInternal<T>(id, data);

        public static void Publish<T>(T data) => PublishInternal<T>(String.Empty, data);

        private static void PublishInternal<T>(string id, T data)
        {
            string typeKey = typeof(T).ToString();
            if (messageBrokerMap.ContainsKey(typeKey))
            {
                if (messageBrokerMap[typeKey].ContainsKey(id))
                {
                    Dictionary<int, SubscriberInfo> subscribers = messageBrokerMap[typeKey][id];
                    foreach (var item in subscribers.Values)
                    {
                        item.methodCallback.Invoke(item.target, new object[] { data });
                    }
                }
            }
        }

        public static void Unsubscribe(string id, Action callback)
        {
            string typeKey = NoParamsKey;
            if (messageBrokerMap.ContainsKey(typeKey))
            {
                if (messageBrokerMap[typeKey].ContainsKey(id))
                {
                    int callbackHashCode = callback.GetHashCode();
                    if (messageBrokerMap[typeKey][id].ContainsKey(callbackHashCode))
                    {
                        messageBrokerMap[typeKey][id].Remove(callbackHashCode);
                    }
                }
            }
        }

        public static void Unsubscribe<T>(string id, Action<T> callback) => UnsubscribeInternal<T>(id, callback);

        public static void Unsubscribe<T>(Action<T> callback) => UnsubscribeInternal<T>(String.Empty, callback);

        private static void UnsubscribeInternal<T>(string id, Action<T> callback)
        {
            string typeKey = typeof(T).ToString();
            if (messageBrokerMap.ContainsKey(typeKey))
            {
                if (messageBrokerMap[typeKey].ContainsKey(id))
                {
                    int callbackHashCode = callback.GetHashCode();
                    if (messageBrokerMap[typeKey][id].ContainsKey(callbackHashCode))
                    {
                        messageBrokerMap[typeKey][id].Remove(callbackHashCode);
                    }
                }
            }
        }
    }

    internal class SubscriberInfo
    {
        public object target;
        public MethodInfo methodCallback;
    }
}
