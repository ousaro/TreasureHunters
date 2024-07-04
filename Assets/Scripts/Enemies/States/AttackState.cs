using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{

    protected Transform _attackPosition;

    protected bool _isAnimationFinished;
    protected bool _isPlayerInMinAgroRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition) : base(entity, stateMachine, animationBoolName)
    {
        _attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        _entity.AnimationToStateMachine.attacState = this;
        _isAnimationFinished = false;

        _entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void TriggerFinishAttack()
    {
        _isAnimationFinished = true;
    }
}
