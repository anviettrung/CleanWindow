using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	#region REFERENCE

	[Header("All layout elements")]
	public GameObject layoutTopUI;
	public GameObject layoutProgress;
	public GameObject layoutShopButton;
	public GameObject layoutLevelEnd;
	public GameObject layoutWindowShop;
	public GameObject layoutToolShop;
	public GameObject layoutSettingPanel;
	public GameObject layoutChangeTheme;

	[Header("Top UI elements")]
	public Image iconMoney;
	public Text labelMoneyNumber;

	public Button buttonSetting;

	public Text labelLevelName;
	public Slider progressBar;
	public Image gameTitle;

	[Header("Bot UI elements")]
	public Button startButton;
	public GameObject tapAndHold;

	[Header("Shop UI elements")]
	public UIWindowShop windowShop;
	public UIToolShop toolShop;

	public Text labelWindowShopPageIndex;

	[Header("Setting elements")]


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
		layoutSettingPanel.SetActive(s);
		startButton.gameObject.SetActive(s);
		layoutChangeTheme.SetActive(s);
		gameTitle.gameObject.SetActive(s);
		tapAndHold.SetActive(s);
	}

	public void SelectTabCleanerTool()
	{
		toolShop.SetData(ToolManager.Instance.cleaner.tools);
	}

	public void SelectTabGlasserTool()
	{
		toolShop.SetData(ToolManager.Instance.glasser.tools);
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
