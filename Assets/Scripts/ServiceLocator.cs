using System;
using System.Collections.Generic;
using UnityEngine;

namespace BallGame
{
    public class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public static void RegisterService<T>(T service) where T : class
        {
            var serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType))
            {
                _services.Add(serviceType, service);
            }
            else
            {
                Debug.LogWarning($"Service {serviceType} is already registered.");
            }
        }

        public static T GetService<T>() where T : class
        {
            var serviceType = typeof(T);
            if (_services.TryGetValue(serviceType, out var service))
            {
                return service as T;
            }
        
            Debug.LogError($"Service {serviceType} not found.");
            return null;
        }
    }
}