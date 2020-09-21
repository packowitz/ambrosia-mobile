using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    public class ConfigsProvider
    {
        private Dictionary<string, ScriptableObject> cache;

        public ConfigsProvider()
        {
            cache = new Dictionary<string, ScriptableObject>();
        }
        
        public T Get<T>() where T : ScriptableObject
        {
            var configName = typeof(T).Name;
            return Get<T>(configName);
        }

        public T Get<T>(string configPath) where T : ScriptableObject
        {
            if (cache.TryGetValue(configPath, out var cachedConfig))
            {
                return cachedConfig as T;
            }
            
            var config = Resources.Load(configPath) as T;
            cache[configPath] = config;
            return config;
        }
    }
}