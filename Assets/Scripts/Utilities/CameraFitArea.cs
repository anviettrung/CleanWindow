using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFitArea : MonoBehaviour
{
	public SpriteRenderer rink;
	public bool isVerticalFit;
	public bool isHorizontalFit;

	void Start()
	{
		ForceAlign();
	}

	public void ForceAlign()
	{
		if (isVerticalFit) {
			verticalFit();
		} else if (isHorizontalFit) {
			horizontalFit();
		}

		AlignVerticalMiddle();
	}

	public void FlexibleAlign()
	{
		float screenRatio = (float)Screen.width / (float)Screen.height;

		float rinkRatio = rink.bounds.size.x / rink.bounds.size.y;

		if (screenRatio < rinkRatio)
			verticalFit();
		else
			horizontalFit();

		AlignVerticalMiddle();
	}

	// Top and bottom edges of the area touch the camera edges
	void verticalFit()
	{
		Camera.main.orthographicSize = rink.bounds.size.y * 0.5f;

	}

	// Left and right edges of the area touch the camera edges
	void horizontalFit()
	{
		Camera.main.orthographicSize = ((float)rink.bounds.size.x * 0.5f) * ((float)Screen.height / (float)Screen.width);
	}

	void AlignLeft()
	{
		Vector3 r = rink.transform.position;
		Vector3 c = Camera.main.transform.position;
		float camWidth = rink.bounds.size.y * Camera.main.aspect;

		Camera.main.transform.position = new Vector3(r.x - 0.5f * (rink.bounds.size.x - camWidth), c.y, c.z);
	}

	void AlignRight()
	{
		Vector3 r = rink.transform.position;
		Vector3 c = Camera.main.transform.position;
		float camWidth = rink.bounds.size.y * Camera.main.aspect;

		Camera.main.transform.position = new Vector3(r.x + 0.5f * (rink.bounds.size.x - camWidth), c.y, c.z);
	}

	void AlignVerticalMiddle()
	{
		Vector3 c = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3(c.x, rink.transform.position.y, c.z);
	}
}
