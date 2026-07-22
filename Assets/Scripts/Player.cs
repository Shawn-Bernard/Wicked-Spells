using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions action;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 moveDirection;
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

    private void OnEnable()
    {
        action.MoveEvent += Movement;
    }

    private void OnDisable()
    {
        action.MoveEvent -= Movement;
    }
}
