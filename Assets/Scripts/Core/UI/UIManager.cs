using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

	[Header("Layout money elemetns")]
	//public GameObject panelMoney;
	public GameObject layoutMoney;
	public Text textMoneyNumber;

	[Header("Top UI elements")]
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
	public List<UIShowStep> uIShowSteps;
	public Image avatarPlayer;
	public Text cityName;
	public Text textLevel;

	[Header("Capture Image")]
	public Image captureImage;
	public Image avatarInCapture;
	public CanvasGroup flashImage;
	public Text cityCaption;

	[Header("Layout Window Shop")]
	public GameObject slotWindow;
	public Transform contentWindow;

	[Header("Gift Box")]
	public Transform panelGiftBox;
	public UIGiftBox uIGiftBox;

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
		layoutMoney.SetActive(s);
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
		for (int i = 0; i < this.uIShowSteps.Count;i++)
		{
			if (this.uIShowSteps[i].state == windowState)
			{
				this.uIShowSteps[i].highlight.SetActive(true);
			}
			else
			{
				this.uIShowSteps[i].highlight.SetActive(false);
			}
		}
	}

	public bool IsPointerUIsObject()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		return results.FindAll(result => result.gameObject.layer == LayerMask.NameToLayer("UI")).Count > 0;
	}

	public void BackToMainMenu()
	{
		PlayerInput.Instance.tool = null;
		if (LevelManager.Instance.currentWindow != null)
			Destroy(LevelManager.Instance.currentWindow.gameObject);

		LevelManager.Instance.OpenHighestLevel();
		LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.HighestLevel);
		this.textLevel.gameObject.SetActive(false);

		CameraMaster.Instance.TransitionToView(CameraMaster.View.FULL_SHOT);
	}

	public void TakePhoto()
	{
		//Add haptic:
		VibrationManager.Instance.OnTakePhoto();

		LevelManager.Instance.currentWindow.gameObject.SetActive(false);
		StartCoroutine(ShowFlash(0.5f));
		StartCoroutine(CoroutineUtils.DelaySeconds(
				LevelManager.Instance.currentWindow.NextState,
				LevelManager.Instance.currentWindow.nextStateAfterBreakGlassTime));

		UIListAvatar.Instance.SaveAvatar();
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
		this.cityCaption.text = this.cityName.text;
		this.CallLayout("End Game");
		//this.panelMoney.SetActive(true);
		this.layoutMoney.SetActive(true);
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
