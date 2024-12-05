using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Player Move
    [SerializeField] private float _playerSpeed = 12f;
    private Vector2 _direction;
    [SerializeField] private InputActionReference _walkAction;
    
    //Player Animation
    [SerializeField] private Animator _animator;
    [SerializeField] private string _idleAnimName;
    [SerializeField] private string _walkAnimName;
    [SerializeField] private string _moveXParamName;
    [SerializeField] private string _moveYParamName;
    
    private void Update()
    {
        Walk();
    }

    private void Walk()
    {
        transform.position = (Vector2)transform.position + _direction * _playerSpeed * Time.deltaTime;
    }

    private void OnWalkAction(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _animator.Play(_walkAnimName);
        }
        
        if (ctx.performed)
        {
            _direction = ctx.ReadValue<Vector2>();
            _animator.SetFloat(_moveXParamName, _direction.x);
            _animator.SetFloat(_moveYParamName, _direction.y);
        }

        if (ctx.canceled)
        {
            _direction = Vector2.zero;
            _animator.Play(_idleAnimName);
            
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