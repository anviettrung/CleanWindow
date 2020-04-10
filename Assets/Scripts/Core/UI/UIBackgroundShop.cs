using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIBackgroundShop : MonoBehaviour
{
    public Image background;

    private void Start()
    {
        var rect = background.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
