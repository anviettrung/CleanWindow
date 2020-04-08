using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeWindow : MonoBehaviour
{
    public void Resize(Sprite sprite)
    {
        transform.localScale = new Vector3(1, 1, 1);

        float width = sprite.bounds.size.x;
        float height = sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        //transform.localScale = xWidth;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        //transform.localScale = yHeight;

        transform.localScale = new Vector3(xWidth.x * 0.8f, yHeight.y * 0.8f, 1);
    }
}
