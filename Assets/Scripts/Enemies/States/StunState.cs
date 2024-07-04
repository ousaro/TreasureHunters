using UnityEngine;

public class StunState : State
{
    protected D_StunState _stateData;

    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _performCloseRangeAction;
    protected bool _isPlayerInMinAgroRange;

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_StunState stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = _entity.CheckGround();
        _performCloseRangeAction = _entity.CheckPlayerInCloseRangeAction();
        _isPlayerInMinAgroRange = _entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        _isStunTimeOver = false;
        _isMovementStopped = false;
        _entity.SetVelocity(_stateData.stunKnockBackSpeed, _stateData.stunKnockBackAngle, _entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        _entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= _startTime + _stateData.stunTime)
        {
            _isStunTimeOver = true;
        }

        if(_isGrounded && Time.time >= _startTime + _stateData.stunKnockBackTime && !_isMovementStopped)
        {
            _entity.SetVelocity(0f);
            _isMovementStopped = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}