using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isMy;
    [SerializeField] private float speed;
    [SerializeField] private CinemachineVirtualCamera vcam;

    void Start()
    {

    }

    void Update()
    {
        if (!isMy) return;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        transform.position += (transform.right * h + transform.forward * v) * speed * Time.deltaTime;
        transform.Rotate(0, x * 2, 0);
        var euler = Quaternion.Euler(vcam.transform.rotation.eulerAngles.x - y * 2, transform.rotation.eulerAngles.y, 0);
        vcam.transform.rotation = euler;
    }
}
