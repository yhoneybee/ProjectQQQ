using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isMy;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Bullet bullet;
    private bool isJumping;

    void Start()
    {
        Physics.gravity = Vector3.down * 30;
    }

    void Update()
    {
        if (!isMy) return;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, vcam.transform.position, vcam.transform.rotation);
        }

        isJumping = !Physics.Raycast(transform.position + Vector3.down * 1.4f, Vector3.down, 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position + Vector3.down * 1.4f, Vector3.down * 0.2f, Color.red, 0.1f);

        transform.position += (transform.right * h + transform.forward * v) * speed * Time.deltaTime;
        transform.Rotate(0, x * 2, 0);
        var euler = Quaternion.Euler(vcam.transform.rotation.eulerAngles.x - y * 2, transform.rotation.eulerAngles.y, 0);
        vcam.transform.rotation = euler;
    }
}
