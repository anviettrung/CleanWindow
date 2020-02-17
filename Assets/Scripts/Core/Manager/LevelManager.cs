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
		UIManager.Instance.CallLayout("Main Menu");

		WindowData data = levels[x].data;
		lastestLevelIndex = x;

		if (currentWindow != null)
			Destroy(currentWindow.gameObject);

		Window w = Instantiate(windowPrefab).GetComponent<Window>();

		w.Data = data; // window will automatic re-init
		PlayerInput.Instance.window = w;// set player input target to new window
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

	public void PlayLevel()
	{
		UIManager.Instance.CallLayout("Playing");
	}

	public void UnlockLevel(string levelKeyName)
	{

	}

	public void LevelCompleted(string levelKeyName)
	{
		for (int i = 0; i < levels.Count; i++)
			if (levels[i].data.KeyName == levelKeyName) {
				levels[i].status = Level.Status.COMPLETE;
				return;
			}

	}
	#endregion

	#region GET/SET
	public int GetLevelIndex(WindowData data)
	{
		for (int i = 0; i < levels.Count; i++) {
			if (levels[i].data.KeyName == data.KeyName)
				return i;
		}

		return -1; // error 404
	}


	#endregion

	#region SAVE/LOAD

	public void Save()
	{
		Debug.Log("Save level");
		for (int i = 0; i < levels.Count; i++) {

			PlayerPrefs.SetInt("lvl_" + levels[i].data.KeyName, (int)levels[i].status);

		}
	}

	public void Load()
	{
		Debug.Log("Load level");
		for (int i = 0; i < levels.Count; i++) {

			string key = "lvl_" + levels[i].data.KeyName;
			if (PlayerPrefs.HasKey(key)) {

				int s = PlayerPrefs.GetInt(key);
				levels[i].status = (Level.Status)s;

			}

		}
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
}