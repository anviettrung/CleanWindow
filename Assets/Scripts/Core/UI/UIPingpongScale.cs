using UnityEngine;

public class UIPingpongScale : MonoBehaviour
{
    private Vector2 min;
    private Vector2 max;
    public float pingPongtime;

    private void Start()
    {
        this.min = this.transform.localScale;
        this.max = min * 1.2f;
    }

    private void Update()
    {
        this.transform.localScale = new Vector3(Mathf.Lerp(this.min.x, this.max.x, Mathf.PingPong(Time.time, this.pingPongtime)),
             Mathf.Lerp(this.min.y, this.max.y, Mathf.PingPong(Time.time, this.pingPongtime)),
             this.transform.localScale.z);
    }
}
