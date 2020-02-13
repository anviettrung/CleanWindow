using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	#region REFERENCE

	[Header("Main layout")]
	public GameObject layoutTopUI;

	[Header("Top UI elements")]
	public GameObject panelMoney;
	public Image iconMoney;
	public Text labelMoneyNumber;

	public Button buttonSetting;

	public Text labelLevelName;
	public Slider progressBar;

	#endregion

	#region FUNCTION

	public void SetLabelLevelName(string s)
	{
		labelLevelName.text = s;
	}

	public void SetProgressBar(float v)
	{
		v = Mathf.Clamp01(v);
		progressBar.value = v;
	}

	#endregion
}
