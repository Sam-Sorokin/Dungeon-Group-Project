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
    //
    public float crouchSpeed = 4f;
    //
    public float wallRunSpeed = 10f;
    public float wallRunUpwardSpeed = 1.5f;
    //
    public float jumpSpeed = 8.0f;
    //---------Gravity------------
    [Header("Gravity - Speed of falling")]
    public float wallRunGravity = 5f;
    public float gravity = 20.0f;

    //-----------CameraVars-----------
    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    //-----------WallRunInfo----------
    [Header("Wall Run Info")]
    private bool isWallRunning = false;
    private float wallRunTimer = 0f;
    private float maxWallRunTime = 1.5f;
    private Vector3 lastWallNormal;
    //--------------------------------
    [Header("Character Controller")]
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [Header("Bools")]
    private bool isCrouching = false;
    private bool canMove = true;
    private bool isGrounded => characterController.isGrounded;

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
    }

    void HandleMovement()
    {
        if (!canMove) return;

        // Set movement speed (no sprinting while crouching)
        float moveSpeed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) && !isCrouching ? runningSpeed : walkingSpeed);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = moveSpeed * Input.GetAxis("Vertical");
        float curSpeedY = moveSpeed * Input.GetAxis("Horizontal");

        moveDirection.x = (forward * curSpeedX + right * curSpeedY).x;
        moveDirection.z = (forward * curSpeedX + right * curSpeedY).z;

        // Handle Crouch (Adjust CharacterController height & Camera position)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            characterController.height = crouchingHeight;
            //characterController.center = new Vector3(0, 0.5f, 0);
            playerCamera.transform.localPosition = new Vector3(0, crouchingCameraHeight, 0);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            characterController.height = standingHeight;
            //characterController.center = new Vector3(0, 1f, 0);
            playerCamera.transform.localPosition = new Vector3(0, standingCameraHeight, 0);
        }

        // Handle Jumping
        if (isGrounded)
        {
            isWallRunning = false;  // Reset wall run state when grounded

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = -1f; // Small downward force to keep grounded
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

    void HandleWallRun()
    {
        if (isGrounded) return;

        RaycastHit leftWallHit, rightWallHit;
        bool leftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 1f);
        bool rightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, 1f);

        if ((leftWall || rightWall) && Input.GetAxis("Vertical") > 0) // Only wall run if moving forward
        {
            Vector3 wallNormal = leftWall ? leftWallHit.normal : rightWallHit.normal;

            // If changing walls, reset wall run height
            if (wallNormal != lastWallNormal)
            {
                wallRunTimer = 0f;
                lastWallNormal = wallNormal;
            }

            if (!isWallRunning)
            {
                isWallRunning = true;
                moveDirection.y = 0; // Reset gravity effect
            }

            wallRunTimer += Time.deltaTime;

            if (wallRunTimer >= maxWallRunTime)
            {
                isWallRunning = false;
                return;
            }

            // Apply wall running movement
            moveDirection = transform.forward * wallRunSpeed;

            // Move upward very slowly
            moveDirection.y = wallRunUpwardSpeed;

            // **Wall Jumping Logic**
            if (Input.GetButtonDown("Jump"))
            {
                isWallRunning = false;
                moveDirection = (wallNormal * 6f + Vector3.up * jumpSpeed).normalized * jumpSpeed; // Push away from the wall
                return;
            }
        }
        else
        {
            isWallRunning = false;
        }
    }
}
