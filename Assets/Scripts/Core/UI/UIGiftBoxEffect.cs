using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGiftBoxEffect : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("PlayCircleWaveEffect", 2f, 2f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void PlayCircleWaveEffect()
    {
        var pool = ObjectPooler.Instance.GetPooledObject();
        pool.SetActive(true);
    }
}
