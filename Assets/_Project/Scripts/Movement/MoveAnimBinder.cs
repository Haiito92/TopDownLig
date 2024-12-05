using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimBinder : MonoBehaviour
{
    //BindedComponent
    [Header("BindedComponent")]
    [SerializeField] private MoveComponent _moveComponent;
    
    //Animation
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _idleAnimName;
    [SerializeField] private string _walkAnimName;
    [SerializeField] private string _moveXParamName;
    [SerializeField] private string _moveYParamName;

    void PlayWalkAnim()
    {
        _animator.Play(_walkAnimName);
    }

    void UpdateWalkAnimParameters(Vector2 inDirection)
    {
        _animator.SetFloat(_moveXParamName, inDirection.x);
        _animator.SetFloat(_moveYParamName, inDirection.y);
    }

    void PlayIdleAnim()
    {
        _animator.Play(_idleAnimName);
    }
    
    private void OnEnable()
    {
        _moveComponent.StartWalkEvent += PlayWalkAnim;
        _moveComponent.UpdateWalkEvent += UpdateWalkAnimParameters;
        _moveComponent.StopWalkEvent += PlayIdleAnim;
    }

    private void OnDisable()
    {
        _moveComponent.StartWalkEvent -= PlayWalkAnim;
        _moveComponent.UpdateWalkEvent -= UpdateWalkAnimParameters;
        _moveComponent.StopWalkEvent -= PlayIdleAnim;
    }
}
