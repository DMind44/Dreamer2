using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject
{
    [SerializeField] private InputActionAsset _inputActionAsset;

    private InputAction
        _jumpAction,
        _lookAction,
        _moveAction;
    
    public event UnityAction<Vector2> moveEvent;
    
    public event UnityAction<Vector2> lookEvent;

    public event UnityAction jumpEvent;
    
    private void MapAction(InputAction inputAction, Action<InputAction.CallbackContext> eventFunction)
    {
        inputAction.started += eventFunction;
        inputAction.performed += eventFunction;
        inputAction.canceled += eventFunction;
        inputAction.Enable();
    } 
    private void UnmapAction(InputAction inputAction, Action<InputAction.CallbackContext> eventFunction)
    {
        inputAction.started -= eventFunction;
        inputAction.performed -= eventFunction;
        inputAction.canceled -= eventFunction;
        inputAction.Disable();
    }

    private void OnEnable()
    {
        _moveAction = _inputActionAsset.FindAction("Move");
        MapAction(_moveAction, OnMove);
        
        _lookAction = _inputActionAsset.FindAction("Look");
        MapAction(_lookAction, OnLook);
        
        _jumpAction = _inputActionAsset.FindAction("Jump");
        MapAction(_jumpAction, OnJump);
    }

    private void OnDisable()
    {
        UnmapAction(_moveAction, OnMove);
        UnmapAction(_lookAction, OnLook);
        UnmapAction(_jumpAction, OnJump);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpEvent?.Invoke();
    }
}

    