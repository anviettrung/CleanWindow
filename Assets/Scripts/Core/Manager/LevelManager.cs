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

	[Header("Prefab")]
	public GameObject windowPrefab;

	// tracking
	protected Window currentWindow;
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
		// UI
		UIManager.Instance.CallLayout("Main Menu");

		// Instantiate
		WindowData data = levels[x].data;
		lastestLevelIndex = x;

		if (currentWindow != null)
			Destroy(currentWindow.gameObject);

		Window w = Instantiate(windowPrefab).GetComponent<Window>();

		// Setting
		w.Data = data; // window will automatic re-init
		PlayerInput.Instance.window = w;// set player input target to new window

		// Setting events
		w.onEnterStateDirty.AddListener(UsingGlasserTool);
		w.onEnterStateWet.AddListener(DestroyGlasserTool);
		w.onEnterStateWet.AddListener(UsingCleanerTool);
		w.onEnterStateComplete.AddListener(DestroyCleanerTool);

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

	protected void UsingCleanerTool()
	{
		ToolManager.Instance.cleaner.CreateTool();
		PlayerInput.Instance.tool = ToolManager.Instance.cleaner.GetTool();
	}

	protected void DestroyCleanerTool()
	{
		if (ToolManager.Instance.cleaner.GetTool() != null)
			Destroy(ToolManager.Instance.cleaner.GetTool().gameObject);
	}

	protected void UsingGlasserTool()
	{
		ToolManager.Instance.glasser.CreateTool();
		PlayerInput.Instance.tool = ToolManager.Instance.glasser.GetTool();
	}

	protected void DestroyGlasserTool()
	{
		if (ToolManager.Instance.glasser.GetTool() != null)
			Destroy(ToolManager.Instance.glasser.GetTool().gameObject);
	}

	public void PlayLevel()
	{
		UIManager.Instance.CallLayout("Playing");
		currentWindow.ChangeState(Window.State.DIRTY);
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