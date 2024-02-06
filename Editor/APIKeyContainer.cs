using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class APIKeyContainer
{
    public static string APIKey => Resources.Load<APIKeyData>("APIKeyData").apiKey;
}