using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILerpAlpha : MonoBehaviour
{
    public void StartFadeOutAlpha()
    {
        Color startColor = new Color(1f, 1f, 1f, 1f);
        Color endColor = new Color(1f, 1f, 1f, 0f);
        Image image = this.GetComponent<Image>();

        StartCoroutine(IEFadeOutAlpha(image, startColor, endColor, 1f));

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

    private void OnDisable()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
