using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public bool isRunning = false;
    public bool isCrouching = false;
    public bool isJumping = false;

    public float crouchHeight = 0.5f;
    public float crouchSpeed = 2.0f;
    public float crouchTransitionSpeed = 10f;
    private float originalHeight;
    public float crouchCameraOffset = -0.5f;
    private Vector3 cameraStandPosition;
    private Vector3 cameraCrouchPosition;

    AudioSource audio;
    public AudioClip walk, sprint, jump, jump_landed;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        originalHeight = characterController.height;
        audio.enabled = true;
        cameraStandPosition = playerCamera.transform.localPosition;
        cameraCrouchPosition = cameraStandPosition + new Vector3(0, crouchCameraOffset, 0);

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run

        if (!isCrouching) isRunning = Input.GetButton("Sprint");
        //isJumping = Input.GetButton("Jump");

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if ((Input.GetButton("Horizontal") | Input.GetButton("Vertical")) && characterController.isGrounded)
        {
            if (audio.clip != jump_landed | !audio.isPlaying)
            {
                //audio.enabled = true;
                if (isRunning)
                {
                    audio.clip = sprint;
                    audio.loop = true;
                    if (!audio.isPlaying) audio.Play();
                }
                else
                {
                    audio.clip = walk;
                    audio.loop = true;
                    if (!audio.isPlaying) audio.Play();
                }
            }
        }
        else if ((audio.clip != jump && audio.clip != jump_landed) | !audio.isPlaying)
        {
            //audio.enabled = false;
            audio.Stop();
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            audio.clip = jump;
            audio.loop = false;
            //audio.enabled = true;
            audio.Play();
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            isJumping = true;
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else 
        {
            if (isJumping)
            {
                isJumping = false;
                audio.clip = jump_landed;
                audio.loop = false;
                //audio.enabled = true;
                audio.Play();
            }
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && canMove)
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                characterController.height = crouchHeight;
                walkingSpeed = crouchSpeed;
            }
        }

        if (isCrouching)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraCrouchPosition, crouchTransitionSpeed * Time.deltaTime);
        }
        else if (!Physics.Raycast(playerCamera.transform.position, Vector3.up, 0.8f))
        {
            characterController.height = originalHeight;
            walkingSpeed = 7.5f;
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, cameraStandPosition, crouchTransitionSpeed * Time.deltaTime);
        }
    }
}