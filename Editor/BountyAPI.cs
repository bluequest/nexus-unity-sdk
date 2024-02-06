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
    public static class BountyAPI
    {
        public struct Bounty
        {
            public string id { get; set; }

            public string name { get; set; }

            public string description { get; set; }

            public string imageSrc { get; set; }

            public string rewardDescription { get; set; }

            public double limit { get; set; }

            public DateTime startsAt { get; set; }

            public DateTime endsAt { get; set; }

            public DateTime lastProgressCheck { get; set; }

            public NexusSDK.BountyAPI.BountyObjective[] objectives { get; set; }

            public NexusSDK.BountyAPI.BountyReward[] rewards { get; set; }

            public struct dependants_Struct_Element
            {
                public string id { get; set; }

                public string name { get; set; }
            }

            public NexusSDK.BountyAPI.Bounty.dependants_Struct_Element[] dependants { get; set; }

            public struct prerequisites_Struct_Element
            {
                public string id { get; set; }

                public string name { get; set; }
            }

            public NexusSDK.BountyAPI.Bounty.prerequisites_Struct_Element[] prerequisites { get; set; }
        }

        public struct BountySku
        {
            public string id { get; set; }

            public string name { get; set; }

            public string slug { get; set; }
        }

        public struct BountyObjective
        {
            public string id { get; set; }

            public string name { get; set; }

            public string type { get; set; }

            public string condition { get; set; }

            public double count { get; set; }

            public string eventType { get; set; }

            public string eventCode { get; set; }

            public string nexusPurchaseObjectiveType { get; set; }

            public NexusSDK.BountyAPI.BountySku[] skus { get; set; }

            public struct category_Struct
            {
                public string id { get; set; }

                public string name { get; set; }

                public string slug { get; set; }
            }

            public NexusSDK.BountyAPI.BountyObjective.category_Struct category { get; set; }

            public struct publisher_Struct
            {
                public string id { get; set; }

                public string name { get; set; }
            }

            public NexusSDK.BountyAPI.BountyObjective.publisher_Struct publisher { get; set; }
        }

        public struct BountyReward
        {
            public string id { get; set; }

            public string name { get; set; }

            public string type { get; set; }

            public NexusSDK.BountyAPI.BountySku sku { get; set; }

            public double amount { get; set; }

            public string currency { get; set; }

            public string externalId { get; set; }
        }

        public struct BountyProgress
        {
            public string id { get; set; }

            public bool completed { get; set; }

            public double completionCount { get; set; }

            public DateTime lastProgressCheck { get; set; }

            public string currentObjectiveGroupId { get; set; }

            public NexusSDK.BountyAPI.BountyObjectiveProgress[] currentObjectives { get; set; }

            public struct completedObjectives_Struct_Element
            {
                public string objectiveGroupId { get; set; }

                public NexusSDK.BountyAPI.BountyObjectiveProgress[] objectives { get; set; }

                public NexusSDK.BountyAPI.BountyProgressReward[] rewards { get; set; }
            }

            public NexusSDK.BountyAPI.BountyProgress.completedObjectives_Struct_Element[] completedObjectives { get; set; }

            public struct member_Struct
            {
                public string id { get; set; }

                public string playerId { get; set; }

                public string name { get; set; }

                public NexusSDK.BountyAPI.Code[] codes { get; set; }
            }

            public NexusSDK.BountyAPI.BountyProgress.member_Struct member { get; set; }
        }

        public struct Code
        {
            public string code { get; set; }

            public bool isPrimary { get; set; }

            public bool isGenerated { get; set; }

            public bool isManaged { get; set; }
        }

        public struct BountyObjectiveProgress
        {
            public string id { get; set; }

            public bool completed { get; set; }

            public double count { get; set; }

            public struct objective_Struct
            {
                public string id { get; set; }

                public string name { get; set; }

                public double count { get; set; }

                public string condition { get; set; }
            }

            public NexusSDK.BountyAPI.BountyObjectiveProgress.objective_Struct objective { get; set; }
        }

        public struct BountyProgressReward
        {
            public string id { get; set; }

            public string name { get; set; }

            public string externalId { get; set; }

            public bool rewardCompleted { get; set; }

            public string rewardReferenceId { get; set; }
        }

        public struct Creator
        {
            public string id { get; set; }

            public string playerId { get; set; }

            public string name { get; set; }

            public string[] slugs { get; set; }
        }

        public struct CreatorGroupEvent
        {
            public string eventCode { get; set; }

            public string playerId { get; set; }

            public string referralCode { get; set; }
        }

        public enum Currency
        {
            AED,
            AFN,
            ALL,
            AMD,
            ANG,
            AOA,
            ARS,
            AUD,
            AWG,
            AZN,
            BAM,
            BBD,
            BDT,
            BGN,
            BHD,
            BIF,
            BMD,
            BND,
            BOB,
            BRL,
            BSD,
            BTC,
            BTN,
            BWP,
            BYN,
            BYR,
            BZD,
            CAD,
            CDF,
            CHF,
            CLF,
            CLP,
            CNY,
            COP,
            CRC,
            CUC,
            CUP,
            CVE,
            CZK,
            DJF,
            DKK,
            DOP,
            DZD,
            EGP,
            ERN,
            ETB,
            EUR,
            FJD,
            FKP,
            GBP,
            GEL,
            GGP,
            GHS,
            GIP,
            GMD,
            GNF,
            GTQ,
            GYD,
            HKD,
            HNL,
            HRK,
            HTG,
            HUF,
            IDR,
            ILS,
            IMP,
            INR,
            IQD,
            IRR,
            ISK,
            JEP,
            JMD,
            JOD,
            JPY,
            KES,
            KGS,
            KHR,
            KMF,
            KPW,
            KRW,
            KWD,
            KYD,
            KZT,
            LAK,
            LBP,
            LKR,
            LRD,
            LSL,
            LTL,
            LVL,
            LYD,
            MAD,
            MDL,
            MGA,
            MKD,
            MMK,
            MNT,
            MOP,
            MRO,
            MUR,
            MVR,
            MWK,
            MXN,
            MYR,
            MZN,
            NAD,
            NGN,
            NIO,
            NOK,
            NPR,
            NZD,
            OMR,
            PAB,
            PEN,
            PGK,
            PHP,
            PKR,
            PLN,
            PYG,
            QAR,
            RON,
            RSD,
            RUB,
            RWF,
            SAR,
            SBD,
            SCR,
            SDG,
            SEK,
            SGD,
            SHP,
            SLL,
            SOS,
            SRD,
            STD,
            SVC,
            SYP,
            SZL,
            THB,
            TJS,
            TMT,
            TND,
            TOP,
            TRY,
            TTD,
            TWD,
            TZS,
            UAH,
            UGX,
            USD,
            UYU,
            UZS,
            VEF,
            VND,
            VUV,
            WST,
            XAF,
            XAG,
            XAU,
            XCD,
            XDR,
            XOF,
            XPF,
            YER,
            ZAR,
            ZMK,
            ZMW,
            ZWL,
        }

        public struct BountyError
        {
            public string code { get; set; }

            public string message { get; set; }
        }

        public delegate void ErrorDelegate(long ErrorCode);
        public struct GetBountiesRequestParams
        {
            public string groupId { get; set; }

            public int page { get; set; }

            public int pageSize { get; set; }
        }

        public struct GetBounties200Response
        {
            public string groupId { get; set; }

            public string groupName { get; set; }

            public int currentPage { get; set; }

            public int currentPageSize { get; set; }

            public int totalCount { get; set; }

            public NexusSDK.BountyAPI.Bounty[] bounties { get; set; }
        }

        public delegate void OnGetBounties200ResponseDelegate(NexusSDK.BountyAPI.GetBounties200Response Param0);
        public delegate void OnGetBounties400ResponseDelegate(NexusSDK.BountyAPI.BountyError Param0);
        public struct GetBountiesResponseCallbacks
        {
            public OnGetBounties200ResponseDelegate OnGetBounties200Response { get; set; }

            public OnGetBounties400ResponseDelegate OnGetBounties400Response { get; set; }
        }

        public static IEnumerator StartGetBountiesRequest(GetBountiesRequestParams RequestParams, GetBountiesResponseCallbacks ResponseCallback, ErrorDelegate ErrorCallback)
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

            string uri = "https://api.nexus.gg/v1/bounties/";
            List<string> parameterStrings = new List<string>{};
            if (RequestParams.groupId != "")
            {
                parameterStrings.Add("groupId=" + RequestParams.groupId);
            }

            parameterStrings.Add("page=" + RequestParams.page);
            parameterStrings.Add("pageSize=" + RequestParams.pageSize);
            uri += "?";
            uri += string.Join("&", parameterStrings);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback.OnGetBounties200Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.BountyAPI.GetBounties200Response>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetBounties200Response(callbackData0);
                        }

                        break;
                    case 400:
                        if (ResponseCallback.OnGetBounties400Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.BountyAPI.BountyError>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetBounties400Response(callbackData0);
                        }

                        break;
                    default:
                        ErrorCallback?.Invoke(webRequest.responseCode);
                        break;
                }
            }
        }

        public struct GetBountyRequestParams
        {
            public string groupId { get; set; }

            public bool includeProgress { get; set; }

            public int page { get; set; }

            public int pageSize { get; set; }

            public string bountyId { get; set; }
        }

        public struct GetBounty200Response
        {
            public string groupId { get; set; }

            public string groupName { get; set; }

            public NexusSDK.BountyAPI.Bounty bounty { get; set; }

            public struct progress_Struct
            {
                public int currentPage { get; set; }

                public int currentPageSize { get; set; }

                public int totalCount { get; set; }

                public struct data_Struct_Element
                {
                    public string id { get; set; }

                    public bool completed { get; set; }

                    public double completionCount { get; set; }

                    public DateTime lastProgressCheck { get; set; }

                    public string currentObjectiveGroupId { get; set; }

                    public NexusSDK.BountyAPI.BountyObjectiveProgress[] currentObjectives { get; set; }

                    public struct completedObjectives_Struct_Element
                    {
                        public string objectiveGroupId { get; set; }

                        public NexusSDK.BountyAPI.BountyObjectiveProgress[] objectives { get; set; }

                        public NexusSDK.BountyAPI.BountyProgressReward[] rewards { get; set; }
                    }

                    public NexusSDK.BountyAPI.BountyProgress.completedObjectives_Struct_Element[] completedObjectives { get; set; }

                    public struct member_Struct
                    {
                        public string id { get; set; }

                        public string playerId { get; set; }

                        public string name { get; set; }

                        public NexusSDK.BountyAPI.Code[] codes { get; set; }
                    }

                    public NexusSDK.BountyAPI.BountyProgress.member_Struct member { get; set; }

                    public NexusSDK.BountyAPI.Creator creator { get; set; }
                }

                public NexusSDK.BountyAPI.GetBounty200Response.progress_Struct.data_Struct_Element[] data { get; set; }
            }

            public NexusSDK.BountyAPI.GetBounty200Response.progress_Struct progress { get; set; }
        }

        public delegate void OnGetBounty200ResponseDelegate(NexusSDK.BountyAPI.GetBounty200Response Param0);
        public delegate void OnGetBounty400ResponseDelegate(NexusSDK.BountyAPI.BountyError Param0);
        public struct GetBountyResponseCallbacks
        {
            public OnGetBounty200ResponseDelegate OnGetBounty200Response { get; set; }

            public OnGetBounty400ResponseDelegate OnGetBounty400Response { get; set; }
        }

        public static IEnumerator StartGetBountyRequest(GetBountyRequestParams RequestParams, GetBountyResponseCallbacks ResponseCallback, ErrorDelegate ErrorCallback)
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

            string uri = "https://api.nexus.gg/v1/bounties/{bountyId}";
            uri = uri.Replace("{bountyId}", RequestParams.bountyId);
            List<string> parameterStrings = new List<string>{};
            if (RequestParams.groupId != "")
            {
                parameterStrings.Add("groupId=" + RequestParams.groupId);
            }

            parameterStrings.Add("includeProgress=" + RequestParams.includeProgress);
            parameterStrings.Add("page=" + RequestParams.page);
            parameterStrings.Add("pageSize=" + RequestParams.pageSize);
            uri += "?";
            uri += string.Join("&", parameterStrings);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback.OnGetBounty200Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.BountyAPI.GetBounty200Response>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetBounty200Response(callbackData0);
                        }

                        break;
                    case 400:
                        if (ResponseCallback.OnGetBounty400Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.BountyAPI.BountyError>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetBounty400Response(callbackData0);
                        }

                        break;
                    default:
                        ErrorCallback?.Invoke(webRequest.responseCode);
                        break;
                }
            }
        }

        public struct GetMemberBountiesByIDRequestParams
        {
            public string groupId { get; set; }

            public int page { get; set; }

            public int pageSize { get; set; }

            public string memberId { get; set; }
        }

        public struct GetMemberBountiesByID200Response
        {
            public string groupId { get; set; }

            public string groupName { get; set; }

            public int currentPage { get; set; }

            public int currentPageSize { get; set; }

            public int totalCount { get; set; }

            public string memberId { get; set; }

            public string playerId { get; set; }

            public struct progress_Struct_Element
            {
                public string id { get; set; }

                public bool completed { get; set; }

                public double completionCount { get; set; }

                public DateTime lastProgressCheck { get; set; }

                public string currentObjectiveGroupId { get; set; }

                public NexusSDK.BountyAPI.BountyObjectiveProgress[] currentObjectives { get; set; }

                public struct completedObjectives_Struct_Element
                {
                    public string objectiveGroupId { get; set; }

                    public NexusSDK.BountyAPI.BountyObjectiveProgress[] objectives { get; set; }

                    public NexusSDK.BountyAPI.BountyProgressReward[] rewards { get; set; }
                }

                public NexusSDK.BountyAPI.BountyProgress.completedObjectives_Struct_Element[] completedObjectives { get; set; }

                public struct member_Struct
                {
                    public string id { get; set; }

                    public string playerId { get; set; }

                    public string name { get; set; }

                    public NexusSDK.BountyAPI.Code[] codes { get; set; }
                }

                public NexusSDK.BountyAPI.BountyProgress.member_Struct member { get; set; }

                public string name { get; set; }

                public double limit { get; set; }
            }

            public NexusSDK.BountyAPI.GetMemberBountiesByID200Response.progress_Struct_Element[] progress { get; set; }
        }

        public delegate void OnGetMemberBountiesByID200ResponseDelegate(NexusSDK.BountyAPI.GetMemberBountiesByID200Response Param0);
        public delegate void OnGetMemberBountiesByID400ResponseDelegate(NexusSDK.BountyAPI.BountyError Param0);
        public struct GetMemberBountiesByIDResponseCallbacks
        {
            public OnGetMemberBountiesByID200ResponseDelegate OnGetMemberBountiesByID200Response { get; set; }

            public OnGetMemberBountiesByID400ResponseDelegate OnGetMemberBountiesByID400Response { get; set; }
        }

        public static IEnumerator StartGetMemberBountiesByIDRequest(GetMemberBountiesByIDRequestParams RequestParams, GetMemberBountiesByIDResponseCallbacks ResponseCallback, ErrorDelegate ErrorCallback)
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

            string uri = "https://api.nexus.gg/v1/bounties/member/id/{memberId}";
            uri = uri.Replace("{memberId}", RequestParams.memberId);
            List<string> parameterStrings = new List<string>{};
            if (RequestParams.groupId != "")
            {
                parameterStrings.Add("groupId=" + RequestParams.groupId);
            }

            parameterStrings.Add("page=" + RequestParams.page);
            parameterStrings.Add("pageSize=" + RequestParams.pageSize);
            uri += "?";
            uri += string.Join("&", parameterStrings);
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
                yield return webRequest.SendWebRequest();
                switch (webRequest.responseCode)
                {
                    case 200:
                        if (ResponseCallback.OnGetMemberBountiesByID200Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.BountyAPI.GetMemberBountiesByID200Response>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetMemberBountiesByID200Response(callbackData0);
                        }

                        break;
                    case 400:
                        if (ResponseCallback.OnGetMemberBountiesByID400Response != null)
                        {
                            var callbackData0 = JsonConvert.DeserializeObject<NexusSDK.BountyAPI.BountyError>(webRequest.downloadHandler.text, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore});
                            ResponseCallback.OnGetMemberBountiesByID400Response(callbackData0);
                        }

                        break;
                    default:
                        ErrorCallback?.Invoke(webRequest.responseCode);
                        break;
                }
            }
        }
    }
}