using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Data", menuName = "Tool Data", order = 2)]
public class ToolData : ScriptableObject
{
	// Data
	[SerializeField] protected string keyName;
	[SerializeField] protected string toolName;
	[SerializeField] protected Sprite art;
	[SerializeField] protected ToolData.Type toolType;

	// Access
	public string KeyName { get { return keyName;  } }
	public string ToolName { get { return toolName;  } }
	public Sprite Art { get { return art;  } }
	public ToolData.Type ToolType { get { return toolType; } }

	public enum Type
	{
		CLEANER,
		GLASSER,
		BREAKER
	}
}
