using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Levels Data", menuName = "Levels", order = 2)]
public class LevelsData : ScriptableObject
{
	// Data
	[SerializeField] protected WindowData[] windows;

	// Access
	public WindowData[] Windows { get { return windows; } }
}
