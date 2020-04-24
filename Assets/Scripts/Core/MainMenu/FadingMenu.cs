using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingMenu : MonoBehaviour
{
    [SerializeField] private Image thisImage;
    public float delayTime;

    private void Start()
    {
        this.thisImage.color = new Color(1f, 1f, 1f, 0f);
    }

    private void OnEnable()
    {
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            this.FadeIn();
        }, this.delayTime));
    }

    public void FadeIn()
    {
        Color startColor = new Color(1f, 1f, 1f, 0f);
        Color endColor = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(IEFadeIn(startColor, endColor, 2f));
    }

    private IEnumerator IEFadeIn(Color startColor, Color endColor, float time)
    {
        float counter = 0f;
        while (counter < time)
        {
            counter += Time.deltaTime;
            this.thisImage.color = Color.Lerp(startColor, endColor, counter / time);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        this.thisImage.color = new Color(1f, 1f, 1f, 0f);
    }
}
