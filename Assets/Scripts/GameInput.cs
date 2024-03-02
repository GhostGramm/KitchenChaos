using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVec = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVec = inputVec.normalized;

        return inputVec;
    }
}