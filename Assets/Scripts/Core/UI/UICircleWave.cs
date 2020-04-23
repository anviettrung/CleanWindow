using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICircleWave : MonoBehaviour
{
    #region UNITY_CALLBACK
    private void Start()
    {
        this.transform.localScale = Vector3.one;
    }

    private void Update()
    {
        if (this.transform.localScale.x >= (this.transform.localScale * 4f).x)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(IEStartSpreadOut());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        this.transform.localScale = Vector3.one;
        this.transform.GetComponent<Image>().color = Color.white;
    }
    #endregion

    #region FUNCTION
    private IEnumerator IEStartSpreadOut()
    {
        Vector3 startScale = this.transform.localScale;
        Vector3 endScale = this.transform.localScale * 4f;

        Color startColor = new Color(1f, 1f, 1f, 1f);
        Color endColor = new Color(1f, 1f, 1f, 0f);

        yield return StartCoroutine(IESpreadOut(startScale, endScale, startColor, endColor, 4f));
    }

    private IEnumerator IESpreadOut(Vector3 startScale, Vector3 endScale, Color startColor, Color endColor, float time)
    {
        float count = 0;
        while (count < time)
        {
            count += Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(startScale, endScale, count / (time * 2f));
            this.transform.GetComponent<Image>().color = Color.Lerp(startColor, endColor, count / time);
            yield return null;
        }
    } 
    #endregion
}
