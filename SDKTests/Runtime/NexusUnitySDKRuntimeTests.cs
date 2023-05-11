using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using static NexusUnitySDKRuntimeTests.A_AttributionAPITests;
using static NexusUnitySDKRuntimeTests.B_ReferralsAPITests;
using static NexusUnitySDKRuntimeTests.C_BountiesAPITests;
using static UnityEngine.Networking.UnityWebRequest;

using Creator = NexusSDK.AttributionAPI.Creator;
using CreatorGroup = NexusSDK.AttributionAPI.CreatorGroup;

public class NexusUnitySDKRuntimeTests
{
	public class A_AttributionAPITests
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
				StartCoroutine(NexusSDK.AttributionAPI.StartGetPingRequest(() =>
				{
					UnityEngine.Debug.Log("Received successful response");
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
				NexusSDK.AttributionAPI.GetCreatorsRequestParams GetCreatorsRequest = new NexusSDK.AttributionAPI.GetCreatorsRequestParams();
				GetCreatorsRequest.page = 1;
				GetCreatorsRequest.pageSize = 100;
				StartCoroutine(NexusSDK.AttributionAPI.StartGetCreatorsRequest(GetCreatorsRequest, OnGetCreators200Response =>
				{
					UnityEngine.Debug.Log("Received successful response");

					//UnityEngine.Debug.Log("currentPage value: " + OnGetCreators200Response.currentPage);
					//UnityEngine.Debug.Log("creators Array size: " + OnGetCreators200Response.creators.Length);
					//UnityEngine.Debug.Log("currentPageSize value: " + OnGetCreators200Response.currentPageSize);

					Assert.True(OnGetCreators200Response.currentPage > 0);
					Assert.True(OnGetCreators200Response.currentPageSize > 0);
					Assert.True(OnGetCreators200Response.creators.Length > 0);

					// Test creators struct
					foreach (Creator CreatorEntry in OnGetCreators200Response.creators)
					{
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.id));
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.name));
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.logoImage));
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.nexusUrl));
						Assert.True(!String.IsNullOrEmpty(CreatorEntry.profileImage));

						// Save this ID later so we can use for GetCreatorDetailsByIdTest
						CreatorIdRef = CreatorEntry.id;

						//UnityEngine.Debug.Log("CreatorEntry id value: " + CreatorEntry.id);
						//UnityEngine.Debug.Log("CreatorEntry name value: " + CreatorEntry.name);
						//UnityEngine.Debug.Log("CreatorEntry logoImage value: " + CreatorEntry.logoImage);
						//UnityEngine.Debug.Log("CreatorEntry nexusUrl value: " + CreatorEntry.nexusUrl);
						//UnityEngine.Debug.Log("CreatorEntry profileImage value: " + CreatorEntry.profileImage);
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
				NexusSDK.AttributionAPI.GetCreatorByUuidRequestParams GetCreatorDetailsByIdRequest = new NexusSDK.AttributionAPI.GetCreatorByUuidRequestParams();
				GetCreatorDetailsByIdRequest.creatorSlugOrId = CreatorIdRef;
				UnityEngine.Debug.Log("Requesting GetCreatorDetailsById... using CreatorId: " + GetCreatorDetailsByIdRequest.creatorSlugOrId);
				StartCoroutine(NexusSDK.AttributionAPI.StartGetCreatorByUuidRequest(GetCreatorDetailsByIdRequest, OnGetCreatorByUuid200Response =>
				{
					UnityEngine.Debug.Log("Received successful response");

					// #TODO Test groups struct. However currently, OnGetCreatorByUuid200Response.PROP_Item0.group comes out as null

					//UnityEngine.Debug.Log("PROP_Item0.group's content: " + OnGetCreatorByUuid200Response.PROP_Item0.groups);
					//UnityEngine.Debug.Log("PROP_Creator's Id: " + OnGetCreatorByUuid200Response.PROP_Creator.id);

					//UnityEngine.Debug.Log("creators Array size: " + OnGetCreatorByUuid200Response.PROP_Item0.groups.Length);
					//Assert.True(OnGetCreatorByUuid200Response.PROP_Item0.groups.Length > 0);

					/*
					foreach (CreatorGroup CreatorGroupEntry in OnGetCreatorByUuid200Response.PROP_Item0.groups)
					{
						//UnityEngine.Debug.Log("CreatorGroupEntry name value: " + CreatorGroupEntry.name);
						//UnityEngine.Debug.Log("CreatorGroupEntry id value: " + CreatorGroupEntry.id);
						//UnityEngine.Debug.Log("CreatorGroupEntry status value: " + CreatorGroupEntry.status);

						Assert.True(!String.IsNullOrEmpty(CreatorGroupEntry.name));
						Assert.True(!String.IsNullOrEmpty(CreatorGroupEntry.id));
						Assert.True(!String.IsNullOrEmpty(CreatorGroupEntry.status));

						// Save this ID later so we can use for GetCreatorDetailsByIdTest
						CreatorIdRef = CreatorGroupEntry.id;
					}
					*/

					bGetCreatorDetailsByIdResponse = true;
				}));
			}
		}
	}

	public class B_ReferralsAPITests
	{
		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetReferralByPlayerIDTest()
		{
			yield return new MonoBehaviourTest<GetReferralByPlayerIDTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_B_GetReferralCodeForPlayerTest()
		{
			yield return new MonoBehaviourTest<GetReferralCodeForPlayerTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_C_GetReferralByCodeTest()
		{
			yield return new MonoBehaviourTest<GetReferralByCodeTest>();
		}

		public class GetReferralByPlayerIDTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetReferralByPlayerIDResponse = false;
			public bool IsTestFinished
			{
				get { return bGetReferralByPlayerIDResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting GetReferralByPlayerID using CreatorId: " + CreatorIdRef + "...");

				NexusSDK.ReferralsAPI.GetReferralInfoByPlayerIdRequestParams GetReferralInfoByPlayerIdRequest = new NexusSDK.ReferralsAPI.GetReferralInfoByPlayerIdRequestParams();
				GetReferralInfoByPlayerIdRequest.playerId = CreatorIdRef;
				GetReferralInfoByPlayerIdRequest.page = 1;
				GetReferralInfoByPlayerIdRequest.pageSize = 100;
				StartCoroutine(NexusSDK.ReferralsAPI.StartGetReferralInfoByPlayerIdRequest(GetReferralInfoByPlayerIdRequest, new NexusSDK.ReferralsAPI.GetReferralInfoByPlayerIdResponseCallbacks()
				{
					OnGetReferralInfoByPlayerId200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");
						bGetReferralByPlayerIDResponse = true;
					},
					OnGetReferralInfoByPlayerId400Response = (FailureResponse) =>
					{
						UnityEngine.Debug.Log("Received failed response with code: '" + FailureResponse.code + "' and message: '" + FailureResponse.message + "'");
						bGetReferralByPlayerIDResponse = true;
					}
				}));
			}
		}

		public class GetReferralCodeForPlayerTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetReferralCodeForPlayerResponse = false;
			public bool IsTestFinished
			{
				get { return bGetReferralCodeForPlayerResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting GetPlayerCurrentReferral using CreatorId: " + CreatorIdRef + "...");

				NexusSDK.ReferralsAPI.GetPlayerCurrentReferralRequestParams GetPlayerCurrentReferralRequest = new NexusSDK.ReferralsAPI.GetPlayerCurrentReferralRequestParams();
				GetPlayerCurrentReferralRequest.playerId = CreatorIdRef;
				StartCoroutine(NexusSDK.ReferralsAPI.StartGetPlayerCurrentReferralRequest(GetPlayerCurrentReferralRequest, new NexusSDK.ReferralsAPI.GetPlayerCurrentReferralResponseCallbacks()
				{
					OnGetPlayerCurrentReferral200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");
						bGetReferralCodeForPlayerResponse = true;
					},
					OnGetPlayerCurrentReferral404Response = (FailureResponse) =>
					{
						UnityEngine.Debug.Log("Received failed response with code: '" + FailureResponse.code + "'");
						bGetReferralCodeForPlayerResponse = true;
					}
				}));
			}
		}

		public class GetReferralByCodeTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetReferralByCodeResponse = false;
			public bool IsTestFinished
			{
				get { return bGetReferralByCodeResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting GetReferralByCode using code: 9jd7RBR_B2BZ0uWO3_lgv");

				NexusSDK.ReferralsAPI.GetReferralInfoByCodeRequestParams GetReferralInfoByCodeRequest = new NexusSDK.ReferralsAPI.GetReferralInfoByCodeRequestParams();
				GetReferralInfoByCodeRequest.code = "9jd7RBR_B2BZ0uWO3_lgv"; // #TODO use expected successful code
				GetReferralInfoByCodeRequest.page = 1;
				GetReferralInfoByCodeRequest.pageSize = 100;
				StartCoroutine(NexusSDK.ReferralsAPI.StartGetReferralInfoByCodeRequest(GetReferralInfoByCodeRequest, new NexusSDK.ReferralsAPI.GetReferralInfoByCodeResponseCallbacks()
				{
					OnGetReferralInfoByCode200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");
						bGetReferralByCodeResponse = true;
					},
					OnGetReferralInfoByCode400Response = (FailureResponse) =>
					{
						UnityEngine.Debug.Log("Received failed response with code: '" + FailureResponse.code + "' and message: '" + FailureResponse.message + "'");
						bGetReferralByCodeResponse = true;
					}
				}));
			}
		}
	}

	public class C_BountiesAPITests
	{
		[UnityTest]
		public IEnumerator NexusUnitySDK_A_GetBountiesTest()
		{
			yield return new MonoBehaviourTest<GetBountiesTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_B_GetBountyByIdTest()
		{
			yield return new MonoBehaviourTest<GetBountyByIdTest>();
		}

		[UnityTest]
		public IEnumerator NexusUnitySDK_C_GetBountyProgressByCreatorIdTest()
		{
			yield return new MonoBehaviourTest<GetBountyProgressByCreatorIdTest>();
		}

		public class GetBountiesTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetBountiesResponse = false;
			public bool IsTestFinished
			{
				get { return bGetBountiesResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting GetBounties...");

				NexusSDK.BountyAPI.GetBountiesRequestParams GetBountiesRequest = new NexusSDK.BountyAPI.GetBountiesRequestParams();
				GetBountiesRequest.page = 1;
				GetBountiesRequest.pageSize = 100;
				StartCoroutine(NexusSDK.BountyAPI.StartGetBountiesRequest(GetBountiesRequest, new NexusSDK.BountyAPI.GetBountiesResponseCallbacks()
				{
					OnGetBounties200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");
						bGetBountiesResponse = true;

						Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupId));
						Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));
						Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));
						Assert.True(SuccessResponse.bounties.Length > 0);

						foreach (NexusSDK.BountyAPI.Bounty bounty in SuccessResponse.bounties)
						{
							Assert.True(!String.IsNullOrEmpty(bounty.id));
							Assert.True(!String.IsNullOrEmpty(bounty.name));
							Assert.True(!String.IsNullOrEmpty(bounty.description));
							Assert.True(!String.IsNullOrEmpty(bounty.imageSrc));
							Assert.True(!String.IsNullOrEmpty(bounty.rewardDescription));

							Assert.True(bounty.limit > 0);
						}
					},
					OnGetBounties400Response = (FailureResponse) =>
					{
						UnityEngine.Debug.Log("Received failed response with code: '" + FailureResponse.code + "' and message: '" + FailureResponse.message + "'");
						bGetBountiesResponse = true;
					}
				}));
			}
		}

		public class GetBountyByIdTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetBountyByIdResponse = false;
			public bool IsTestFinished
			{
				get { return bGetBountyByIdResponse; }
			}
			void Start()
			{
				UnityEngine.Debug.Log("Requesting GetBountyById...");

				NexusSDK.BountyAPI.GetBountyRequestParams GetBountyRequest = new NexusSDK.BountyAPI.GetBountyRequestParams();
				GetBountyRequest.groupId = "CTyLYzlOC0VPsWAhk949n"; // #TODO use group id ref
				GetBountyRequest.includeProgress = false;
				GetBountyRequest.page = 1;
				GetBountyRequest.pageSize = 100;
				StartCoroutine(NexusSDK.BountyAPI.StartGetBountyRequest(GetBountyRequest, new NexusSDK.BountyAPI.GetBountyResponseCallbacks()
				{
					OnGetBounty200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");
						bGetBountyByIdResponse = true;
					},
					OnGetBounty400Response = (FailureResponse) =>
					{
						UnityEngine.Debug.Log("Received failed response with code: '" + FailureResponse.code + "' and message: '" + FailureResponse.message + "'");
						bGetBountyByIdResponse = true;
					}
				}));
			}
		}

		public class GetBountyProgressByCreatorIdTest : MonoBehaviour, IMonoBehaviourTest
		{
			private bool bGetBountyProgressByCreatorIdResponse = false;
			public bool IsTestFinished
			{
				get { return bGetBountyProgressByCreatorIdResponse; }
			}

			void Start()
			{
				UnityEngine.Debug.Log("Requesting BountyProgressByCreatorId... using CreatorId: " + CreatorIdRef + "...");

				NexusSDK.BountyAPI.GetCreatorBountiesByIDRequestParams CreatorBountiesByIDRequest = new NexusSDK.BountyAPI.GetCreatorBountiesByIDRequestParams();
				CreatorBountiesByIDRequest.creatorId = CreatorIdRef;
				CreatorBountiesByIDRequest.page = 1;
				CreatorBountiesByIDRequest.pageSize = 100;
				StartCoroutine(NexusSDK.BountyAPI.StartGetCreatorBountiesByIDRequest(CreatorBountiesByIDRequest, new NexusSDK.BountyAPI.GetCreatorBountiesByIDResponseCallbacks()
				{
					OnGetCreatorBountiesByID200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");
						bGetBountyProgressByCreatorIdResponse = true;
					},
					OnGetCreatorBountiesByID400Response = (FailureResponse) =>
					{
						UnityEngine.Debug.Log("Received failed response with code: '" + FailureResponse.code + "' and message: '" + FailureResponse.message + "'");
						bGetBountyProgressByCreatorIdResponse = true;
					}
				}));
			}
		}
	}
}
