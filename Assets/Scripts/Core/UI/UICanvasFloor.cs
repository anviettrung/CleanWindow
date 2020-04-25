using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasFloor : MonoBehaviour
{
    public RectTransform canvasFloor;

    private void Start()
    {
        this.canvasFloor.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
