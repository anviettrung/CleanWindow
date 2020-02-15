using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraFitArea))]
public class CameraFitAreaEditor : Editor
{
	CameraFitArea mTarget;

	private void Awake()
	{
		mTarget = (CameraFitArea)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Align")) {
			mTarget.Align();
		}
	}
}
