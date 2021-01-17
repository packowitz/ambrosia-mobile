using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Models.Enums;
using Configs;
using ModestTree;
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

        public List<Property> Properties(PropertyType type, int level)
        {
            return properties[type].props.Where(prop => prop.level == level).ToList();
        }
        
        public int JewelValue(JewelType jewelType, int level)
        {
            Enum.TryParse($"{jewelType}_JEWEL", out PropertyType propType);
            var props = Properties(propType, level);

            if (!props.IsEmpty())
            {
                return props[0].value1;
            }
            return 0;
        }
        
        public int HeroMaxXp(int level)
        {
            var props = Properties(PropertyType.XP_MAX_HERO, level);
            return props.IsEmpty() ? 0 : props[0].value1;
        }
        
        public int HeroMergeXp(int level) {
            var props = Properties(PropertyType.MERGE_XP_HERO, level);
            return props.IsEmpty() ? 0 : props[0].value1;
        }

        public int HeroMaxAsc(int ascLevel) {
            var props = Properties(PropertyType.ASC_POINTS_MAX_HERO, ascLevel);
            return props.IsEmpty() ? 0 : props[0].value1;
        }

        public int HeroMergeAsc(int rarity) {
            var props = Properties(PropertyType.MERGE_ASC_HERO, rarity);
            return props.IsEmpty() ? 0 : props[0].value1;
        }
        
        public int VehicleStat(Vehicle vehicle, VehicleStat stat)
        {
            var parts = new List<VehiclePart>();
            if (vehicle.engine != null)
            {
                parts.Add(vehicle.engine);
            }
            if (vehicle.frame != null)
            {
                parts.Add(vehicle.frame);
            }
            if (vehicle.computer != null)
            {
                parts.Add(vehicle.computer);
            }
            if (vehicle.specialPart1 != null)
            {
                parts.Add(vehicle.specialPart1);
            }
            if (vehicle.specialPart2 != null)
            {
                parts.Add(vehicle.specialPart2);
            }
            if (vehicle.specialPart3 != null)
            {
                parts.Add(vehicle.specialPart3);
            }

            var sum = 0;
            parts.ForEach(part =>
            {
                var propType = (PropertyType) Enum.Parse(typeof(PropertyType), $"{part.type}_PART_{part.quality}");
                Properties(propType, part.level)
                    .Where(prop => prop.vehicleStat == stat)
                    .ToList()
                    .ForEach(prop => sum += prop.value1);
            });
            return sum;
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
            serverAPI.DoGet<List<Property>>("/properties/type/" + type + "/v/" + version, data =>
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