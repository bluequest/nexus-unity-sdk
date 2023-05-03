using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using static NexusUnitySDKRuntimeTests.AttributionAPITests;
using static NexusUnitySDKRuntimeTests.ReferralsAPITests;
using static UnityEngine.Networking.UnityWebRequest;

using Creator = NexusAPI_Attributions.Creator;
using Group = NexusAPI_Attributions.Group;

public class NexusUnitySDKRuntimeTests
{
	public class AttributionAPITests
	{
		public static string CreatorIdRef = new string("");

		[UnityTest]
		public IEnumerator NexusUnitySDK_A_PingAttributionsTest()
		{
			yield return new MonoBehaviourTest<PingAttributionsTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_B_GetCreatorsTest()
		{
			yield return new MonoBehaviourTest<GetCreatorsTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_C_GetCreatorDetailsByIdTest()
		{
			yield return new MonoBehaviourTest<GetCreatorDetailsByIdTest>();
		}

		public class PingAttributionsTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bPingAttributionsResponse = false;
			public bool IsTestFinished
			{
				get { return bPingAttributionsResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting PingAttributions...");
				StartCoroutine(NexusAPI_Attributions.PingAttributionsRequest((bWasSuccessful) =>
				{
					UnityEngine.Debug.Log("Received response of Success value: " + bWasSuccessful);
					Assert.AreEqual(bWasSuccessful, true);
					bPingAttributionsResponse = true;
				}));
			}
		}

		public class GetCreatorsTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetCreatorsResponse = false;
			public bool IsTestFinished
			{
				get { return bGetCreatorsResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting GetCreators...");
				NexusAPI_Attributions.GetCreatorsParameters GetCreatorsRequest = new NexusAPI_Attributions.GetCreatorsParameters(1, 100, "");
				StartCoroutine(NexusAPI_Attributions.GetCreatorsRequest(GetCreatorsRequest, (UnityWebRequestResult, GetCreatorsRequestResponse) =>
				{
					UnityEngine.Debug.Log("Received response of UnityWebRequestResult value: " + UnityWebRequestResult);
					Assert.AreEqual(UnityWebRequestResult, UnityWebRequest.Result.Success);

					UnityEngine.Debug.Log("currentPage value: " + GetCreatorsRequestResponse.currentPage);
					Assert.True(GetCreatorsRequestResponse.currentPage > 0);

					UnityEngine.Debug.Log("currentPageSize value: " + GetCreatorsRequestResponse.currentPageSize);
					Assert.True(GetCreatorsRequestResponse.currentPageSize > 0);

					// Test creators struct
					UnityEngine.Debug.Log("creators Array size: " + GetCreatorsRequestResponse.creators.Count);
					Assert.True(GetCreatorsRequestResponse.creators.Count > 0);

					foreach(Creator CreatorEntry in GetCreatorsRequestResponse.creators)
					{
						UnityEngine.Debug.Log("CreatorEntry id value: " + CreatorEntry.id);
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.id));

						// Save this ID later so we can use for GetCreatorDetailsByIdTest
						CreatorIdRef = CreatorEntry.id;

						UnityEngine.Debug.Log("CreatorEntry name value: " + CreatorEntry.name);
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.name));

						UnityEngine.Debug.Log("CreatorEntry logoImage value: " + CreatorEntry.logoImage);
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.logoImage));

						UnityEngine.Debug.Log("CreatorEntry nexusUrl value: " + CreatorEntry.nexusUrl);
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.nexusUrl));

						UnityEngine.Debug.Log("CreatorEntry profileImage value: " + CreatorEntry.profileImage);
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.profileImage));
					}

					bGetCreatorsResponse = true;
				}));
			}
		}

		public class GetCreatorDetailsByIdTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetCreatorDetailsByIdResponse = false;
			public bool IsTestFinished
			{
				get { return bGetCreatorDetailsByIdResponse; }
			}

			void Start()
			{
				NexusAPI_Attributions.CreatorByIdParameters GetCreatorDetailsByIdRequest = new NexusAPI_Attributions.CreatorByIdParameters(CreatorIdRef);
				UnityEngine.Debug.Log("Requesting GetCreatorDetailsById... Using id: " + GetCreatorDetailsByIdRequest.creatorSlugOrId);
				StartCoroutine(NexusAPI_Attributions.GetCreatorByIdRequest(GetCreatorDetailsByIdRequest, (UnityWebRequestResult, CreatorByIdResponse) =>
				{
					UnityEngine.Debug.Log("Received response of UnityWebRequestResult value: " + UnityWebRequestResult);
					Assert.AreEqual(UnityWebRequestResult, UnityWebRequest.Result.Success);

					UnityEngine.Debug.Log("id value: " + CreatorByIdResponse.id);
					Assert.True(!String.IsNullOrEmpty(CreatorByIdResponse.id));

					UnityEngine.Debug.Log("name value: " + CreatorByIdResponse.name);
					Assert.True(!String.IsNullOrEmpty(CreatorByIdResponse.name));

					UnityEngine.Debug.Log("logoImage value: " + CreatorByIdResponse.logoImage);
					Assert.True(!String.IsNullOrEmpty(CreatorByIdResponse.logoImage));

					UnityEngine.Debug.Log("nexusUrl value: " + CreatorByIdResponse.nexusUrl);
					Assert.True(!String.IsNullOrEmpty(CreatorByIdResponse.nexusUrl));

					UnityEngine.Debug.Log("profileImage value: " + CreatorByIdResponse.profileImage);
					Assert.True(!String.IsNullOrEmpty(CreatorByIdResponse.profileImage));

					// Test Group contents
					foreach(Group GroupEntry in CreatorByIdResponse.groups)
					{
						UnityEngine.Debug.Log("GroupEntry name value: " + GroupEntry.name);
						Assert.True(!String.IsNullOrEmpty(GroupEntry.name));

						UnityEngine.Debug.Log("GroupEntry id value: " + GroupEntry.id);
						Assert.True(!String.IsNullOrEmpty(GroupEntry.id));

						UnityEngine.Debug.Log("GroupEntry status value: " + GroupEntry.status);
						Assert.True(!String.IsNullOrEmpty(GroupEntry.status));
					}

					bGetCreatorDetailsByIdResponse = true;
				}));
			}
		}
	}

	public class ReferralsAPITests
	{
		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetReferralByPlayerIDTest()
		{
			yield return new MonoBehaviourTest<GetReferralByPlayerIDTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetReferralCodeForPlayerTest()
		{
			yield return new MonoBehaviourTest<GetReferralCodeForPlayerTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetReferralByCodeTest()
		{
			yield return new MonoBehaviourTest<GetReferralByCodeTest>();
		}

		public class GetReferralByPlayerIDTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetReferralByPlayerIDResponse = true; // #TODO init true as temp
			public bool IsTestFinished
			{
				get { return bGetReferralByPlayerIDResponse; }
			}
		}

		public class GetReferralCodeForPlayerTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetReferralCodeForPlayerResponse = true; // #TODO init true as temp
			public bool IsTestFinished
			{
				get { return bGetReferralCodeForPlayerResponse; }
			}
		}

		public class GetReferralByCodeTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetReferralByCodeResponse = true; // #TODO init true as temp
			public bool IsTestFinished
			{
				get { return bGetReferralByCodeResponse; }
			}
		}
	}

	public class BountiesAPITests
	{
		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetBountiesTest()
		{
			yield return new MonoBehaviourTest<GetBountiesTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetBountyByIdTest()
		{
			yield return new MonoBehaviourTest<GetBountyByIdTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetBountyProgressByCreatorIdTest()
		{
			yield return new MonoBehaviourTest<GetBountyProgressByCreatorIdTest>();
		}

		public class GetBountiesTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetBountiesResponse = true; // #TODO init true as temp
			public bool IsTestFinished
			{
				get { return bGetBountiesResponse; }
			}
		}

		public class GetBountyByIdTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetBountyByIdResponse = true; // #TODO init true as temp
			public bool IsTestFinished
			{
				get { return bGetBountyByIdResponse; }
			}
		}

		public class GetBountyProgressByCreatorIdTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetBountyProgressByCreatorIdResponse = true; // #TODO init true as temp
			public bool IsTestFinished
			{
				get { return bGetBountyProgressByCreatorIdResponse; }
			}
		}
	}
}
