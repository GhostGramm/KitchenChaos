
using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform holdPoint;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;
    private KitchenObject kitchenObject;

    public Action<ClearCounter> OnSetSelectedCounter;

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
        if(selectedCounter != null) selectedCounter.Interact(this);
    }
    private void HandleInteractions()
    {
        Vector2 inputVec = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVec.x, 0f, inputVec.y);

        if(moveDir != Vector3.zero)
            lastInteractDirection = moveDir;

        float distance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, distance, counterLayerMask))
        {
            if(hit.transform.TryGetComponent(out ClearCounter counter))
            {
                SetClearCounter(counter);
            }
            else
            {
                SetClearCounter(null);
            }
        }
        else
        {
            SetClearCounter(null);
        }
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

    private void SetClearCounter(ClearCounter counter)
    {
        selectedCounter = counter;
        OnSetSelectedCounter?.Invoke(counter);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return holdPoint;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;
    }

    public bool IsKitchenObjectAvailable()
    {
        return kitchenObject != null;
    }
}
