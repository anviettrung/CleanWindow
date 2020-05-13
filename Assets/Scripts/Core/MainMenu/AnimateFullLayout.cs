using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateFullLayout : MonoBehaviour
{
    public GameObject mainLayout;
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        LayoutMoveInside();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ResetTransform();
    }

    public void LayoutMoveInside()
    {
        StartCoroutine(IEMoveLayout(0.5f, inside.position));
    }

    public void LayoutMoveOutside()
    {
        StartCoroutine(IEMoveLayout(0.5f, outside.position));
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            mainLayout.gameObject.SetActive(false);
        }, 0.7f
       ));
    }

    private IEnumerator IEMoveLayout(float time, Vector3 end)
    {
        float counter = 0f;
        while (counter < time)
        {
            counter += Time.deltaTime;
            mainLayout.transform.position = Vector3.Lerp(mainLayout.transform.position, end, counter / time);
            yield return null;
        }
    }

    private void ResetTransform()
    {
        mainLayout.transform.position = outside.position;
    }
}
