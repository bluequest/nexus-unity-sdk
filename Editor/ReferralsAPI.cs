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
    public static class ReferralsAPI
    {
        public struct Referral
        {
            public string id { get; set; }

            public string code { get; set; }

            public string playerId { get; set; }

            public string playerName { get; set; }

            public DateTime referralDate { get; set; }
        }

        public struct ReferralError
        {
            public string code { get; set; }

            public string message { get; set; }
        }

        public struct ReferralCodeResponse
        {
            public string code { get; set; }

            public bool isPrimary { get; set; }

            public bool isGenerated { get; set; }

            public bool isManaged { get; set; }
        }

        public struct GetReferralInfoByPlayerIdRequestParams
        {
            public string playerId { get; set; }

            public string groupId { get; set; }

            public int page { get; set; }

            public int pageSize { get; set; }

            public bool excludeReferralList { get; set; }
        }

        public struct GetReferralInfoByPlayerId200Response
        {
            public string groupId { get; set; }

            public string groupName { get; set; }

            public NexusSDK.ReferralsAPI.ReferralCodeResponse[] referralCodes { get; set; }

            public string playerId { get; set; }

            public int currentPage { get; set; }

            public int currentPageSize { get; set; }

            public int totalCount { get; set; }

            public NexusSDK.ReferralsAPI.Referral[] referrals { get; set; }
        }

        public delegate void OnGetReferralInfoByPlayerId200ResponseDelegate(NexusSDK.ReferralsAPI.GetReferralInfoByPlayerId200Response Param0);
        public delegate void OnGetReferralInfoByPlayerId400ResponseDelegate(NexusSDK.ReferralsAPI.ReferralError Param0);
        public struct GetReferralInfoByPlayerIdResponseCallbacks
        {
            public OnGetReferralInfoByPlayerId200ResponseDelegate OnGetReferralInfoByPlayerId200Response { get; set; }

            public OnGetReferralInfoByPlayerId400ResponseDelegate OnGetReferralInfoByPlayerId400Response { get; set; }
        }

        public static IEnumerator StartGetReferralInfoByPlayerIdRequest(GetReferralInfoByPlayerIdRequestParams RequestParams, GetReferralInfoByPlayerIdResponseCallbacks ResponseCallback)
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

            string uri = "https://api.nexus.gg/v1/referrals/player/{playerId}";
            uri = uri.Replace("{playerId}", RequestParams.playerId);
            List<string> parameterStrings = new List<string>{};
            if (RequestParams.groupId != "")
            {
                parameterStrings.Add("groupId=" + RequestParams.groupId);
            }

            parameterStrings.Add("page=" + RequestParams.page);
            parameterStrings.Add("pageSize=" + RequestParams.pageSize);
            parameterStrings.Add("excludeReferralList=" + RequestParams.excludeReferralList);
            uri += "?";
            uri += string.Join("&", parameterStrings);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback.OnGetReferralInfoByPlayerId200Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.ReferralsAPI.GetReferralInfoByPlayerId200Response>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetReferralInfoByPlayerId200Response(callbackData0);
                        }

                        break;
                    case 400:
                        if (ResponseCallback.OnGetReferralInfoByPlayerId400Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.ReferralsAPI.ReferralError>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetReferralInfoByPlayerId400Response(callbackData0);
                        }

                        break;
                    default:
                        throw new Exception(); //TODO: Exception on error
                        break;
                }
            }
        }

        public struct GetPlayerCurrentReferralRequestParams
        {
            public string playerId { get; set; }

            public string groupId { get; set; }
        }

        public delegate void OnGetPlayerCurrentReferral200ResponseDelegate(string Param0);
        public struct GetPlayerCurrentReferral404Response
        {
            public string code { get; set; }
        }

        public delegate void OnGetPlayerCurrentReferral404ResponseDelegate(NexusSDK.ReferralsAPI.GetPlayerCurrentReferral404Response Param0);
        public struct GetPlayerCurrentReferralResponseCallbacks
        {
            public OnGetPlayerCurrentReferral200ResponseDelegate OnGetPlayerCurrentReferral200Response { get; set; }

            public OnGetPlayerCurrentReferral404ResponseDelegate OnGetPlayerCurrentReferral404Response { get; set; }
        }

        public static IEnumerator StartGetPlayerCurrentReferralRequest(GetPlayerCurrentReferralRequestParams RequestParams, GetPlayerCurrentReferralResponseCallbacks ResponseCallback)
        {
            string uri = "https://api.nexus.gg/v1/referrals/player/{playerId}/code";
            uri = uri.Replace("{playerId}", RequestParams.playerId);
            List<string> parameterStrings = new List<string>{};
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
                        if (ResponseCallback.OnGetPlayerCurrentReferral200Response != null)
                        {
                            var callbackData0 = webRequest.downloadHandler.text;
                            ResponseCallback.OnGetPlayerCurrentReferral200Response(callbackData0);
                        }

                        break;
                    case 404:
                        if (ResponseCallback.OnGetPlayerCurrentReferral404Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.ReferralsAPI.GetPlayerCurrentReferral404Response>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetPlayerCurrentReferral404Response(callbackData0);
                        }

                        break;
                    default:
                        throw new Exception(); //TODO: Exception on error
                        break;
                }
            }
        }

        public struct GetReferralInfoByCodeRequestParams
        {
            public string code { get; set; }

            public string groupId { get; set; }

            public int page { get; set; }

            public int pageSize { get; set; }

            public bool excludeReferralList { get; set; }
        }

        public struct GetReferralInfoByCode200Response
        {
            public string groupId { get; set; }

            public string groupName { get; set; }

            public NexusSDK.ReferralsAPI.ReferralCodeResponse[] referralCodes { get; set; }

            public string playerId { get; set; }

            public int currentPage { get; set; }

            public int currentPageSize { get; set; }

            public int totalCount { get; set; }

            public NexusSDK.ReferralsAPI.Referral[] referrals { get; set; }
        }

        public delegate void OnGetReferralInfoByCode200ResponseDelegate(NexusSDK.ReferralsAPI.GetReferralInfoByCode200Response Param0);
        public delegate void OnGetReferralInfoByCode400ResponseDelegate(NexusSDK.ReferralsAPI.ReferralError Param0);
        public struct GetReferralInfoByCodeResponseCallbacks
        {
            public OnGetReferralInfoByCode200ResponseDelegate OnGetReferralInfoByCode200Response { get; set; }

            public OnGetReferralInfoByCode400ResponseDelegate OnGetReferralInfoByCode400Response { get; set; }
        }

        public static IEnumerator StartGetReferralInfoByCodeRequest(GetReferralInfoByCodeRequestParams RequestParams, GetReferralInfoByCodeResponseCallbacks ResponseCallback)
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

            string uri = "https://api.nexus.gg/v1/referrals/code/{code}";
            uri = uri.Replace("{code}", RequestParams.code);
            List<string> parameterStrings = new List<string>{};
            if (RequestParams.groupId != "")
            {
                parameterStrings.Add("groupId=" + RequestParams.groupId);
            }

            parameterStrings.Add("page=" + RequestParams.page);
            parameterStrings.Add("pageSize=" + RequestParams.pageSize);
            parameterStrings.Add("excludeReferralList=" + RequestParams.excludeReferralList);
            uri += "?";
            uri += string.Join("&", parameterStrings);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback.OnGetReferralInfoByCode200Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.ReferralsAPI.GetReferralInfoByCode200Response>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetReferralInfoByCode200Response(callbackData0);
                        }

                        break;
                    case 400:
                        if (ResponseCallback.OnGetReferralInfoByCode400Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.ReferralsAPI.ReferralError>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetReferralInfoByCode400Response(callbackData0);
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