using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NexusUnitySDKRuntimeTests
{
	[UnityTest]
	public IEnumerator NexusUnitySDK_PingAttributionsTest()
	{
		yield return new MonoBehaviourTest<PingAttributionsTest>();
	}

	public class PingAttributionsTest : MonoBehaviour, IMonoBehaviourTest
	{
		private int frameCount;
		private bool bPingAttributionsResponse = false;
		public bool IsTestFinished
		{
			get { return bPingAttributionsResponse; }
		}

		void Start()
		{
			UnityEngine.Debug.Log("Beginning Coroutine to request PingAttributions!");
			StartCoroutine(NexusAPI_Attributions.PingAttributionsRequest((isSuccess) =>
			{
				UnityEngine.Debug.Log("Received response of value: " + isSuccess);
				Assert.AreEqual(isSuccess, true);
				bPingAttributionsResponse = true;
			}));
		}
	}
}
