using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Configs;
using Newtonsoft.Json;
using UnityEngine;

namespace Backend.Services
{
    public class PropertyService
    {
        private readonly EnvConfig envConfig;
        private readonly ServerAPI serverAPI;
        private readonly Dictionary<PropertyType, PropertyCache> properties = new Dictionary<PropertyType, PropertyCache>();
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        private bool versionsChecked;
        private int waitingCalls;
        
        public PropertyService(ConfigsProvider configsProvider, ServerAPI serverAPI)
        {
            envConfig = configsProvider.Get<EnvConfig>();
            this.serverAPI = serverAPI;
        }

        public bool IsInitialized => versionsChecked && waitingCalls == 0;

        public Property[] GetProperties(PropertyType type, int level)
        {
            return properties[type].props.Where(prop => prop.level == level).ToArray();
        }

        public void CheckVersions()
        {
            serverAPI.DoGet<PropertyVersion[]>("/properties/versions", data =>
            {
                foreach (var propertyVersion in data)
                {
                    var cacheString = PlayerPrefs.GetString(StorageKey(propertyVersion.propertyType), "");
                    if (cacheString.Length > 0)
                    {
                        var cache = JsonConvert.DeserializeObject<PropertyCache>(cacheString, serializerSettings);
                        if (cache.version == propertyVersion.version)
                        {
                            properties.Add(cache.type, cache);
                        }
                        else
                        {
                            UpdateProperties(propertyVersion.propertyType, propertyVersion.version);
                        }
                    }
                    else
                    {
                        UpdateProperties(propertyVersion.propertyType, propertyVersion.version);
                    }
                }

                versionsChecked = true;
            });
        }

        private void UpdateProperties(PropertyType type, int version)
        {
            waitingCalls++;
            Debug.Log($"Updating properties of type {type} to version {version}");
            serverAPI.DoGet<Property[]>("/properties/type/" + type + "/v/" + version, data =>
            {
                waitingCalls--;
                var updatedCache = new PropertyCache 
                {
                   type = type,
                   version = version,
                   props = data
                };
                properties.Add(type, updatedCache);
                var json = JsonConvert.SerializeObject(updatedCache, serializerSettings);
                PlayerPrefs.SetString(StorageKey(type), json);
            });
        }

        private string StorageKey(PropertyType type)
        {
            return "Property." + type + (envConfig.IsLocal ? ".local" : "");
        }
    }
}