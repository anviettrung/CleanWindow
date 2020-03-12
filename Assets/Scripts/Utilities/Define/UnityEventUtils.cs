using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Define list of Unity event

public class UnityStringEvent  : UnityEvent<string>  { }
public class UnityIntEvent     : UnityEvent<int>     { }
public class UnityFloatEvent   : UnityEvent<float>   { }
public class UnityVector2Event : UnityEvent<Vector2> { }