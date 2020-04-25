using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTransform : MonoBehaviour
{
    [Header("Glasser & Cleaner Tools")]
    public Transform spawnTransform;
    public Transform startGlasserTransform;
    public Transform startCleanerTransform;
    public Transform endTransform;

    [Header("Breaker Tool")]
    public Transform spawnBreakerTransform;
    public Transform startBreakerTransform;
}
