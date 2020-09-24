using System;
using Backend.Responses;
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
        
        public void DoPost(string actionPath, object body, Action<PlayerActionResponse> onSuccess, Action<ErrorResponse> onError)
        {
            var request = UnityWebRequest.Put(envConfig.ApiUrl + actionPath, JsonUtility.ToJson(body));
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            var requestAsyncOperation = request.SendWebRequest();
            requestAsyncOperation.completed += operation =>
            {
                if (request.isNetworkError)
                {
                    onError?.Invoke(new ErrorResponse()
                    {
                        title = "Network error",
                        message = "Please check you internet connection and try again.",
                        httpStatus = 0
                    });
                }
                else if (request.isHttpError)
                {
                    onError?.Invoke(JsonConvert.DeserializeObject<ErrorResponse>(request.downloadHandler.text));
                }
                else
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    };
                    var response = JsonConvert.DeserializeObject<PlayerActionResponse>(request.downloadHandler.text, settings);
                    onSuccess?.Invoke(response);
                }
            };
        }
    }
}