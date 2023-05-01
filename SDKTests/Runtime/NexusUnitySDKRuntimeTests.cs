using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NexusUnitySDKRuntimeTests
{
    [UnityTest]
    public IEnumerator GameFlowTest()
    {
		// Use the Assert class to test conditions
		Assert.AreEqual(1, 1);
		yield return null;
    }
}
