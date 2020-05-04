using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILerpAlpha : MonoBehaviour
{
    [SerializeField] Text text;

    public void StartFadeOutAlphaByImage()
    {
        Color startColor = new Color(1f, 1f, 1f, 1f);
        Color endColor = new Color(1f, 1f, 1f, 0f);
        Image image = this.GetComponent<Image>();

        StartCoroutine(IEFadeOutAlpha(image, startColor, endColor, 1f));
    }

    public void StartFadeOutAlphaByTextColor()
    {
        Color startColor = new Color(1f, 1f, 1f, 1f);
        Color endColor = new Color(1f, 1f, 1f, 0f);

        StartCoroutine(IEFadingByTextColor(startColor, endColor, 1f));

    }

    private IEnumerator IEFadeOutAlpha(Image image, Color startColor, Color endColor, float time)
    {
        float counter = 0f;
        while (counter < time)
        {
            counter += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, counter / time);
            yield return null;
        }
    }


    private IEnumerator IEFadingByTextColor(Color startColor, Color endColor, float time)
    {
        float counter = 0f;
        while (counter < time)
        {
            counter += Time.deltaTime;
            this.text.color = Color.Lerp(startColor, endColor, counter / time);
            yield return null;
        }
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        if (this.text != null)
        {
            this.text.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
