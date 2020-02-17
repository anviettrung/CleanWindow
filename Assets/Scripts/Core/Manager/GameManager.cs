using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region DATA
	[SerializeField] private GeneralResource generalResources;
	public GeneralResource GeneralResources { get { return generalResources; } }
	#endregion

	#region UNITY_CALLBACK
	protected void Start()
	{
		// Load game data
		LevelManager.Instance.Load();
		// Initialization
		UIManager.Instance.ShowAll(false);
		UIManager.Instance.ShowTopUI(true);
		UIManager.Instance.ShowStartButton(true);

		// Game Intro

		// Open level
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
		PlayerInput.Instance.UnlockInput();
	}

	public void ResetLevel()
	{
		//PlayerInput.Instance.LockInput();
	}
	#endregion
}
