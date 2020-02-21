using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	#region DATA
	[Header("Resources")]
	[SerializeField] private GeneralResource generalResources = null;
	public GeneralResource GeneralResources { get { return generalResources; } }

	#endregion

	#region UNITY_CALLBACK
	protected void Awake()
	{
	}

	protected void Start()
	{
		// Load game data
		if (!PlayerPrefs.HasKey("Old user")) {
			// Setting first time play
			LevelManager.Instance.levels[0].status = Level.Status.UNLOCK; // Unlock first level
			ToolManager.Instance.cleaner.tools[0].status = ToolItem.Status.UNLOCK; // Unlock first cleaner tool
			ToolManager.Instance.glasser.tools[0].status = ToolItem.Status.UNLOCK; // Unlock first glasser tool

			PlayerPrefs.SetInt("Old user", 1);
		} else {
			// Setting if played
			LevelManager.Instance.Load();
			ToolManager.Instance.Load();
		}
		// Initialization

		// Game Intro

		// Open level
		LevelManager.Instance.OpenLastestLevel();
	}

	private void OnApplicationQuit()
	{
		UIManager.Instance.ShowAll(false);
		// Save game
		LevelManager.Instance.Save();
		ToolManager.Instance.Save();

		PlayerPrefs.Save();
	}
	#endregion

	#region FUNCTION

	#endregion
}

// Events
public class StringEvent : UnityEvent<string> { }

