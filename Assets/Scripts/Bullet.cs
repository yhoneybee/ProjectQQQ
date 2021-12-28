using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private void OnEnable()
    {
        Invoke(nameof(ReturnObj), 2);
    }

    private void ReturnObj()
    {
        ObjPool.ReturnObj(this);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
