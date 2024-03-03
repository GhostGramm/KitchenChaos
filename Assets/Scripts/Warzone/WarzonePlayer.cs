
using System;
using UnityEngine;

public class WarzonePlayer : MonoBehaviour
{
    public static WarzonePlayer Instance { get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Instance already exists");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractPerformed += GameInput_OnInteractAction;
    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAction()
    {
        
    }
    private void HandleInteractions()
    {

    }

    private void HandleMovement()
    {
        Vector2 inputVec = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //cannot move only on the X

                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction;
                }
            }
        }

        if (canMove)
            transform.position += moveDir * moveDistance;

        //Face the movement direction
        float rotSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotSpeed);
    }

    public bool IsWalking() => isWalking;

}
