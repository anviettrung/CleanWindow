using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraTransition : MonoBehaviour
{
	protected Camera targetCamera;

	[System.Serializable]
	public class ViewInfo
	{
		public string viewName;
		public Vector3 targetPosition;
		public float targetSize = 10;
		public float transitionTime = 1;
	}

	public List<ViewInfo> views;

	protected void Awake()
	{
		targetCamera = GetComponent<Camera>();

	}

	public ViewInfo GetViewInfoByName(string name)
	{
		foreach (ViewInfo info in views)
			if (info.viewName == name)
				return info;

		return null;
	}

	public void AnimMove(string keyName)
	{
		ViewInfo info = GetViewInfoByName(keyName);

		if (info != null) {
			StartCoroutine(IEMove(info, info.transitionTime));
		}
	}


	public void AnimMove(string keyName, float transitionTime)
	{
		ViewInfo info = GetViewInfoByName(keyName);

		if (info != null) {
			StartCoroutine(IEMove(info, transitionTime));
		}
	}

	public void ForceMove(string keyName)
	{
		ViewInfo info = GetViewInfoByName(keyName);

		if (info != null) {
			SetCameraSettingAs(info);
		}
	}

	protected void SetCameraSettingAs(ViewInfo inf)
	{
		transform.position = inf.targetPosition;
		targetCamera.orthographicSize = inf.targetSize;
	}

	public IEnumerator IEMove(ViewInfo inf, float time)
	{
		Vector3 startPosition = transform.position;
		float startSize = targetCamera.orthographicSize;

		float elapsedTime = 0;
		while (elapsedTime < time) {

			elapsedTime += Time.deltaTime;
			float weight = (time - elapsedTime) / time;

			transform.position = Vector3.Lerp(inf.targetPosition, startPosition, weight);
			targetCamera.orthographicSize = Mathf.Lerp(inf.targetSize, startSize, weight);

			yield return new WaitForEndOfFrame();
		}

		SetCameraSettingAs(inf);
	}
}
