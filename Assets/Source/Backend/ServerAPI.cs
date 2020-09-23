using System;
using Configs;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Backend
{
    public class ServerAPI
    {
        private EnvConfig envConfig;
        
        public ServerAPI(ConfigsProvider configsProvider)
        {
            envConfig = configsProvider.Get<EnvConfig>();
        }
        
        public void DoPost<T>(string actionPath, object body, Action<T> onSuccess, Action<string> onError)
        {
            var request = UnityWebRequest.Put(envConfig.ApiUrl + actionPath, JsonUtility.ToJson(body));
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            var requestAsyncOperation = request.SendWebRequest();
            requestAsyncOperation.completed += operation =>
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var response = JsonConvert.DeserializeObject<T>(request.downloadHandler.text, settings);
                onSuccess?.Invoke(response);
            };
        }
    }
}