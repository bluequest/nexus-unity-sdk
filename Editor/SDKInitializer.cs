using UnityEngine;
using System;

namespace NexusSDK
{
	public static class SDKInitializer
	{
		public static string ApiKey { get; private set; }
		public static string ApiBaseUrl { get; private set; }

		/// <summary>
		/// Initializes the SDK with the specified API key and environment.
		/// </summary>
		/// <param name="apiKey">The public API key to be used for API requests.</param>
		/// <param name="environment">The environment to use ("production" or "sandbox"). Defaults to "sandbox".</param>
		public static void Init(string apiKey, string environment = "sandbox")
		{
			if (string.IsNullOrEmpty(apiKey))
			{
				throw new ArgumentException("API Key cannot be null or empty", nameof(apiKey));
			}

			ApiKey = apiKey;

			if (string.Equals(environment, "production", StringComparison.OrdinalIgnoreCase))
			{
				ApiBaseUrl = "https://api.nexus.gg/v1";
			}
			else
			{
				ApiBaseUrl = "https://api.nexus-dev.gg/v1"; // Default to sandbox
			}
		}
	}
}