using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public Action OnInteractPerformed;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        AddButtonCallbacks();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVec = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVec = inputVec.normalized;

        return inputVec;
    }

    private void AddButtonCallbacks()
    {
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj) => OnInteractPerformed?.Invoke();

}