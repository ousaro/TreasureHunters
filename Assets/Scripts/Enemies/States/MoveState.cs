using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{

    protected D_MoveState _stateData;

    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_MoveState stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isDetectingWall = _entity.CheckWall();
        _isDetectingLedge = _entity.CheckLedge();
        _isPlayerInMinAgroRange = _entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        _entity.SetVelocity(_stateData.movementSpeed);
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
}
