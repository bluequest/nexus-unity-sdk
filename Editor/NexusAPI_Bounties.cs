using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class NexusAPI_ : MonoBehaviour
{

    public class InvalidGroupError
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Bounty
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageSrc { get; set; }
        public string rewardDescription { get; set; }
        public int limit { get; set; }
        public DateTime startsAt { get; set; }
        public DateTime endsAt { get; set; }
        public DateTime lastProgressCheck { get; set; }
        public List<Objective> objectives { get; set; }
        public List<Reward> rewards { get; set; }
        public List<Dependant> dependants { get; set; }
        public List<Prerequisite> prerequisites { get; set; }
    }

    public class Category
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

    public class Dependant
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Objective
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string condition { get; set; }
        public int count { get; set; }
        public string eventType { get; set; }
        public string eventCode { get; set; }
        public string nexusPurchaseObjectiveType { get; set; }
        public List<Sku> skus { get; set; }
        public Category category { get; set; }
        public Publisher publisher { get; set; }
    }

    public class Prerequisite
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Publisher
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Reward
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Sku sku { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string externalId { get; set; }
    }

    public class BountyListRequest
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public int currentPage { get; set; }
        public int currentPageSize { get; set; }
        public int totalCount { get; set; }
        public List<Bounty> bounties { get; set; }
    }

    public class Sku
    {
        public string id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

    public void GetBounties() {
        StartCoroutine(GetBountiesRequest("https://api.nexus.gg/v1/bounties/"));
    }
    IEnumerator GetBountiesRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
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
                    Debug.LogError(String.Format("Success: {0}", webRequest.error));
                    BountyListRequest bountyListRequest = JsonConvert.DeserializeObject<BountyListRequest>(webRequest.downloadHandler.text);
                    break;
            }
        }
    }



    // public class CompletedObjective
    // {
    //     public string objectiveGroupId { get; set; }
    //     public List<object> objectives { get; set; }
    //     public List<Reward> rewards { get; set; }
    // }

    // public class Creator
    // {
    //     public string id { get; set; }
    //     public string playerId { get; set; }
    //     public string name { get; set; }
    //     public List<string> slugs { get; set; }
    // }

    // public class CurrentObjective
    // {
    //     public string id { get; set; }
    //     public bool completed { get; set; }
    //     public int count { get; set; }
    //     public Objective objective { get; set; }
    // }

    // public class Datum
    // {
    //     public string id { get; set; }
    //     public bool completed { get; set; }
    //     public int completionCount { get; set; }
    //     public DateTime lastProgressCheck { get; set; }
    //     public string currentObjectiveGroupId { get; set; }
    //     public List<CurrentObjective> currentObjectives { get; set; }
    //     public List<CompletedObjective> completedObjectives { get; set; }
    //     public Creator creator { get; set; }
    // }

    // public class Objective
    // {
    //     public string id { get; set; }
    //     public string name { get; set; }
    //     public string type { get; set; }
    //     public string condition { get; set; }
    //     public int count { get; set; }
    //     public string eventType { get; set; }
    //     public string eventCode { get; set; }
    //     public string nexusPurchaseObjectiveType { get; set; }
    //     public List<Sku> skus { get; set; }
    //     public Category category { get; set; }
    //     public Publisher publisher { get; set; }
    // }

    // public class Progress
    // {
    //     public int currentPage { get; set; }
    //     public int currentPageSize { get; set; }
    //     public int totalCount { get; set; }
    //     public List<Datum> data { get; set; }
    // }

    // public class Reward
    // {
    //     public string id { get; set; }
    //     public string name { get; set; }
    //     public string type { get; set; }
    //     public Sku sku { get; set; }
    //     public int amount { get; set; }
    //     public string currency { get; set; }
    //     public string externalId { get; set; }
    //     public bool rewardCompleted { get; set; }
    //     public string rewardReferenceId { get; set; }
    // }

    // public class Root
    // {
    //     public string groupId { get; set; }
    //     public string groupName { get; set; }
    //     public Bounty bounty { get; set; }
    //     public Progress progress { get; set; }
    // }

    // public void GetBountyById(string bountyId) {
    //     StartCoroutine(GetBountyByIdRequest("https://api.nexus.gg/v1/bounties/", bountyId));
    // }
    // IEnumerator GetBountyByIdRequest(string uri, string bountyId)
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + bountyId))
    //     {
    //         webRequest.SetRequestHeader("x-shared-secret", APIKey);
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
    //                 Debug.LogError(String.Format("Success: {0}", webRequest.error));
    //                 BountyListRequest bountyListRequest = JsonConvert.DeserializeObject<BountyListRequest>(webRequest.downloadHandler.text);
    //                 break;
    //         }
    //     }
    // }




    // public class Bounty
    // {
    //     public string id { get; set; }
    //     public string name { get; set; }
    // }

    // public class CompletedObjective
    // {
    //     public string objectiveGroupId { get; set; }
    //     public List<Objective> objectives { get; set; }
    // }

    // public class CurrentObjective
    // {
    //     public string id { get; set; }
    //     public bool completed { get; set; }
    //     public int count { get; set; }
    //     public Objective objective { get; set; }
    // }

    // public class Objective
    // {
    //     public string id { get; set; }
    //     public string name { get; set; }
    //     public int count { get; set; }
    //     public string condition { get; set; }
    // }

    // public class Progress
    // {
    //     public string id { get; set; }
    //     public bool completed { get; set; }
    //     public int completionCount { get; set; }
    //     public string currentObjectiveGroupId { get; set; }
    //     public List<CurrentObjective> currentObjectives { get; set; }
    //     public List<CompletedObjective> completedObjectives { get; set; }
    //     public List<Reward> rewards { get; set; }
    //     public Bounty bounty { get; set; }
    // }

    // public class Reward
    // {
    //     public string id { get; set; }
    //     public string name { get; set; }
    //     public string externalId { get; set; }
    //     public bool rewardCompleted { get; set; }
    //     public string rewardReferenceId { get; set; }
    // }

    // public class Root
    // {
    //     public string groupId { get; set; }
    //     public string groupName { get; set; }
    //     public int currentPage { get; set; }
    //     public int currentPageSize { get; set; }
    //     public int totalCount { get; set; }
    //     public string creatorId { get; set; }
    //     public string playerId { get; set; }
    //     public List<Progress> progress { get; set; }
    // }

    // public void GetBountyProgressByCreatorId(string creatorId) {
    //     StartCoroutine(GetBountyProgressByCreatorIdRequest("https://api.nexus.gg/v1/bounties/creator/id/", creatorId));
    // }
    // IEnumerator GetBountyProgressByCreatorIdRequest(string uri, string creatorId)
    // {
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + creatorId))
    //     {
    //         webRequest.SetRequestHeader("x-shared-secret", APIKey);
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
    //                 Debug.LogError(String.Format("Success: {0}", webRequest.error));
    //                 BountyListRequest bountyListRequest = JsonConvert.DeserializeObject<BountyListRequest>(webRequest.downloadHandler.text);
    //                 break;
    //         }
    //     }
    // }

}
