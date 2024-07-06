using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState _stateData;

    protected bool _isPlayerInMinAgroRange;
    protected bool _isPlayerInMaxAgroRange;
    protected bool _performCloseRangeAction;
    protected bool _isLedgeDetected;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isPlayerInMinAgroRange = _entity.CheckPlayerInMinAgroRange();
        _isPlayerInMaxAgroRange = _entity.CheckPlayerInMaxAgroRange();
        _performCloseRangeAction = _entity.CheckPlayerInCloseRangeAction();
        _isLedgeDetected = _entity.CheckLedge();
    }

    public override void Enter()
    {
        base.Enter();

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
}
