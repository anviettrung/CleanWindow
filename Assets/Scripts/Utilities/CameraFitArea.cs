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
		if (isVerticalFit) {
			verticalFit();
		} else if (isHorizontalFit) {
			horizontalFit();
		} 

		AlignVerticalMiddle();
	}

	// Top and bottom edges of the area touch the camera edges
	void verticalFit()
	{
		Camera.main.orthographicSize = rink.bounds.size.y / 2;

	}

	// Left and right edges of the area touch the camera edges
	void horizontalFit()
	{
		Camera.main.orthographicSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;
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
