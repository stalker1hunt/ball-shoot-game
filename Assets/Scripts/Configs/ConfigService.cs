using System.Collections.Generic;
using UnityEngine;

namespace BallGame.Configs
{
    public class ConfigService
    {
        private Dictionary<string, ScriptableObject> _configs;

        public ConfigService()
        {
            _configs = new Dictionary<string, ScriptableObject>();
            LoadConfigs();
        }

        private void LoadConfigs()
        {
            var configs = Resources.LoadAll<ScriptableObject>("Configs");
            foreach (var config in configs)
            {
                _configs[config.name] = config;
            }
        }

        public T GetConfig<T>(string key) where T : ScriptableObject
        {
            if (_configs.TryGetValue(key, out ScriptableObject config))
            {
                return config as T;
            }
            Debug.LogError("Config with key \"" + key + "\" not found.");
            return null;
        }
    }
}