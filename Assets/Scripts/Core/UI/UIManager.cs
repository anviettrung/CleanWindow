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

	[Header("Bot UI elements")]
	public Button startButton;

	[Header("Shop UI elements")]
	public GameObject shopButton;

	[Header("Navigator button")]
	public Button nextButton;
	public Button watchAdsButton;

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

	public void SetActiveStartButton(bool s)
	{
		startButton.gameObject.SetActive(s);
	}

	public void ShowShopButton()
	{
		shopButton.SetActive(true);
	}

	public void HideShopButton()
	{
		shopButton.SetActive(false);
	}

	public void ShowEndgameUI()
	{
		ShowShopButton();
		nextButton.gameObject.SetActive(true);
		watchAdsButton.gameObject.SetActive(true);
	}

	public void HideEndgameUI()
	{
		HideShopButton();
		nextButton.gameObject.SetActive(false);
		watchAdsButton.gameObject.SetActive(false);
	}

	#endregion
}
