using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
public class ExplodeExe : MonoBehaviour
{
	private Explodable _explodable;

	void Start()
	{
		_explodable = GetComponent<Explodable>();
	}

	public void Action()
	{
		_explodable.explode((new GameObject(gameObject.name)).transform);
		ExplosionForce ef = FindObjectOfType<ExplosionForce>();
		ef.doExplosion(transform.position);
	}
}
