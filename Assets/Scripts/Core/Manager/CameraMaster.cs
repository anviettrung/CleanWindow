﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : Singleton<CameraMaster>
{
	public Animator mainAnimator;
	[System.Serializable]
	public enum View
	{
		FULL_SHOT,
		MEDIUM_SHOT
	}
	protected Dictionary<View, string> views = new Dictionary<View, string>();

	protected void Awake()
	{
		views.Add(View.FULL_SHOT, "FullShot");
		views.Add(View.MEDIUM_SHOT, "MediumShot");
	}

	public void TransitionToView(View v)
	{
		mainAnimator.SetTrigger(views[v]);
	}

	public void TransitionToView(string vName)
	{
		mainAnimator.SetTrigger(vName);
	}

}
