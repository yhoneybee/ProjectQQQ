using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Serialization<T>
{
    public Serialization(List<T> targets) => target = targets;
    public List<T> target;
}
