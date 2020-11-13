using System;
using Backend.Responses;
using Backend.Signal;
using Configs;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace Backend
{
    public class ServerAPI
    {
        private readonly string tokenKey;
        private string userToken;
        
        private readonly SignalBus signalBus;
        private readonly EnvConfig envConfig;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        
        public ServerAPI(ConfigsProvider configsProvider, SignalBus signalBus)
        {
            envConfig = configsProvider.Get<EnvConfig>();
            tokenKey = "ServerAPI.playerToken" + (envConfig.IsLocal ? ".local" : "");
            this.signalBus = signalBus;
            this.signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (!string.IsNullOrEmpty(signal.Data.token))
                {
                    UserToken = signal.Data.token;
                }
            });
        }
        
        public bool IsLoggedIn => !string.IsNullOrEmpty(UserToken);

        private string UserToken
        {
            get
            {
                if (string.IsNullOrEmpty(userToken))
                {
                    userToken = PlayerPrefs.GetString(tokenKey, null);
                }
                return userToken;
            }
            set
            {
                userToken = value;
                PlayerPrefs.SetString(tokenKey, userToken);
            }
        }

        private void AddGenericHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("Accept", "application/json");

            if (!string.IsNullOrEmpty(UserToken))
            {
                request.SetRequestHeader("Authorization", $"Bearer {UserToken}");
            }
        }

        public void DoGet<T>(string path, Action<T> onSuccess = null, Action<ErrorResponse> onError = null)
        {
            var url = envConfig.ApiUrl + (path.StartsWith("/") ? path : "/" + path);
            var request = UnityWebRequest.Get(url);
            AddGenericHeaders(request);
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

        public void DoPost(string path, object body = null, Action<PlayerActionResponse> onSuccess = null,
            Action<ErrorResponse> onError = null)
        {
            var url = envConfig.ApiUrl + (path.StartsWith("/") ? path : "/" + path);
            var bodyData = body != null ? JsonConvert.SerializeObject(body) : "{}";
            var request = UnityWebRequest.Put(url, bodyData);
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            AddGenericHeaders(request);
            Debug.Log($"POST: {url}\nBody: {bodyData}");
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
                    var responseModel = JsonConvert.DeserializeObject<PlayerActionResponse>(request.downloadHandler.text, serializerSettings);
                    signalBus.Fire(new PlayerActionSignal(responseModel));
                    onSuccess?.Invoke(responseModel);
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
                    Debug.LogError(error.title);
                    Debug.LogError(error.message);
                }
            }
        }
    }
}