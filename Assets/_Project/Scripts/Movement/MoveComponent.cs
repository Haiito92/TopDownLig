using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MoveComponent : MonoBehaviour
{
    //Player Move
    [SerializeField] private Transform _movedTransform;
    [SerializeField] private float _speed = 12f;
    private Vector2 _direction;
    [SerializeField] private InputActionReference _walkAction;
    
    //Actions
    public UnityAction StartWalkEvent;
    public UnityAction<Vector2> UpdateWalkEvent;
    public UnityAction StopWalkEvent;
    
    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        if (_movedTransform == null) return;
        _movedTransform.position = (Vector2)_movedTransform.position + _direction * _speed * Time.deltaTime;
    }

    private void OnWalkAction(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            StartWalkEvent?.Invoke();
        }
        
        if (ctx.performed)
        {
            _direction = ctx.ReadValue<Vector2>();
            UpdateWalkEvent?.Invoke(_direction);
        }

        if (ctx.canceled)
        {
            _direction = Vector2.zero;
            StopWalkEvent?.Invoke();
        }
    }

    private void OnEnable()
    {
        _walkAction.action.started += OnWalkAction;
        _walkAction.action.performed += OnWalkAction;
        _walkAction.action.canceled += OnWalkAction;
    }

    private void OnDisable()
    {
        _walkAction.action.started -= OnWalkAction;
        _walkAction.action.performed -= OnWalkAction;
        _walkAction.action.canceled -= OnWalkAction;
    }
}
