using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region DATA
	[Header("Resources")]
	[SerializeField] private GeneralResource generalResources = null;
	public GeneralResource GeneralResources { get { return generalResources; } }

	#endregion

	#region UNITY_CALLBACK
	protected void Start()
	{
		// Load game data
		LevelManager.Instance.Load();
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
	}
	#endregion

	#region FUNCTION

	#endregion
}
