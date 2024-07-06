using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected PlayerData _playerData;

    protected bool _isAnimationFinished;

    private float _startTime;

    protected string _animationBoolString;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animationBoolString)
    {
        _player = player;
        _stateMachine = playerStateMachine;
        _playerData = playerData;
        _animationBoolString = animationBoolString;
    }

    public virtual void Enter()
    {
        DoChecks();
        _player.Animator.SetBool(_animationBoolString, true);
        _startTime = Time.time;
        _isAnimationFinished = false;
        //Debug.Log(_animationBoolString);
    }

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animationBoolString, false);

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => _isAnimationFinished = true;
}
