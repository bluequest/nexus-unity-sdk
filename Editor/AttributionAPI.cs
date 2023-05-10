//Example SDK For Nexus SDK Generator
/*  NOTE NOTE NOTE
*   GENERATED CODE
*   ANY CHANGES TO THIS FILE WILL BE OVERWRITTEN
*	PLEASE MAKE ANY CHANGES TO THE SDK TEMPLATES IN THE SDK GENERATOR
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace NexusSDK
{
    public static class AttributionAPI
    {
        public struct Transaction
        {
            public string creatorId { get; set; }

            public string currency { get; set; }

            public string description { get; set; }

            public string status { get; set; }

            public double subtotal { get; set; }

            public string transactionId { get; set; }

            public DateTime transactionDate { get; set; }

            public string playerId { get; set; }

            public DateTime playerLastPurchase { get; set; }

            public DateTime playerJoinDate { get; set; }

            public string playerName { get; set; }
        }

        public struct Creator
        {
            public string id { get; set; }

            public string name { get; set; }

            public string logoImage { get; set; }

            public string nexusUrl { get; set; }

            public string profileImage { get; set; }
        }

        public struct CreatorGroup
        {
            public string name { get; set; }

            public string id { get; set; }

            public string status { get; set; }
        }

        public struct GetCreatorsRequestParams
        {
            public int page { get; set; }

            public int pageSize { get; set; }

            public string groupId { get; set; }
        }

        public struct GetCreators200Response
        {
            public int currentPage { get; set; }

            public int currentPageSize { get; set; }

            public NexusSDK.AttributionAPI.Creator[] creators { get; set; }
        }

        public delegate void OnGetCreators200ResponseDelegate(NexusSDK.AttributionAPI.GetCreators200Response Param0);
        public static IEnumerator StartGetCreatorsRequest(GetCreatorsRequestParams RequestParams, OnGetCreators200ResponseDelegate ResponseCallback)
        {
            if (RequestParams.page > 9999)
            {
                yield break;
            }

            if (RequestParams.page < 1)
            {
                yield break;
            }

            if (RequestParams.pageSize > 100)
            {
                yield break;
            }

            if (RequestParams.pageSize < 1)
            {
                yield break;
            }

            string uri = "https://api.nexus.gg/v1/attributions/creators";
            List<string> parameterStrings = new List<string>{};
            parameterStrings.Add("page=" + RequestParams.page);
            parameterStrings.Add("pageSize=" + RequestParams.pageSize);
            if (RequestParams.groupId != "")
            {
                parameterStrings.Add("groupId=" + RequestParams.groupId);
            }

            uri += "?";
            uri += string.Join("&", parameterStrings);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.AttributionAPI.GetCreators200Response>(webRequest.downloadHandler.text);
                            ResponseCallback?.Invoke(callbackData0);
                        }

                        break;
                    default:
                        throw new Exception(); //TODO: Exception on error
                        break;
                }
            }
        }

        public struct GetCreatorByUuidRequestParams
        {
            public string creatorSlugOrId { get; set; }
        }

        public struct GetCreatorByUuid200Response
        {
            public struct Item0
            {
                public NexusSDK.AttributionAPI.CreatorGroup[] groups { get; set; }
            }

            public NexusSDK.AttributionAPI.GetCreatorByUuid200Response.Item0 PROP_Item0 { get; set; }

            public NexusSDK.AttributionAPI.Creator PROP_Creator { get; set; }
        }

        public delegate void OnGetCreatorByUuid200ResponseDelegate(NexusSDK.AttributionAPI.GetCreatorByUuid200Response Param0);
        public static IEnumerator StartGetCreatorByUuidRequest(GetCreatorByUuidRequestParams RequestParams, OnGetCreatorByUuid200ResponseDelegate ResponseCallback)
        {
            string uri = "https://api.nexus.gg/v1/attributions/creators/{creatorSlugOrId}";
            uri = uri.Replace("{creatorSlugOrId}", RequestParams.creatorSlugOrId);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.AttributionAPI.GetCreatorByUuid200Response>(webRequest.downloadHandler.text);
                            ResponseCallback?.Invoke(callbackData0);
                        }

                        break;
                    default:
                        throw new Exception(); //TODO: Exception on error
                        break;
                }
            }
        }

        public delegate void OnGetPing200ResponseDelegate();
        public static IEnumerator StartGetPingRequest(OnGetPing200ResponseDelegate ResponseCallback)
        {
            string uri = "https://api.nexus.gg/v1/attributions/ping";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback != null)
                        {
                            ResponseCallback?.Invoke();
                        }

                        break;
                    default:
                        throw new Exception(); //TODO: Exception on error
                        break;
                }
            }
        }
    }
}