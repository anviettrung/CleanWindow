using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Window Data", menuName = "Window Data", order = 1)]
public class ToolData : ScriptableObject
{
	// Data
	[SerializeField] protected Sprite art;

	// Access
	public Sprite Art { get { return art; } }
}
