using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	#region DATA
	[Header("Resources")]
	public LevelsData levelsData;
	[Header("Working data generated from resources")]
	public List<Level> levels;

	//[Header("Prefab")]
	//public GameObject windowPrefab;

	// tracking
	[HideInInspector] public Window currentWindow;
	protected int lastestLevelIndex;

	#endregion

	#region UNITY_CALLBACK
	public void Awake()
	{
		Init();
	}
	#endregion

	#region INITIALIZATION
	public void Init()
	{
		for (int i = 0; i < this.levelsData.Windows.Length; i++)
		{
			var slot_window = Instantiate(UIManager.Instance.slotWindow, UIManager.Instance.contentWindow);
			slot_window.GetComponent<UIWindowShopItem>().UpdateUI();
			slot_window.GetComponent<UIWindowShopItem>().levelText.text = "LEVEL " + i;
			if (!UIManager.Instance.windowShop.items.Contains(slot_window.GetComponent<UIWindowShopItem>()))
			{
				UIManager.Instance.windowShop.items.Add(slot_window.GetComponent<UIWindowShopItem>());
			}
		}

		levels = new List<Level>();
		for (int i = 0; i < levelsData.Windows.Length; i++) {
			Level lvl = new Level(levelsData.Windows[i]);
			levels.Add(lvl);
		}

		UIManager.Instance.windowShop.SetData(levels);
	}
	#endregion

	#region OPEN_LEVEL
	public void OpenLevel(int x)
	{
		//StopAllCoroutines();
		var tool_list_template = FindObjectsOfType<ToolListTemplate>();
		foreach (var tool in tool_list_template)
		{
			tool.StopCoroutineMoveTool();
			if (tool.currentTool != null)
			{
				if (tool.currentTool.Data.ToolType != ToolData.Type.BREAKER)
				{
					tool.currentTool.transform.position = GameManager.Instance.toolTransform.spawnTransform.position;
				}
				else
				{
					tool.currentTool.transform.position = GameManager.Instance.toolTransform.spawnBreakerTransform.position;
				}
			}
		}

		//var list_tools = FindObjectsOfType<Tool>();
		//for (int i = 0; i < list_tools.Length; i++)
		//{
		//	if (list_tools[i].Data.ToolType != ToolData.Type.BREAKER)
		//	{
		//		list_tools[i].transform.position = GameManager.Instance.toolTransform.spawnTransform.position;
		//	}
		//	else
		//	{
		//		list_tools[i].transform.position = GameManager.Instance.toolTransform.spawnBreakerTransform.position;
		//	}
		//}


		// UI
		UIManager.Instance.CallLayout("Main Menu");
		UIManager.Instance.cityName.text = "???";

		// Instantiate
		WindowData data = levels[x].data;
		lastestLevelIndex = x;

		if (currentWindow != null)
			Destroy(currentWindow.gameObject);

		ChangeMenuTheme.Instance.CreateNewWindow();

		//Window w = Instantiate(windowPrefab).GetComponent<Window>();
		currentWindow.gameObject.SetActive(true);
		Window w = currentWindow;

		// Setting
		w.Data = data; // window will automatic re-init
		PlayerInput.Instance.window = w;// set player input target to new window

		// Setting events
		w.onEnterStateDirty.AddListener(UsingGlasserTool);
		w.onEnterStateWet.AddListener(DestroyGlasserTool);
		w.onEnterStateWet.AddListener(UsingCleanerTool);
		w.onEnterStateBreakGlass.AddListener(DestroyCleanerTool);
		w.onEnterStateBreakGlass.AddListener(UsingBreakerTool);
		w.onEnterStateCapture.AddListener(DestroyBreakerTool);
		w.onEnterStateCapture.AddListener(EnableButtonCapture);
		w.onEnterStateComplete.AddListener(DisableButtonCapture);

		currentWindow = w; // track
	}

	public void OpenLevel(WindowData data)
	{
		int x = GetLevelIndex(data);
		if (x != -1)
			OpenLevel(x);
	}

	public void OpenLastestLevel()
	{
		OpenLevel(lastestLevelIndex);
	}

	public void OpenNextLevel()
	{
		OpenLevel((lastestLevelIndex + 1) % levels.Count);
	}

	#endregion

	#region FUNCTION

	protected void UsingBreakerTool()
	{
		ToolManager.Instance.breaker.CreateTool();
		//PlayerInput.Instance.tool = ToolManager.Instance.breaker.GetTool();

		ExplodeWindow explode = FindObjectOfType<ExplodeWindow>();
		explode.breakerAnim = PlayerInput.Instance.tool.transform.GetComponentInChildren<Animation>();

		UIManager.Instance.tapAndHold.SetActive(true);
	}

	protected void DestroyBreakerTool()
	{
		//if (ToolManager.Instance.breaker.GetTool() != null)
		//	Destroy(ToolManager.Instance.breaker.GetTool().gameObject);
		if (ToolManager.Instance.breaker.GetTool() != null)
		{
			ToolManager.Instance.breaker.MoveTool(GameManager.Instance.toolTransform.spawnBreakerTransform.position, 2f);
			StartCoroutine(CoroutineUtils.DelaySeconds(() =>
			{
				ToolManager.Instance.breaker.GetTool().transform.GetChild(0).transform.localPosition = Vector3.zero;
			},2f));
			
		}
	}

	protected void UsingCleanerTool()
	{
		ToolManager.Instance.cleaner.CreateTool();
		//PlayerInput.Instance.tool = ToolManager.Instance.cleaner.GetTool();
	}

	protected void DestroyCleanerTool()
	{
		//if (ToolManager.Instance.cleaner.GetTool() != null)
		//	Destroy(ToolManager.Instance.cleaner.GetTool().gameObject);

		if (ToolManager.Instance.cleaner.GetTool() != null)
		{
			ToolManager.Instance.cleaner.MoveTool(GameManager.Instance.toolTransform.endTransform.position, 2f);
		}
	}

	protected void UsingGlasserTool()
	{
		ToolManager.Instance.glasser.CreateTool();
		//PlayerInput.Instance.tool = ToolManager.Instance.glasser.GetTool();
	}

	protected void DestroyGlasserTool()
	{
		//if (ToolManager.Instance.glasser.GetTool() != null)
		//	Destroy(ToolManager.Instance.glasser.GetTool().gameObject);
		if (ToolManager.Instance.glasser.GetTool() != null)
		{
			ToolManager.Instance.glasser.MoveTool(GameManager.Instance.toolTransform.endTransform.position, 2f);
		}
	}

	protected void EnableButtonCapture()
	{
		UIManager.Instance.layoutCapture.gameObject.SetActive(true);
	}

	protected void DisableButtonCapture()
	{
		UIManager.Instance.layoutCapture.gameObject.SetActive(false);
	}

	public void PlayLevel()
	{
		currentWindow.gameObject.SetActive(true);

		PlayerInput.Instance.window = currentWindow;

		var all_winddows = FindObjectsOfType<Window>();
		foreach (var w in all_winddows)
		{
			if (w != currentWindow)
			{
				Destroy(w.gameObject);
			}
		}

		// Setting events
		currentWindow.onEnterStateDirty.AddListener(UsingGlasserTool);
		currentWindow.onEnterStateWet.AddListener(DestroyGlasserTool);
		currentWindow.onEnterStateWet.AddListener(UsingCleanerTool);
		currentWindow.onEnterStateBreakGlass.AddListener(DestroyCleanerTool);
		currentWindow.onEnterStateBreakGlass.AddListener(UsingBreakerTool);
		currentWindow.onEnterStateCapture.AddListener(DestroyBreakerTool);
		currentWindow.onEnterStateCapture.AddListener(EnableButtonCapture);
		currentWindow.onEnterStateComplete.AddListener(DisableButtonCapture);

		StartCoroutine(CoroutineUtils.Chain(
			CoroutineUtils.Do(() =>
			{
				UIManager.Instance.CallLayout("Playing");
				currentWindow.ChangeState(Window.State.DIRTY);
			})));

		StartCoroutine(CoroutineUtils.Chain(
			CoroutineUtils.Do(() => CameraMaster.Instance.TransitionToView(CameraMaster.View.MEDIUM_SHOT)),
			CoroutineUtils.WaitForSeconds(2)
			//CoroutineUtils.Do(() => { 
			//	UIManager.Instance.CallLayout("Playing");
			//	currentWindow.ChangeState(Window.State.DIRTY);
			//})
		));

		foreach (var window in ChangeMenuTheme.Instance.windows)
		{
			if (window.gameObject.activeInHierarchy == false)
			{
				Destroy(window.gameObject);
			}
		}
		ChangeMenuTheme.Instance.windows.TrimExcess();
	}

	public void UnlockLevel(int x)
	{
		if (x != -1) {
			levels[x].ChangeStatus(Level.Status.UNLOCK);
			Save();
		}
	}

	public void UnlockLevel(string keyName)
	{
		UnlockLevel(GetLevelIndex(keyName));
	}

	public void LevelCompleted(string keyName)
	{
		for (int i = 0; i < levels.Count; i++) {
			if (levels[i].data.KeyName == keyName) {
				levels[i].ChangeStatus(Level.Status.COMPLETE);

				// Auto unlock next level
				if (i + 1 < levels.Count)
					UnlockLevel(i + 1);

				Save();
				return;
			}
		}

		//Add haptic:
		VibrationManager.Instance.OnAfterTakePhoto();

	}
	#endregion

	#region GET/SET
	public int GetLevelIndex(string keyName)
	{
		for (int i = 0; i < levels.Count; i++) {
			if (levels[i].data.KeyName == keyName)
				return i;
		}

		return -1; // error 404
	}

	public int GetLevelIndex(WindowData data)
	{
		return GetLevelIndex(data.KeyName);
	}


	#endregion

	#region SAVE/LOAD

	public void Save()
	{
		for (int i = 0; i < levels.Count; i++) {

			PlayerPrefs.SetInt("lvl_" + levels[i].data.KeyName, (int)levels[i].status);

		}

		PlayerPrefs.SetInt("lastest_lvl", lastestLevelIndex);
	}

	public void Load()
	{
		for (int i = 0; i < levels.Count; i++) {

			string key = "lvl_" + levels[i].data.KeyName;
			if (PlayerPrefs.HasKey(key)) {

				int s = PlayerPrefs.GetInt(key);
				levels[i].status = (Level.Status)s;

			}

		}

		if (PlayerPrefs.HasKey("lastest_lvl"))
			lastestLevelIndex = PlayerPrefs.GetInt("lastest_lvl");
	}
	#endregion
}

[System.Serializable]
public class Level
{
	public WindowData data;
	public Status status;
	public enum Status
	{
		LOCK,
		UNLOCK,
		COMPLETE
	}

	public Level(WindowData d)
	{
		data = d;
	}

	public void ChangeStatus(Level.Status s)
	{
		// Once status is complete, it couldn't back to other status
		if (status != Status.COMPLETE)
			status = s;
	}
}