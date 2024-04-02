using System;
using System.Collections.Generic;

namespace Initialization
{
    public class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service) where T : class
        {
            services[typeof(T)] = service;
        }

        public static T GetService<T>() where T : class
        {
            return (T)services[typeof(T)];
        }
    }
}