﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Window Data", menuName = "Window Data", order = 1)]
public class WindowData : ScriptableObject
{
	// Data
	[SerializeField] protected string keyName;
	[SerializeField] protected string windowName;
	[SerializeField] protected Sprite picture;
	[SerializeField] protected Color dirtyColor = new Color(0.5f, 0.5f, 0.5f, 0.9f);

	// Access
	public string KeyName { get { return keyName; } }
	public string WindowName { get { return windowName;  } }
	public Sprite Picture { get { return picture;  } }
	public Color DirtyColor { get { return dirtyColor;  } }

}
