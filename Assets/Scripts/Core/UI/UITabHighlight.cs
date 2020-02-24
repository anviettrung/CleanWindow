using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabHighlight : MonoBehaviour
{
	[Header("References")]
	public Image image;
	public Sprite spriteHighlightOn;
	public Sprite spriteHighlightOff;

	[SerializeField] protected bool isOn;

	public void SwitchStatus()
	{
		isOn = !isOn;
	}

	public void SetStatus(bool s)
	{
		isOn = s;
	}

	public void Update()
	{
		if (isOn)
			image.sprite = spriteHighlightOn;
		else
			image.sprite = spriteHighlightOff;
	}
}
