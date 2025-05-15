using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _listeners = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
                _listeners[type] = new List<Delegate>();
            
            if (!_listeners[type].Contains(callback))
                _listeners[type].Add(callback);
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (_listeners.ContainsKey(type))
                _listeners[type].Remove(callback);
        }

        public static void Publish<T>(T evt)
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type)) return;
            
            foreach (var callback in _listeners[type])
            {
                (callback as Action<T>)?.Invoke(evt);
            }
        }
    }
}