using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaterDropData
{
	public float duration;
	public float speed;
	public float scalerStart;
	public float scalerEnd;

	public float startDropTime;
	public float elapsedDropTime;
	public Vector2 dropOrigin;

	public WaterDropData(Vector2 _dropOrigin, float _duration = 3, float _speed = 50, 
							float _scalerStart = 0.8f, float _scalerEnd = 0.1f)
	{
		dropOrigin = _dropOrigin;
		duration = _duration;
		speed = _speed;
		scalerStart = _scalerStart;
		scalerEnd = _scalerEnd;
	}
}

public class WaterDrop : MonoBehaviour
{ 
	public TextureDrawer target;

	[Header("General Setting")]
	public float dropRangeX = 250;
	public int maxDrop = 10;

	[Header("Duration")]
	public float durationMin = 1;
	public float durationMax = 3;

	[Header("Speed")]
	public float speedMin = 50;
	public float speedMax = 100;

	[Header("Brush Scale Start")]
	public float brushScaleStartMin = 0.8f;
	public float brushScaleStartMax = 0.6f;

	[Header("Brush Scale End")]
	public float brushScaleEndMin = 0;
	public float brushScaleEndMax = 0.5f;


	public List<WaterDropData> drops;

	public void Start()
	{
		target.onDrawAtVector2.AddListener(OnDrawAt);
		drops = new List<WaterDropData>();
	}

	public void Update()
	{
		Debug.Log(drops.Count);
		for (int i = 0; i < drops.Count; i++) {
			WaterDropData drop = drops[i];
			drop.elapsedDropTime = Time.time - drop.startDropTime;
			if (drop.elapsedDropTime < drop.duration) {
				float weight = drop.elapsedDropTime * 0.33f;
				target.DrawAt(drop.dropOrigin + Vector2.down * drop.speed * drop.elapsedDropTime, 
					target.brushScale * Mathf.Lerp(drop.scalerStart, drop.scalerEnd, weight), false);
			}
			else {
				drops.Remove(drop);
			}
		}
	}

	public WaterDropData FindDropNearPosition(Vector2 pos)
	{
		foreach (WaterDropData drop in drops) {
			if (Mathf.Abs(pos.x - drop.dropOrigin.x) < dropRangeX) {
				return drop;
			}
		}
		return null;
	}

	public void OnDrawAt(Vector2 targetPosition)
	{
		WaterDropData d = FindDropNearPosition(targetPosition);

		if (d == null) {
			if (drops.Count < maxDrop) {
				WaterDropData newDrop = new WaterDropData(
					targetPosition,
					Random.Range(durationMin, durationMax),
					Random.Range(speedMin, speedMax),
					Random.Range(brushScaleStartMin, brushScaleStartMax),
					Random.Range(brushScaleEndMin, brushScaleEndMax));
				newDrop.startDropTime = Time.time;

				drops.Add(newDrop);
			}
		} else {
			d.duration += Time.deltaTime;
		}
	}
}
