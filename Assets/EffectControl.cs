using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
	public GameObject effect;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			effect.SetActive(true);
		} else if (Input.GetMouseButtonUp(0)) {
			effect.SetActive(false);
		}
	}
}
