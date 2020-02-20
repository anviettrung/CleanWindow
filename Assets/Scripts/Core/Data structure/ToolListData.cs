using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool List Data", menuName = "List/Tool List", order = 1)]
public class ToolListData : ScriptableObject
{
	// Data
	[SerializeField] protected ToolData[] tools;

	// Access
	public ToolData[] Tools { get { return tools; } }
}
