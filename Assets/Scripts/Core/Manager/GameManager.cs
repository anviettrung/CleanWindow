using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private GeneralResource generalResources;
	public GeneralResource GeneralResources { get { return generalResources; } }

	protected void Start()
	{
		// Load game data

		// Initialization

		// Game Intro

		// Open level
	}

	public void StartLevel()
	{
		PlayerInput.Instance.UnlockInput();
	}

	public void ResetLevel()
	{
		//PlayerInput.Instance.LockInput();
	}

	private void OnApplicationQuit()
	{
		// Save game
	}
}
