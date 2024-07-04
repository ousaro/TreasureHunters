using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine _stateMachine;

    protected Entity _entity;


    protected float _startTime;

    protected string _animationBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animationBoolName)
    {
        _stateMachine = stateMachine;
        _entity = entity;  
        _animationBoolName = animationBoolName;
    }


    public virtual void Enter()
    {
        _startTime = Time.time;
        _entity.Animator.SetBool(_animationBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        _entity.Animator.SetBool(_animationBoolName, false);
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
}
