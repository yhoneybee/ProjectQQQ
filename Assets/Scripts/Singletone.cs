using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;

public abstract class Singletone<T> : MonoBehaviour
{
    public static T Instance { get; protected set; }

    protected virtual void Awake()
    {
        Instance = GetComponent<T>();
    }
}
