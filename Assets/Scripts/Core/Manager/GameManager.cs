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
		PlayerInput.Instance.LockInput("manager");
		// Load game data
		LevelManager.Instance.Load();
		// Initialization
		UIManager.Instance.ShowAll(false);
		UIManager.Instance.ShowTopUI(true);
		UIManager.Instance.ShowStartButton(true);

		// Game Intro

		// Open level
		LevelManager.Instance.GenLevel(0);
	}

	private void OnApplicationQuit()
	{
		// Save game
		LevelManager.Instance.Save();
	}
	#endregion

	#region FUNCTION
	public void StartLevel()
	{
		PlayerInput.Instance.UnlockInput("manager");
	}

	public void ResetLevel()
	{
		LevelManager.Instance.GenLevel(0);
		//PlayerInput.Instance.LockInput();
	}
	#endregion
}
