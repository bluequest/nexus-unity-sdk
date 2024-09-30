# Nexus Unity SDK Documentation

Welcome to the **Nexus Unity SDK Documentation**. This document will guide you through the setup, initialization, and usage of the Nexus Unity SDK for integrating Nexus APIs into your Unity projects.

## Overview

The Nexus Unity SDK allows you to easily interact with Nexus APIs, including validation of creator codes, in-game purchases, and other API-driven features. This SDK is designed to be lightweight, easy to integrate, and supports both sandbox and production environments.

---

## Table of Contents
- [Installation](#installation)
- [Initialization](#initialization)
- [Usage](#usage)
  - [Validating Creator Codes](#validating-creator-codes)
  - [Other API Requests](#other-api-requests)
- [Best Practices](#best-practices)
- [FAQ](#faq)

---

## Installation

### Adding the Nexus SDK via Git URL

To add the Nexus Unity SDK to your project, follow these steps:

1. Open your Unity project.
2. Go to `Window > Package Manager` in the Unity Editor.
3. Click the `+` button in the top left corner of the Package Manager and select `Add package from git URL...`.
4. In the URL field, enter:  
   `https://github.com/bluequest/nexus-unity-sdk.git`
5. Unity will automatically download and install the SDK into your project.

---

## Initialization

Before making any API calls, the SDK needs to be initialized with your API key and environment. This setup ensures that all API interactions use the correct environment and authentication.

### Example Code: Initializing the SDK


```csharp
using NexusSDK;

public class GameStartup : MonoBehaviour
{
    void Start()
    {
        // Initialize Nexus SDK with your API key and environment.
        SDKInitializer.Init("your-api-key-here", "production");

        // The environment can be set to "sandbox" for testing.
        // If omitted, it defaults to "sandbox".
    }
}
```

---

## Usage

Once the SDK is initialized, you can start using it to interact with Nexus APIs, such as validating creator codes or retrieving data from your backend.

### Validating Creator Codes

```csharp
using NexusSDK;

public class CodeValidator : MonoBehaviour
{
    void ValidateCreatorCode(string creatorCode)
    {
        AttributionAPI.ValidateCreatorCode(creatorCode, (isValid) =>
        {
            if (isValid)
            {
                Debug.Log("Creator code is valid!");
            }
            else
            {
                Debug.Log("Invalid creator code.");
            }
        });
    }
}
```

### Other API Requests

You can also perform additional API interactions, such as retrieving data about members, by using the SDK’s built-in API helpers.

```csharp
using NexusSDK;

public class MemberQuery : MonoBehaviour
{
    void FetchMemberData(string memberID)
    {
        MembersAPI.GetMemberDetails(memberID, (memberData) =>
        {
            if (memberData != null)
            {
                Debug.Log($"Member found: {memberData.name}");
            }
            else
            {
                Debug.Log("Member not found.");
            }
        });
    }
}
```

---

## Best Practices

- **Environment Setup**: Always use the "sandbox" environment when testing to avoid affecting live data.
- **Error Handling**: Make sure to handle API request failures gracefully by checking for null results or exceptions.
- **Asynchronous Operations**: Remember that API calls are asynchronous; provide callbacks or use coroutines to handle API responses in your game.

---

## FAQ

### 1. What is the difference between "sandbox" and "production" environments?
   - **Sandbox**: Use this environment for testing purposes. It doesn’t affect real data or transactions.
   - **Production**: Use this environment when the game is live and interacting with real user data.

### 2. Can I make changes to the SDK?
   - The SDK is open-source, and you can modify it for your project. However, changes may be overwritten if the SDK is updated.

### 3. What is required to use the Nexus SDK?
   - You will need an API key from the Nexus platform, which you can get by signing up as a developer on Nexus.gg.

---

This document is a quick reference to help you integrate the Nexus SDK into your Unity projects. For more detailed information, visit our [GitHub Repository](https://github.com/bluequest/nexus-unity-sdk).
