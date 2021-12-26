using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;

public abstract class Singletone<T> : MonoBehaviour
    where T : class
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = GetComponent<T>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
