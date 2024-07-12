using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public CharacterController CharacterController
    {
        get { return characterController = characterController ?? GetComponent<CharacterController>(); }
    }

    private Camera characterCamera;
    public Camera CharacterCamera
    {
        get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }
    }

    private Animator characterAnimator;
    public Animator CharacterAnimator
    {
        get { return characterAnimator = characterAnimator ?? GetComponent<Animator>(); }
    }

    private Vector2 moveDirection;

    private Vector3 move;
    private Vector3 verticalVelocity = Vector3.zero;

    private float rotationAngle;
    private float gravity = -30f;

    private bool isJumping;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float speedMovement;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight;


    private void OnEnable()
    {
        InputController.MoveEvent += HandleMove;
        InputController.JumpEvent += Jump;
    }

    private void OnDisable()
    {
        InputController.MoveEvent -= HandleMove;
        InputController.JumpEvent -= Jump;
    }

    private void Update()
    {
        move = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        CheckGround();

        verticalVelocity.y = verticalVelocity.y + gravity * Time.deltaTime;

        var rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * move.normalized;
        var verticalMovement = Vector3.up * verticalVelocity.y;

        if (move.magnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;

            CharacterAnimator.SetBool("isRun", true);
        }
        if (move.magnitude == 0.0f)
        {
            CharacterAnimator.SetBool("isRun", false);
        }

        CharacterController.Move((verticalMovement + rotatedMovement * speedMovement) * Time.deltaTime);

        Quaternion currentRotation = CharacterController.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        CharacterController.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void CheckGround()
    {
        if (isJumping && verticalVelocity.y < 0.0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.2f, groundMask))
            {
                isJumping = false;
            }
        }

        if (isJumping)
        {
            CharacterAnimator.SetBool("isJump", true);
        }
        else
        {
            CharacterAnimator.SetBool("isJump", false);
        }
    }

    private void HandleMove(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Jump()
    {
        CharacterAnimator.SetBool("isJump", true);

        verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);

        isJumping = true;
    }
}
