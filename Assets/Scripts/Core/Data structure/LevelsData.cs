using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level List Data", menuName = "List/Level List", order = 0)]
public class LevelsData : ScriptableObject
{
	// Data
	[SerializeField] protected WindowData[] windows;

	// Access
	public WindowData[] Windows { get { return windows; } }
}
