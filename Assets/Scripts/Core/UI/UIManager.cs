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
	public GameObject layoutTopUIGameplay;

	[Header("Top UI elements")]
	public Image iconMoney;
	public Text labelMoneyNumber;

	public Button buttonSetting;

	public Text labelLevelName;
	public Slider progressBar;
	public Image gameTitle;

	[Header("Bot UI elements")]
	public Button startButton;
	public GameObject layoutCapture;
	public GameObject tapAndHold;

	[Header("Shop UI elements")]
	public UIWindowShop windowShop;
	public UIToolShop toolShop;

	public Text labelWindowShopPageIndex;

	[Header("Navigator button")]
	public Button nextButton;
	public Button watchAdsButton;

	[Header("Top UI in Gameplay")]
	public List<UIProgress> uIProgresses;
	public Image avatarPlayer;
	public Text cityName;

	[Header("Capture Image")]
	public Image captureImage;
	public Image avatarInCapture;
	public CanvasGroup flashImage;

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
		layoutTopUIGameplay.SetActive(s);
		layoutCapture.SetActive(s);
	}

	public void SelectTabCleanerTool()
	{
		toolShop.SetData(ToolManager.Instance.cleaner.tools);
	}

	public void SelectTabGlasserTool()
	{
		toolShop.SetData(ToolManager.Instance.glasser.tools);
	}

	public void SelectTabCustomizeTool()
	{
		toolShop.SetData(ToolManager.Instance.breaker.tools);
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

	public void ShowNextStepProgress(Window.State windowState)
	{
		for (int i = 0; i < this.uIProgresses.Count;i++)
		{
			if (this.uIProgresses[i].state == windowState)
			{
				this.uIProgresses[i].highlight.SetActive(true);
			}
			else
			{
				this.uIProgresses[i].highlight.SetActive(false);
			}
		}
	}

	public void BackToMainMenu()
	{
		if (ToolManager.Instance.cleaner.GetTool() != null)
			Destroy(ToolManager.Instance.cleaner.GetTool().gameObject);
		if (ToolManager.Instance.glasser.GetTool() != null)
			Destroy(ToolManager.Instance.glasser.GetTool().gameObject);
		if (ToolManager.Instance.breaker.GetTool() != null)
			Destroy(ToolManager.Instance.breaker.GetTool().gameObject);
		if (LevelManager.Instance.currentWindow != null)
			Destroy(LevelManager.Instance.currentWindow.gameObject);

		LevelManager.Instance.OpenLastestLevel();

		CameraMaster.Instance.TransitionToView(CameraMaster.View.FULL_SHOT);
	}

	public void TakePhoto()
	{
		LevelManager.Instance.currentWindow.gameObject.SetActive(false);
		StartCoroutine(ShowFlash(0.5f));
		StartCoroutine(CoroutineUtils.DelaySeconds(
				LevelManager.Instance.currentWindow.NextState,
				LevelManager.Instance.currentWindow.nextStateAfterBreakGlassTime));
	}

	private IEnumerator ShowFlash(float time)
	{
		float count = 0f;
		while (count < time)
		{
			count += Time.deltaTime;
			this.flashImage.alpha = Mathf.Lerp(0f, 1f, count / time);
			yield return null;
		}
		this.captureImage.sprite = LevelManager.Instance.currentWindow.srMainPicture.sprite;
		this.avatarInCapture.sprite = this.avatarPlayer.sprite;
		this.CallLayout("End Game");
		count = 0f;
		while (count < time)
		{
			count += Time.deltaTime;
			this.flashImage.alpha = Mathf.Lerp(1f, 0f, count / time);
			yield return null;
		}
	}

	#endregion
}
