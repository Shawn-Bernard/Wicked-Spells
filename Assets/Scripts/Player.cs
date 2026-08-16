using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions action;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 inputDirection;
    private Vector3 lastInputDirection;

    [SerializeField] private BoolEvent isWalking;

    [SerializeField] private LayerMask interactablesMask;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleInteractions()
    {
        Vector3 moveDirection = inputDirection;

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInputDirection,out RaycastHit hitInfo, interactDistance,interactablesMask))
        {
            if (hitInfo.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.OnInteract();
            }
        }
    }

    private void HandleMovement()
    {

        Vector3 moveDirection = inputDirection;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
                else
                {
                    Debug.Log("Cannot move");
                }
            }

        }

        if (canMove)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            transform.position += moveDirection * moveDistance;
            //Debug.Log(moveDirection);
        }
    }

    private void Movement(Vector2 inputVector)
    {
        inputDirection = new Vector3(inputVector.x,0, inputVector.y).normalized;

        if (inputDirection != Vector3.zero)
        {
            lastInputDirection = inputDirection;
        }
    }

    private void IsWalking()
    {
        isWalking.RaiseEvent(inputDirection != Vector3.zero);
    }
    private void OnEnable()
    {
        action.MoveEvent += Movement;
        action.MoveStartedEvent += IsWalking;
        action.MoveCanceledEvent += IsWalking;
        action.InteractStartedEvent += HandleInteractions;
    }

    private void OnDisable()
    {
        action.MoveEvent -= Movement;
        action.MoveStartedEvent -= IsWalking;
        action.MoveCanceledEvent -= IsWalking;
        action.InteractStartedEvent -= HandleInteractions;
    }
}
