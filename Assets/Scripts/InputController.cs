using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, GameInput.IGamePlayActions
{
    private GameInput gameInput;
    private GameInput.GamePlayActions gamePlayActions;

    public static event Action<Vector2> MoveEvent;
    public static event Action JumpEvent;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gamePlayActions = gameInput.GamePlay;

            gameInput.Enable();

            gameInput.GamePlay.SetCallbacks(this);

            CinemasineCam.DisableMovementEvent += DisableCharacterMovement;
        }
    }

    private void OnDisable()
    {
        if(gameInput != null)
        {
            gameInput.Disable();

            gameInput.GamePlay.RemoveCallbacks(this);

            CinemasineCam.DisableMovementEvent -= DisableCharacterMovement;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void DisableCharacterMovement(bool dis)
    {
        if (dis)
        {
            gameInput.Disable();
        }
        else
        {
            gameInput.Enable();
        }
    }
}
