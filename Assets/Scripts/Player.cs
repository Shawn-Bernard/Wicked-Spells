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
        moveDirection = moveDirection.normalized;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward,moveDirection, Time.deltaTime * rotationSpeed);
        //Debug.Log(movementVector);
    }

    private void Movement(Vector2 inputVector)
    {
        moveDirection = new Vector3(inputVector.x,0, inputVector.y);
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
