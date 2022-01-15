using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private float CurrentCameraRotationX
    {
        get => currentCameraRotationX;
        set
        {
            currentCameraRotationX = value;
            theCamera.transform.localEulerAngles = Vector3.right * currentCameraRotationX;
        }
    }
    private float currentCameraRotationX;

    [SerializeField] private Camera theCamera;
    [SerializeField] private Rigidbody rigid;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!rigid) rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();

        CameraRotation();

        CharacterRotation();
    }

    private void CharacterRotation()
    {
        float rotationY = Input.GetAxisRaw("Mouse X");
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(Vector3.up * rotationY * lookSensitivity));
    }

    private void CameraRotation()
    {
        float rotationX = Input.GetAxisRaw("Mouse Y");
        CurrentCameraRotationX -= rotationX * lookSensitivity;
        CurrentCameraRotationX = Mathf.Clamp(CurrentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        var x = transform.right * h;
        var z = transform.forward * v;

        rigid.MovePosition(transform.position + (x + z).normalized * walkSpeed * Time.deltaTime);
    }
}
