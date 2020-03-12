using UnityEngine;
using Cinemachine;

public class FWCam : MonoBehaviour
{
	public SpriteRenderer rink;

	void Start()
	{
		// Force fixed width
		float ratio = (float)Screen.height / (float)Screen.width;
		GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = (float)rink.bounds.size.x * ratio * 0.5f;
	}
}
