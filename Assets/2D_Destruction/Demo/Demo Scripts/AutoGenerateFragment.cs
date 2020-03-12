using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Explodable))]
public class AutoGenerateFragment : MonoBehaviour
{
	private Explodable _explodable;

	void Start()
	{
		_explodable = GetComponent<Explodable>();
		_explodable.fragmentInEditor();
	}
}
