using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIBlinking : MonoBehaviour
{
    private CanvasGroup thisCanvasGr;
    private const float MIN_ALPHA = 0.2f;

    //private void Start()
    //{
    //    this.thisCanvasGr = GetComponent<CanvasGroup>();
    //    this.thisCanvasGr.alpha = MIN_ALPHA;
    //    this.StartBlinking();
    //}

    private IEnumerator Blinking(float time)
    {
        while (true)
        {
            yield return StartCoroutine(LerpAlpha(1f, MIN_ALPHA, time));
            yield return StartCoroutine(LerpAlpha(MIN_ALPHA, 1f, time));
        }
    }

    private IEnumerator LerpAlpha(float start_alpha, float end_alpha, float time)
    {
        float count = 0f;
        while (count < time)
        {
            count += Time.deltaTime;
            this.thisCanvasGr.alpha = Mathf.Lerp(start_alpha, end_alpha, count / time);
            yield return null;
        }
    }

    private void StartBlinking()
    {
        StopCoroutine(Blinking(0.5f));
        StartCoroutine(Blinking(0.5f));
    }

    private void OnEnable()
    {
        this.thisCanvasGr = GetComponent<CanvasGroup>();
        this.thisCanvasGr.alpha = MIN_ALPHA;
        this.StartBlinking();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
