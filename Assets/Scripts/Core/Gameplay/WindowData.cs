using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Window Data", menuName = "Window Data", order = 1)]
public class WindowData : ScriptableObject
{
	// Data
	[SerializeField] protected string windowName;
	[SerializeField] protected Sprite picture;
	[SerializeField] protected Color mainColor;

	// Access
	public string WindowName { get { return windowName;  } }
	public Sprite Picture { get { return picture;  } }
	public Color MainColor { get { return mainColor;  } }

}
