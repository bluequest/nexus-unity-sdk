using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;



public class NexusAPI_Referrals : MonoBehaviour
{
    public Button referralByPlayerIdButton;
    public Button referralCodeByPlayerIdButton;
    public Button referralInfoByCodeButton;
    

    // void OnEnable()
    // {
    //     //Register Button Events
    //     referralByPlayerIdButton.onClick.AddListener(() => GetReferralByPlayerID("dusty", ""));
    // }

    // void OnDisable()
    // {
    //     //Un-Register Button Events
    //     referralByPlayerIdButton.onClick.RemoveAllListeners();
    // }


    public class InvalidGroupError
    {
        public string code { get; set; }
        public string message { get; set; }
    }


    public class Referral
    {
        public string id { get; set; }
        public string code { get; set; }
        public string playerId { get; set; }
        public string playerName { get; set; }
        public DateTime referralDate { get; set; }
    }
    public class ReferralCode
    {
        public string code { get; set; }
        public bool isPrimary { get; set; }
        public bool isGenerated { get; set; }
        public bool isManaged { get; set; }
    }
    public class ReferralByPlayerIDRequest
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public List<ReferralCode> referralCodes { get; set; }
        public string playerId { get; set; }
        public int currentPage { get; set; }
        public int currentPageSize { get; set; }
        public int totalCount { get; set; }
        public List<Referral> referrals { get; set; }
    }

    public void GetReferralByPlayerID(string playerId, string groupId) {
        StartCoroutine(GetReferralByPlayerIDRequest("https://api.nexus.gg/v1/referrals/player/", playerId, groupId));
    }
    IEnumerator GetReferralByPlayerIDRequest(string uri, string playerId, string groupId)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + playerId))
        {
            webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(String.Format("Connection Error: {0}", webRequest.error));
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(String.Format("Data Processing Error: {0}", webRequest.error));
                    InvalidGroupError invalidGroupError = JsonConvert.DeserializeObject<InvalidGroupError>(webRequest.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    print(String.Format("Success: {0}", webRequest.error));
                    print(webRequest.downloadHandler.text);
                    // ReferralByPlayerIDRequest referralByPlayerIDRequest = JsonConvert.DeserializeObject<ReferralByPlayerIDRequest>(webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    // public void GetReferralCodeForPlayer(string playerId, string referralCode) {
    //     StartCoroutine(GetReferralCodeForPlayerRequest("https://api.nexus.gg/v1/referrals/player/", playerId, referralCode));
    // }
    // IEnumerator GetReferralCodeForPlayerRequest(string uri, string playerId, string referralCode)
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + playerId + "/code"))
    //     {
    //         webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
    //         yield return webRequest.SendWebRequest();

    //         switch (webRequest.result)
    //         {
    //             case UnityWebRequest.Result.ConnectionError:
    //                 Debug.LogError(String.Format("Connection Error: {0}", webRequest.error));
    //                 break;
    //             case UnityWebRequest.Result.DataProcessingError:
    //                 Debug.LogError(String.Format("Data Processing Error: {0}", webRequest.error));
    //                 InvalidGroupError invalidGroupError = JsonConvert.DeserializeObject<InvalidGroupError>(webRequest.downloadHandler.text);
    //                 break;
    //             case UnityWebRequest.Result.Success:
    //                 print(String.Format("Success: {0}", webRequest.error));
    //                 print(webRequest.downloadHandler.text);
    //                 string code = webRequest.downloadHandler.text;
    //                 break;
    //         }
    //     }
    // }



    // public void GetReferralByCode(string referralCode, string groupId) {
    //     StartCoroutine(GetReferralByCodeRequest("https://api.nexus.gg/v1/referrals/", referralCode, groupId));
    // }
    // IEnumerator GetReferralByCodeRequest(string uri, string referralCode, string groupId)
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + referralCode))
    //     {
    //         webRequest.SetRequestHeader("x-shared-secret", APIKeyContainer.APIKey);
    //         yield return webRequest.SendWebRequest();

    //         switch (webRequest.result)
    //         {
    //             case UnityWebRequest.Result.ConnectionError:
    //                 Debug.LogError(String.Format("Connection Error: {0}", webRequest.error));
    //                 break;
    //             case UnityWebRequest.Result.DataProcessingError:
    //                 Debug.LogError(String.Format("Data Processing Error: {0}", webRequest.error));
    //                 InvalidGroupError invalidGroupError = JsonConvert.DeserializeObject<InvalidGroupError>(webRequest.downloadHandler.text);
    //                 break;
    //             case UnityWebRequest.Result.Success:
    //                 print(String.Format("Success: {0}", webRequest.error));
    //                 print(webRequest.downloadHandler.text);
    //                 // ReferralByPlayerIDRequest referralByPlayerIDRequest = JsonConvert.DeserializeObject<ReferralByPlayerIDRequest>(webRequest.downloadHandler.text);
    //                 break;
    //         }
    //     }
    // }
}
