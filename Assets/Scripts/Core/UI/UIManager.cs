using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	#region REFERENCE

	[Header("Main layout")]
	public GameObject layoutTopUI;
	public GameObject layoutProgress;
	public GameObject layoutShopButton;
	public GameObject layoutLevelEnd;
	public GameObject layoutWindowShop;
	public GameObject layoutToolShop;

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
	public UIWindowShop windowShop;

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

	#endregion

	#region SHOW/HIDE
	public void ShowAll(bool s)
	{
		layoutTopUI.SetActive(s);
		layoutProgress.SetActive(s);
		layoutShopButton.SetActive(s);
		layoutLevelEnd.SetActive(s);
		layoutWindowShop.SetActive(s);
		layoutToolShop.SetActive(s);
	}

	public void ShowTopUI(bool s)
	{
		layoutTopUI.SetActive(s);
	}

	public void ShowShopButton(bool s)
	{
		layoutShopButton.SetActive(s);
	}

	public void ShowEndgameUI(bool s)
	{
		ShowShopButton(s);
		layoutLevelEnd.SetActive(s);
	}

	public void ShowStartButton(bool s)
	{
		startButton.gameObject.SetActive(s);
	}

	public void ShowWindowShop(bool s)
	{
		layoutWindowShop.SetActive(s);

		if (s == true) {
			windowShop.UpdateUI();
		}
	}

	#endregion

	#region LAYOUT MANAGER
	[System.Serializable]
	public class UILayoutInfo
	{
		public string layoutName;
		public GameObject[] elements;
	}

	[Header("UI Layouts")]
	public List<UILayoutInfo> layouts;

	public UILayoutInfo GetLayout(string name)
	{
		foreach (UILayoutInfo layout in layouts)
			if (layout.layoutName == name)
				return layout;

		return null;
	}

	public void CallLayout(string name)
	{
		UILayoutInfo layoutInf = GetLayout(name);
		if (layoutInf == null) return;

		ShowAll(false);
		for (int i = 0; i < layoutInf.elements.Length; i++) {
			layoutInf.elements[i].SetActive(true);
		}
	}

	#endregion
}
