using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlasserEffect : MonoBehaviour
{
    public ParticleSystem[] effects;

    public void PlayGlasserEffect(bool isTouching)
    {
        if (isTouching)
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
