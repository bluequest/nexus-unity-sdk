# Nexus Unity SDK: Setup and Usage Instructions

The Nexus Unity SDK provides a streamlined way to integrate Nexus APIs into your Unity projects. Follow these steps to set up and use the SDK.

## Installation

### Add the SDK via Git URL
To install the Nexus Unity SDK, follow these steps:

1. Open your Unity project.
2. Go to `Window > Package Manager` in the Unity Editor.
3. Click the `+` button in the top left corner of the Package Manager window and select `Add package from git URL...`.
4. In the URL field, enter the following URL: `https://github.com/bluequest/nexus-unity-sdk.git`
5. Click `Add`. Unity will download and install the SDK directly into your project.

## Initialization

Before making any API calls, you need to initialize the SDK with your API key and environment (sandbox or production).

### Example Initialization Code

```csharp
using NexusSDK;

public class MyGame : MonoBehaviour
{
    void Start()
    {
        // Initialize the SDK with your API key and environment
        // Use "sandbox" for testing or leave empty to default to sandbox
        SDKInitializer.Init("your-api-key-here", "production");
        
        // Now you can make API calls
    }
}
```