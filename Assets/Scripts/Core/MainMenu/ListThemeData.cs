using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List Theme Data", menuName = "List/Theme", order = 0)]
public class ListThemeData : ScriptableObject
{
    [SerializeField] Theme[] themes;

    public Theme[] Theme { get { return this.themes; } }
}
