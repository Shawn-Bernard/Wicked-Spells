using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Player Actions",menuName = "Input Action Reader/Player Actions")]
public class PlayerInputActions : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputActions;

    #region Move Events
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction MoveStartedEvent;
    public event UnityAction MoveCanceledEvent;
    #endregion

    #region Attack Events
    public event UnityAction AttackStartedEvent;
    public event UnityAction AttackCanceledEvent;
    #endregion

    #region Crouch Events
    public event UnityAction CrouchStartedEvent;
    public event UnityAction CrouchCanceledEvent;
    #endregion

    #region Interact Events
    public event UnityAction InteractStartedEvent;
    public event UnityAction InteractCanceledEvent;
    #endregion

    #region Jump Events
    public event UnityAction JumpStartedEvent;
    public event UnityAction JumpCanceledEvent;
    #endregion
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            AttackCanceledEvent?.Invoke();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CrouchStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            CrouchCanceledEvent?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            InteractCanceledEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            JumpCanceledEvent?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());

        if (context.started)
        {
            MoveStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            MoveCanceledEvent?.Invoke();
        }
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
