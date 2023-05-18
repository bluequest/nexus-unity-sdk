using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static NexusUnitySDKRuntimeTests.A_AttributionAPITests;
using static NexusUnitySDKRuntimeTests.B_ReferralsAPITests;
using static NexusUnitySDKRuntimeTests.C_BountiesAPITests;
using static UnityEngine.Networking.UnityWebRequest;

using Creator = NexusSDK.AttributionAPI.Creator;
using CreatorGroup = NexusSDK.AttributionAPI.CreatorGroup;
public class NexusUnitySDKRuntimeTests
{
	/*
	 * Use the bTestContent flag to test validity of the content retrieved from a successful response.
	 * If set to false, only test if a response has been received using the respective SDK call.
	 */
	static bool bTestContent = false;
	public class A_AttributionAPITests
	{
		public static string CreatorIdRef = new string("");
		public static string GroupIdRef = new string("");

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

					if (bTestContent == true)
					{
						Assert.True(OnGetCreatorByUuid200Response.groups.Length > 0);
						UnityEngine.Debug.Log("OnGetCreatorByUuid200Response.groups value: " + OnGetCreatorByUuid200Response.groups);
						foreach (CreatorGroup CreatorGroupEntry in OnGetCreatorByUuid200Response.groups)
						{
							Assert.True(!String.IsNullOrEmpty(CreatorGroupEntry.name));
							Assert.True(!String.IsNullOrEmpty(CreatorGroupEntry.id));
							Assert.True(!String.IsNullOrEmpty(CreatorGroupEntry.status));

							// Save this ID later so we can use for GetCreatorDetailsByIdTest
							CreatorIdRef = CreatorGroupEntry.id;
						}

						Assert.True(!String.IsNullOrEmpty(OnGetCreatorByUuid200Response.id));
						Assert.True(!String.IsNullOrEmpty(OnGetCreatorByUuid200Response.name));
						Assert.True(!String.IsNullOrEmpty(OnGetCreatorByUuid200Response.logoImage));
						Assert.True(!String.IsNullOrEmpty(OnGetCreatorByUuid200Response.nexusUrl));
						Assert.True(!String.IsNullOrEmpty(OnGetCreatorByUuid200Response.profileImage));
					}

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
						Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupId));
						Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));

						foreach (NexusSDK.ReferralsAPI.ReferralCodeResponse referralCode in SuccessResponse.referralCodes)
						{
							Assert.True(!String.IsNullOrEmpty(referralCode.code));
						}

						Assert.True(!String.IsNullOrEmpty(SuccessResponse.playerId));
						Assert.True(SuccessResponse.currentPage > 0);
						Assert.True(SuccessResponse.currentPageSize > 0);
						Assert.True(SuccessResponse.totalCount > 0);

						foreach (NexusSDK.ReferralsAPI.Referral referral in SuccessResponse.referrals)
						{
							Assert.True(!String.IsNullOrEmpty(referral.id));
							Assert.True(!String.IsNullOrEmpty(referral.code));
							Assert.True(!String.IsNullOrEmpty(referral.playerId));
							Assert.True(!String.IsNullOrEmpty(referral.playerName));
							Assert.True(!String.IsNullOrEmpty(referral.referralDate.ToString()));
						}

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

						if (bTestContent == true)
						{
							// test referral code				
							Assert.True(!String.IsNullOrEmpty(SuccessResponse));
						}

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

						if (bTestContent == true)
						{
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupId));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));

							foreach (NexusSDK.ReferralsAPI.ReferralCodeResponse referralCode in SuccessResponse.referralCodes)
							{
								Assert.True(!String.IsNullOrEmpty(referralCode.code));
							}

							Assert.True(!String.IsNullOrEmpty(SuccessResponse.playerId));
							Assert.True(SuccessResponse.currentPage > 0);
							Assert.True(SuccessResponse.currentPageSize > 0);
							Assert.True(SuccessResponse.totalCount > 0);

							foreach (NexusSDK.ReferralsAPI.Referral referral in SuccessResponse.referrals)
							{
								Assert.True(!String.IsNullOrEmpty(referral.id));
								Assert.True(!String.IsNullOrEmpty(referral.code));
								Assert.True(!String.IsNullOrEmpty(referral.playerId));
								Assert.True(!String.IsNullOrEmpty(referral.playerName));
								Assert.True(!String.IsNullOrEmpty(referral.referralDate.ToString()));
							}
						}

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

						if (bTestContent == true)
						{
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupId));
							// Save group id for GetBountyByIdTest
							GroupIdRef = SuccessResponse.groupId;
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));
							Assert.True(SuccessResponse.bounties.Length > -1); // amount of bounties could be 0

							foreach (NexusSDK.BountyAPI.Bounty bounty in SuccessResponse.bounties)
							{
								Assert.True(!String.IsNullOrEmpty(bounty.id));
								Assert.True(!String.IsNullOrEmpty(bounty.name));
								Assert.True(!String.IsNullOrEmpty(bounty.description));
								Assert.True(!String.IsNullOrEmpty(bounty.imageSrc));
								Assert.True(!String.IsNullOrEmpty(bounty.rewardDescription));
								Assert.True(bounty.limit > 0);
								Assert.True(!String.IsNullOrEmpty(bounty.startsAt.ToString()));
								Assert.True(!String.IsNullOrEmpty(bounty.endsAt.ToString()));
								Assert.True(!String.IsNullOrEmpty(bounty.lastProgressCheck.ToString()));

								foreach (NexusSDK.BountyAPI.BountyObjective bountyObjective in bounty.objectives)
								{
									Assert.True(!String.IsNullOrEmpty(bountyObjective.id));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.name));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.type));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.condition));
									Assert.True(bountyObjective.count > 0);
									Assert.True(!String.IsNullOrEmpty(bountyObjective.eventType));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.eventCode));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.nexusPurchaseObjectiveType));

									foreach (NexusSDK.BountyAPI.BountySku sku in bountyObjective.skus)
									{
										Assert.True(!String.IsNullOrEmpty(sku.id));
										Assert.True(!String.IsNullOrEmpty(sku.name));
										Assert.True(!String.IsNullOrEmpty(sku.slug));
									}

									Assert.True(!String.IsNullOrEmpty(bountyObjective.category.id));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.category.name));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.category.slug));

									Assert.True(!String.IsNullOrEmpty(bountyObjective.publisher.id));
									Assert.True(!String.IsNullOrEmpty(bountyObjective.publisher.name));
								}

								foreach (NexusSDK.BountyAPI.BountyReward reward in bounty.rewards)
								{
									Assert.True(!String.IsNullOrEmpty(reward.id));
									Assert.True(!String.IsNullOrEmpty(reward.name));
									Assert.True(!String.IsNullOrEmpty(reward.type));

									Assert.True(!String.IsNullOrEmpty(reward.sku.id));
									Assert.True(!String.IsNullOrEmpty(reward.sku.name));
									Assert.True(!String.IsNullOrEmpty(reward.sku.slug));

									Assert.True(reward.amount > 0);
									Assert.True(!String.IsNullOrEmpty(reward.currency));
									Assert.True(!String.IsNullOrEmpty(reward.externalId));
								}

								foreach (NexusSDK.BountyAPI.Bounty.dependants_Struct_Element dependant in bounty.dependants)
								{
									Assert.True(!String.IsNullOrEmpty(dependant.id));
									Assert.True(!String.IsNullOrEmpty(dependant.name));
								}

								foreach (NexusSDK.BountyAPI.Bounty.prerequisites_Struct_Element prerequisite in bounty.prerequisites)
								{
									Assert.True(!String.IsNullOrEmpty(prerequisite.id));
									Assert.True(!String.IsNullOrEmpty(prerequisite.name));
								}
							}
						}

						bGetBountiesResponse = true;
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
				GetBountyRequest.groupId = GroupIdRef;
				GetBountyRequest.includeProgress = false;
				GetBountyRequest.page = 1;
				GetBountyRequest.pageSize = 100;
				StartCoroutine(NexusSDK.BountyAPI.StartGetBountyRequest(GetBountyRequest, new NexusSDK.BountyAPI.GetBountyResponseCallbacks()
				{
					OnGetBounty200Response = (SuccessResponse) =>
					{
						UnityEngine.Debug.Log("Received successful response");

						if (bTestContent == true)
						{
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupId));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));

							// bounty 
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.id));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.name));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.description));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.imageSrc));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.rewardDescription));
							Assert.True(SuccessResponse.bounty.limit > 0);
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.startsAt.ToString()));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.endsAt.ToString()));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.bounty.lastProgressCheck.ToString()));
							foreach (NexusSDK.BountyAPI.BountyObjective bountyObjective in SuccessResponse.bounty.objectives)
							{
								Assert.True(!String.IsNullOrEmpty(bountyObjective.id));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.name));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.type));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.condition));
								Assert.True(bountyObjective.count > 0);
								Assert.True(!String.IsNullOrEmpty(bountyObjective.eventType));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.eventCode));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.nexusPurchaseObjectiveType));

								foreach (NexusSDK.BountyAPI.BountySku sku in bountyObjective.skus)
								{
									Assert.True(!String.IsNullOrEmpty(sku.id));
									Assert.True(!String.IsNullOrEmpty(sku.name));
									Assert.True(!String.IsNullOrEmpty(sku.slug));
								}

								Assert.True(!String.IsNullOrEmpty(bountyObjective.category.id));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.category.name));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.category.slug));

								Assert.True(!String.IsNullOrEmpty(bountyObjective.publisher.id));
								Assert.True(!String.IsNullOrEmpty(bountyObjective.publisher.name));
							}
							foreach (NexusSDK.BountyAPI.BountyReward reward in SuccessResponse.bounty.rewards)
							{
								Assert.True(!String.IsNullOrEmpty(reward.id));
								Assert.True(!String.IsNullOrEmpty(reward.name));
								Assert.True(!String.IsNullOrEmpty(reward.type));

								Assert.True(!String.IsNullOrEmpty(reward.sku.id));
								Assert.True(!String.IsNullOrEmpty(reward.sku.name));
								Assert.True(!String.IsNullOrEmpty(reward.sku.slug));

								Assert.True(reward.amount > 0);
								Assert.True(!String.IsNullOrEmpty(reward.currency));
								Assert.True(!String.IsNullOrEmpty(reward.externalId));
							}
							foreach (NexusSDK.BountyAPI.Bounty.dependants_Struct_Element dependant in SuccessResponse.bounty.dependants)
							{
								Assert.True(!String.IsNullOrEmpty(dependant.id));
								Assert.True(!String.IsNullOrEmpty(dependant.name));
							}
							foreach (NexusSDK.BountyAPI.Bounty.prerequisites_Struct_Element prerequisite in SuccessResponse.bounty.prerequisites)
							{
								Assert.True(!String.IsNullOrEmpty(prerequisite.id));
								Assert.True(!String.IsNullOrEmpty(prerequisite.name));
							}
							// end bounty

							Assert.True(SuccessResponse.progress.currentPage > 0);
							Assert.True(SuccessResponse.progress.currentPageSize > 0);
							Assert.True(SuccessResponse.progress.totalCount > 0);

							foreach (NexusSDK.BountyAPI.GetBounty200Response.progress_Struct.data_Struct_Element dataElement in SuccessResponse.progress.data)
							{
								Assert.True(!String.IsNullOrEmpty(dataElement.id));
								Assert.True(dataElement.completionCount > -1);
								Assert.True(!String.IsNullOrEmpty(dataElement.lastProgressCheck.ToString()));
								Assert.True(!String.IsNullOrEmpty(dataElement.currentObjectiveGroupId));

								foreach (NexusSDK.BountyAPI.BountyObjectiveProgress currentObjective in dataElement.currentObjectives)
								{
									Assert.True(!String.IsNullOrEmpty(currentObjective.id));
									Assert.True(currentObjective.count > -1);

									Assert.True(!String.IsNullOrEmpty(currentObjective.objective.id));
									Assert.True(!String.IsNullOrEmpty(currentObjective.objective.name));
									Assert.True(currentObjective.objective.count > -1);
									Assert.True(!String.IsNullOrEmpty(currentObjective.objective.condition));
								}

								Assert.True(!String.IsNullOrEmpty(dataElement.creator.id));
								Assert.True(!String.IsNullOrEmpty(dataElement.creator.playerId));
								Assert.True(!String.IsNullOrEmpty(dataElement.creator.name));

								foreach (string slug in dataElement.creator.slugs)
								{
									Assert.True(!String.IsNullOrEmpty(slug));
								}
							}
						}

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

						if (bTestContent == true)
						{
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupId));
							Assert.True(!String.IsNullOrEmpty(SuccessResponse.groupName));
							Assert.True(SuccessResponse.currentPage > 0);
							Assert.True(SuccessResponse.currentPageSize > 0);
							Assert.True(SuccessResponse.totalCount > -1);
							//Assert.True(!String.IsNullOrEmpty(SuccessResponse.creatorId)); can be null if total count is 0
							//Assert.True(!String.IsNullOrEmpty(SuccessResponse.playerId)); can be null if total count is 0

							foreach (NexusSDK.BountyAPI.GetCreatorBountiesByID200Response.progress_Struct_Element progressElement in SuccessResponse.progress)
							{
								Assert.True(!String.IsNullOrEmpty(progressElement.id));
								Assert.True(progressElement.completionCount > -1);
								Assert.True(!String.IsNullOrEmpty(progressElement.lastProgressCheck.ToString()));
								Assert.True(!String.IsNullOrEmpty(progressElement.currentObjectiveGroupId));

								foreach (NexusSDK.BountyAPI.BountyObjectiveProgress currentObjective in progressElement.currentObjectives)
								{
									Assert.True(!String.IsNullOrEmpty(currentObjective.id));
									Assert.True(currentObjective.count > -1);
									Assert.True(!String.IsNullOrEmpty(currentObjective.objective.id));
									Assert.True(!String.IsNullOrEmpty(currentObjective.objective.name));
									Assert.True(currentObjective.objective.count > -1);
									Assert.True(!String.IsNullOrEmpty(currentObjective.objective.condition));
								}
								foreach (NexusSDK.BountyAPI.BountyProgress.completedObjectives_Struct_Element completedObjective in progressElement.completedObjectives)
								{
									Assert.True(!String.IsNullOrEmpty(completedObjective.objectiveGroupId));

									foreach (NexusSDK.BountyAPI.BountyObjectiveProgress objective in completedObjective.objectives)
									{
										Assert.True(!String.IsNullOrEmpty(objective.id));
										Assert.True(objective.count > -1);
										Assert.True(!String.IsNullOrEmpty(objective.objective.id));
										Assert.True(!String.IsNullOrEmpty(objective.objective.name));
										Assert.True(objective.objective.count > -1);
										Assert.True(!String.IsNullOrEmpty(objective.objective.condition));
									}

									foreach (NexusSDK.BountyAPI.BountyProgressReward reward in completedObjective.rewards)
									{
										Assert.True(!String.IsNullOrEmpty(reward.id));
										Assert.True(!String.IsNullOrEmpty(reward.name));
										Assert.True(!String.IsNullOrEmpty(reward.externalId));
										Assert.True(!String.IsNullOrEmpty(reward.rewardReferenceId));
									}
								}

								Assert.True(!String.IsNullOrEmpty(progressElement.id));
								Assert.True(!String.IsNullOrEmpty(progressElement.name));
								Assert.True(progressElement.limit > -1);
							}
						}

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
