using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward);
    }
}
