using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapView : MonoBehaviour
{
	public CameraFitArea fitter;
	public List<SpriteRenderer> rinks;
	private int track = 0;

	public void Next()
	{
		if (track < rinks.Count - 1)
			track++;
		else
			track = 0;

		fitter.rink = rinks[track];
		fitter.Align();
	}
}
