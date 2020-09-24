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
        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public ServerAPI(ConfigsProvider configsProvider)
        {
            envConfig = configsProvider.Get<EnvConfig>();
        }

        public void DoGet<T>(string path, Action<T> onSuccess = null, Action<ErrorResponse> onError = null)
        {
            var url = envConfig.ApiUrl + (path.StartsWith("/") ? path : "/" + path);
            var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Accept", "application/json");
            // TODO add user token if exist
            var requestAsyncOperation = request.SendWebRequest();
            requestAsyncOperation.completed += operation =>
            {
                if (request.isNetworkError)
                {
                    HandleNetworkError(onError);
                }
                else if (request.isHttpError)
                {
                    HandleHttpError(JsonConvert.DeserializeObject<ErrorResponse>(request.downloadHandler.text), onError);
                }
                else
                {
                    onSuccess?.Invoke(JsonConvert.DeserializeObject<T>(request.downloadHandler.text, serializerSettings));
                }
            };
        }

        public void DoPost(string path, object body, Action<PlayerActionResponse> onSuccess = null,
            Action<ErrorResponse> onError = null)
        {
            var url = envConfig.ApiUrl + (path.StartsWith("/") ? path : "/" + path);
            var request = UnityWebRequest.Put(url, JsonUtility.ToJson(body));
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");
            // TODO add user token if exist
            var requestAsyncOperation = request.SendWebRequest();
            requestAsyncOperation.completed += operation =>
            {
                if (request.isNetworkError)
                {
                    HandleNetworkError(onError);
                }
                else if (request.isHttpError)
                {
                    HandleHttpError(JsonConvert.DeserializeObject<ErrorResponse>(request.downloadHandler.text), onError);
                }
                else
                {
                    onSuccess?.Invoke(
                        JsonConvert.DeserializeObject<PlayerActionResponse>(request.downloadHandler.text, serializerSettings));
                }
            };
        }

        private void HandleNetworkError(Action<ErrorResponse> handler)
        {
            if (handler != null)
            {
                handler.Invoke(new ErrorResponse()
                {
                    title = "Network error",
                    message = "Please check you internet connection and try again.",
                    httpStatus = 0
                });
            }
            else
            {
                Debug.Log("TODO show network error without explicit handling");
            }
        }

        private void HandleHttpError(ErrorResponse error, Action<ErrorResponse> handler)
        {
            if (handler != null)
            {
                handler.Invoke(error);
            }
            else
            {
                if (error.httpStatus == 401)
                {
                    Debug.Log("TODO handle unauthorized: delete token and send user to login screen");
                }
                else
                {
                    Debug.Log("TODO show http error without explicit handling");
                }
            }
        }
    }
}