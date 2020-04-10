using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIHighLightText : MonoBehaviour
{
    public Color selectColor;
    public Color deselectColor;
    public Text mainText;
    public GameObject deselectButton;
    private UITabHighlight tab;

    private void Start()
    {
        tab = GetComponent<UITabHighlight>();
        if (tab.isOn)
        {
            mainText.color = selectColor;
            deselectButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (tab.isOn)
        {
            mainText.color = selectColor;
            deselectButton.SetActive(false);
        }
        else
        {
            mainText.color = deselectColor;
            deselectButton.SetActive(true);
        }
    }
}
