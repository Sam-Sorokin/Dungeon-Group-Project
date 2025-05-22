using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    [Header("Audio")]
    PlayerFootsteps footSteps;
    [Header("Speeds")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float slideInitialSpeed = 15f;
    public float slideDrag = 25f; // Higher = faster stop
    public float jumpSpeed = 8.0f;
    public float dodgeSpeed = 25f;
    public float dodgeDuration = 0.2f;
    public float dodgeCooldown = 2f;

    [Header("Gravity")]
    public float gravity = 20.0f;

    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private bool isSliding = false;
    private bool canMove = true;
    private bool isGrounded => characterController.isGrounded;
    private bool isDodging = false;
    private bool canDodge = true;
    private bool isSprinting = false;

    [Header("Heights")]
    private float standingHeight = 2.0f;
    private float crouchingHeight = 1.0f;
    private float standingCameraHeight = 1.75f;
    private float crouchingCameraHeight = 1.0f;

    private Vector3 slideVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        footSteps = GetComponent<PlayerFootsteps>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial heights
        characterController.height = standingHeight;
        Vector3 camPos = playerCamera.transform.localPosition;
        camPos.y = standingCameraHeight;
        playerCamera.transform.localPosition = camPos;
    }

    void Update()
    {
        HandleMovement();
        HandleLook();
        HandleDodge();
        HandleSprintToggle();
        HandleSlide();
    }

    void HandleMovement()
    {
        if (!canMove || isDodging || isSliding) return;

        float moveSpeed = isSprinting ? runningSpeed : walkingSpeed;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = moveSpeed * Input.GetAxis("Vertical");
        float curSpeedY = moveSpeed * Input.GetAxis("Horizontal");

        moveDirection.x = (forward * curSpeedX + right * curSpeedY).x;
        moveDirection.z = (forward * curSpeedX + right * curSpeedY).z;

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = -1f;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (footSteps != null)
        {
            float inputAmount = Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical"));
            bool isMoving = inputAmount > 0.1f;
            bool isAboveThreshold = isMoving && moveSpeed > (walkingSpeed * 0.5f);

            if (isGrounded && isAboveThreshold)
            {
                if (!footSteps.playerAudio.isPlaying)
                    footSteps.PlayFootStepSound();

                footSteps.playerAudio.pitch = moveSpeed / walkingSpeed;
            }
            else
            {
                if (footSteps.playerAudio.isPlaying)
                    footSteps.StopFootStepSound();
            }

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
            if (inputDirection.magnitude > 0)
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

    void HandleSprintToggle()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = !isSprinting;
        }
    }

    void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding)
        {
            StartCoroutine(SlideRoutine());
        }
    }

    IEnumerator SlideRoutine()
    {
        isSliding = true;

        // Set crouching height for sliding
        characterController.height = crouchingHeight;
        Vector3 camPos = playerCamera.transform.localPosition;
        camPos.y = crouchingCameraHeight;
        playerCamera.transform.localPosition = camPos;

        // Set initial slide velocity in the facing direction (XZ only)
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();
        slideVelocity = forward * slideInitialSpeed;

        bool jumpedOutOfSlide = false;

        while (slideVelocity.magnitude > 0.5f)
        {
            // If Jump is pressed, cancel slide and apply jump force
            if (Input.GetButtonDown("Jump"))
            {
                // Cancel slide and keep half of the momentum
                moveDirection = slideVelocity * 0.5f;

                // Apply vertical jump speed
                moveDirection.y = jumpSpeed;

                // Mark jump was triggered during slide
                jumpedOutOfSlide = true;

                // Exit the loop early, jump has been initiated
                break;
            }

            // Cancel slide if player is no longer grounded (falling off ledge)
            if (!characterController.isGrounded)
            {
                break;
            }

            // Decelerate slide over time
            slideVelocity = Vector3.MoveTowards(slideVelocity, Vector3.zero, slideDrag * Time.deltaTime);

            // Apply movement (keeping y grounded)
            Vector3 move = slideVelocity * Time.deltaTime;
            move.y = -1f;
            characterController.Move(move);

            yield return null;
        }

        // If jump occurred, exit the slide routine here
        if (jumpedOutOfSlide)
        {
            // Transition back to standing height (exit crouch)
            characterController.height = standingHeight;
            camPos.y = standingCameraHeight;
            playerCamera.transform.localPosition = camPos;

            // Apply the movement with jump force
            characterController.Move(moveDirection * Time.deltaTime);

            isSliding = false;

            // Now handle gravity and movement properly (fall or jump) in Update
            yield break; // exit early so jump is processed
        }

        // If no jump, cancel slide on falling off a ledge
        // Transition to standing height
        characterController.height = standingHeight;
        camPos.y = standingCameraHeight;
        playerCamera.transform.localPosition = camPos;

        isSliding = false;
    }


}