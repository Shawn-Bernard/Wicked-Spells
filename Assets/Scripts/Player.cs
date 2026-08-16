using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions action;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 moveDirection;

    [SerializeField] private BoolEvent isWalking;

    private void Update()
    {

        
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirection,moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
            }

        }

        if (canMove)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            transform.position += moveDirection * moveDistance;
            //Debug.Log(movementVector);
        }
    }

    private void Movement(Vector2 inputVector)
    {
        moveDirection = new Vector3(inputVector.x,0, inputVector.y).normalized;
    }

    private void IsWalking()
    {
        isWalking.RaiseEvent(moveDirection != Vector3.zero);
    }
    private void OnEnable()
    {
        action.MoveEvent += Movement;
        action.MoveStartedEvent += IsWalking;
        action.MoveCanceledEvent += IsWalking;
    }

    private void OnDisable()
    {
        action.MoveEvent -= Movement;
        action.MoveStartedEvent -= IsWalking;
        action.MoveCanceledEvent -= IsWalking;
    }
}
