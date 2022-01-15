using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

using PooledObj = UnityEngine.GameObject;

public class ObjPool : Singletone<ObjPool>
{
    [SerializeField] private PooledObj originManagedObj;
    private List<PooledObj> pool = new List<PooledObj>();
    private Transform trnDisable;

    private void Start()
    {
        var obj = new GameObject("Disable");
        obj.transform.SetParent(transform);
        trnDisable = obj.transform;
    }

    public static PooledObj GetObj()
    {
        PooledObj obj = null;
        if (Instance.pool.Count > 0)
        {
            obj = Instance.pool.FirstOrDefault();
            Instance.pool.RemoveAt(0);
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate(Instance.originManagedObj);
        }
        return obj;
    }

    public static PooledObj GetObj(Vector3 pos)
    {
        var obj = GetObj();
        obj.transform.position = pos;
        return obj;
    }

    public static PooledObj GetObj(Vector3 pos, Transform parent, bool worldPositionStays = true)
    {
        var obj = GetObj();
        obj.transform.position = pos;
        obj.transform.SetParent(parent, worldPositionStays);
        return obj;
    }

    public static PooledObj GetObj(Vector3 pos, Quaternion rot)
    {
        var obj = GetObj();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        return obj;
    }

    public static PooledObj GetObj(Vector3 pos, Quaternion rot, Transform parent, bool worldPositionStays = true)
    {
        var obj = GetObj();
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.transform.SetParent(parent, worldPositionStays);
        return obj;
    }

    public static void ReturnObj(PooledObj obj)
    {
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.SetParent(Instance.trnDisable);
        obj.gameObject.SetActive(false);
        Instance.pool.Add(obj);
    }
}
