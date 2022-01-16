using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float moveSpeed;
    private bool isMove;
    private bool IsRun
    {
        get => isRun;
        set
        {
            if (IsCrouch) return;
            isRun = value;
            moveSpeed = isRun ? runSpeed : walkSpeed;
        }
    }
    private bool isRun;
    private bool IsGround => Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    private bool IsCrouch
    {
        get => isCrouch;
        set
        {
            isCrouch = value;
            moveSpeed = isCrouch ? crouchSpeed : walkSpeed;
            StopAllCoroutines();
            StartCoroutine(ECrouch(isCrouch ? crouchPosY : originPosY));
        }
    }
    private bool isCrouch;
    [SerializeField] private float crouchPosY;
    [SerializeField] private float originPosY;
    [SerializeField] private float jumpForce;
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

    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Camera theCamera;
    [SerializeField] private Rigidbody rigid;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!rigid) rigid = GetComponent<Rigidbody>();
        IsRun = false;
        IsCrouch = false;
    }

    private void Update()
    {
        if (transform.CompareTag("OtherPlayer"))
        {
            // TODO : Server에서 위치와 회전값을 받고 적용시키기
        }
        else
        {
            if (!IsGround) IsCrouch = false;
            else if (Input.GetKey(KeyCode.LeftControl)) IsCrouch = true;
            else IsCrouch = false;

            if (Input.GetKey(KeyCode.Space) && IsGround)
                rigid.velocity = transform.up * jumpForce;

            TryRun();

            Move();

            CameraRotation();

            CharacterRotation();
        }
    }

    private IEnumerator ECrouch(float posY)
    {
        var wait = new WaitForSeconds(0.01f);
        while (Mathf.Abs(theCamera.transform.localPosition.y - posY) > 0.01f)
        {
            theCamera.transform.localPosition = Vector3.Lerp(theCamera.transform.localPosition, Vector3.up * posY, Time.deltaTime * 15);
            yield return wait;
        }
        theCamera.transform.localPosition = Vector3.up * posY;
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            IsRun = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            IsRun = false;
    }

    private void CharacterRotation()
    {
        float rotationY = Input.GetAxisRaw("Mouse X");
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(Vector3.up * rotationY * lookSensitivity));
    }

    private void CameraRotation()
    {
        float rotationX = Input.GetAxisRaw("Mouse Y");
        CurrentCameraRotationX = Mathf.Clamp(CurrentCameraRotationX - rotationX * lookSensitivity, -cameraRotationLimit, cameraRotationLimit);
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        var x = transform.right * h;
        var z = transform.forward * v;

        rigid.MovePosition(transform.position + (x + z).normalized * moveSpeed * Time.deltaTime);

        isMove = h != 0 || v != 0;
    }
}
