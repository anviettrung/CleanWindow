using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasserEffect : MonoBehaviour
{
    public ParticleSystem[] effects;

    private void OnEnable()
    {
        foreach (var effect in effects)
        {
            effect.Stop();
        }
    }

    public void PlayGlasserEffect(bool isTouching)
    {
        if (isTouching && !UIManager.Instance.IsPointerUIsObject())
        {
            foreach (var effect in effects)
            {
                effect.Play();
            }
        }
        else
        {
            foreach (var effect in effects)
            {
                effect.Stop();
            }
        }
    }
}
