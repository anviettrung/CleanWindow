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
            this.FadeInByImageColor();
        }, this.delayTime));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        this.thisImage.color = new Color(1f, 1f, 1f, 0f);
    }

    public void FadeInByImageColor()
    {
        Color startColor = new Color(1f, 1f, 1f, 0f);
        Color endColor = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(IEFadingByImageColor(startColor, endColor, 1.5f));
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            UIManager.Instance.startButton.interactable = true;
        },
        1.5f));
    }

    private IEnumerator IEFadingByImageColor(Color startColor, Color endColor, float time)
    {
        float counter = 0f;
        while (counter < time)
        {
            counter += Time.deltaTime;
            this.thisImage.color = Color.Lerp(startColor, endColor, counter / time);
            yield return null;
        }
    }

    public void FadeOutByImageColor()
    {
        Color startColor = new Color(1f, 1f, 1f, 0f);
        Color endColor = new Color(1f, 1f, 1f, 1f);
        StartCoroutine(IEFadingByImageColor(endColor, startColor, 1f));
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            UIManager.Instance.CallLayout("Playing");
        }, 1f));
    }
}
