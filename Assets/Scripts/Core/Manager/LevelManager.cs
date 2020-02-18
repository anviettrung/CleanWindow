using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
	#region DATA
	public LevelsData levelsData;
	public List<Level> levels;
	#endregion

	#region UNITY_CALLBACK
	public void Awake()
	{
		InitLevel();
	}
	#endregion

	#region INITIALIZATION
	public void InitLevel()
	{
		levels = new List<Level>();
		for (int i = 0; i < levelsData.Windows.Length; i++) {
			Level lvl = new Level(levelsData.Windows[i]);
			levels.Add(lvl);
		}

		UIManager.Instance.windowShop.SetData(levels);
	}
	#endregion

	#region FUNCTION
	public void LevelCompleted(string levelKeyName)
	{
		for (int i = 0; i < levels.Count; i++)
			if (levels[i].data.KeyName == levelKeyName) {
				levels[i].status = Level.Status.COMPLETE;
				return;
			}

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