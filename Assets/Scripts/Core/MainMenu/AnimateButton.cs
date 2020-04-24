using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateButton : MonoBehaviour
{
    public List<GameObject> listObjectToMove;
    public List<Transform> listInsideTransform;

    private List<Vector3> listOriginPosition = new List<Vector3>();

    private void Awake()
    {
        for (int i = 0; i < this.listObjectToMove.Count; i++)
        {
            this.listOriginPosition.Add(this.listObjectToMove[i].transform.position);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            StartCoroutine(IEMoveAllButtonIntoScreen());
        }, 0.1f
        ));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        this.ResetAllTransform();
    }

    private IEnumerator IEMoveAllButtonIntoScreen()
    {
        for (int i = 0; i < this.listObjectToMove.Count; i++)
        {
            StartCoroutine(IEAnimateMoveObject(this.listObjectToMove[i],
                this.listObjectToMove[i].transform,
                this.listInsideTransform[i],
                2f));
            yield return new WaitForSeconds(0.05f);
            continue;
        }
    }

    private IEnumerator IEAnimateMoveObject(GameObject go, Transform start, Transform end, float time)
    {
        float counter = 0f;
        while (counter < time)
        {
            counter += Time.deltaTime;
            go.transform.position = Vector3.Lerp(start.position, end.position, counter / time);
            yield return null;
        }
    }

    private void ResetAllTransform()
    {
        for (int i = 0; i < this.listObjectToMove.Count; i++)
        {
            this.listObjectToMove[i].transform.position = this.listOriginPosition[i];
        }
    }
}
