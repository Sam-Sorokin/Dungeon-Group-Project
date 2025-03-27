using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : Damagable
{
    //---------Speeds-----------
    [Header("Speeds")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float crouchSpeed = 4f;
    public float wallRunSpeed = 10f;
    public float wallRunUpwardSpeed = 1.5f;
    public float jumpSpeed = 8.0f;
    public float dodgeSpeed = 25f; // Speed of the dodge
    public float dodgeDuration = 0.2f; // How long the dodge lasts
    public float dodgeCooldown = 2f; // Cooldown time

    [Header("Gravity - Speed of falling")]
    public float wallRunGravity = 5f;
    public float gravity = 20.0f;

    //-----------CameraVars-----------
    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    [Header("Wall Run Info")]
    private bool isWallRunning = false;
    private float wallRunTimer = 0f;
    private float maxWallRunTime = 1.5f;
    private Vector3 lastWallNormal;

    [Header("Character Controller")]
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [Header("Bools")]
    private bool isCrouching = false;
    private bool canMove = true;
    private bool isGrounded => characterController.isGrounded;
    private bool isDodging = false;
    private bool canDodge = true;

    [Header("Floats")]
    private float standingHeight = 2.0f;
    private float crouchingHeight = 1.0f;
    private float standingCameraHeight = 1.75f;
    private float crouchingCameraHeight = 1.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleWallRun();
        HandleDodge();
    }

    void HandleMovement()
    {
        if (!canMove || isDodging) return;

        float moveSpeed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) && !isCrouching ? runningSpeed : walkingSpeed);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = moveSpeed * Input.GetAxis("Vertical");
        float curSpeedY = moveSpeed * Input.GetAxis("Horizontal");

        moveDirection.x = (forward * curSpeedX + right * curSpeedY).x;
        moveDirection.z = (forward * curSpeedX + right * curSpeedY).z;

        if (isGrounded)
        {
            isWallRunning = false;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = -1f;
            }
        }
        else if (!isWallRunning)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleLook()
    {
        if (!canMove) return;

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock) && canDodge && !isDodging)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (inputDirection.magnitude > 0) // Only dodge if moving
            {
                StartCoroutine(DodgeRoutine(inputDirection));
            }
        }
    }

    IEnumerator DodgeRoutine(Vector3 inputDirection)
    {
        isDodging = true;
        canDodge = false;
        Vector3 dodgeDirection = (transform.TransformDirection(inputDirection)).normalized * dodgeSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < dodgeDuration)
        {
            characterController.Move(dodgeDirection * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDodging = false;
        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true;
    }

    void HandleWallRun()
    {
        if (isGrounded) return;

        RaycastHit leftWallHit, rightWallHit;
        bool leftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 1f);
        bool rightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, 1f);

        if ((leftWall || rightWall) && Input.GetAxis("Vertical") > 0)
        {
            Vector3 wallNormal = leftWall ? leftWallHit.normal : rightWallHit.normal;
            if (wallNormal != lastWallNormal)
            {
                wallRunTimer = 0f;
                lastWallNormal = wallNormal;
            }

            if (!isWallRunning)
            {
                isWallRunning = true;
                moveDirection.y = 0;
            }

            wallRunTimer += Time.deltaTime;
            if (wallRunTimer >= maxWallRunTime)
            {
                isWallRunning = false;
                return;
            }

            moveDirection = transform.forward * wallRunSpeed;
            moveDirection.y = wallRunUpwardSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                isWallRunning = false;
                moveDirection = (wallNormal * 6f + Vector3.up * jumpSpeed).normalized * jumpSpeed;
                return;
            }
        }
        else
        {
            isWallRunning = false;
        }
    }
}