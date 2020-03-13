using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleControl : Singleton<TimeScaleControl>
{
	public IEnumerator DecayOverTime(float startScale, float endScale, float duration)
	{
		Time.timeScale = startScale;
		float elapsed = 0;

		while (elapsed < duration) {

			elapsed += Time.unscaledDeltaTime;

			Time.timeScale = Mathf.Lerp(startScale, endScale, elapsed / duration);

			yield return new WaitForEndOfFrame();
		}

		Time.timeScale = endScale;
	}
}
