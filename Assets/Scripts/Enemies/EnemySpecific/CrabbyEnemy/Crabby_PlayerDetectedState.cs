using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabby_PlayerDetectedState : PlayerDetectedState
{
    private CrabbyEnemy _crabbyEnemy;

    private float _lastPerformedCloseRangeTime;
    public Crabby_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_PlayerDetectedState stateData, CrabbyEnemy crabbyEnemy) : base(entity, stateMachine, animationBoolName, stateData)
    {
        _crabbyEnemy = crabbyEnemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_performCloseRangeAction && Time.time >= _lastPerformedCloseRangeTime + _stateData.timeBetweenAttacks)
        {
            _stateMachine.ChangeState(_crabbyEnemy.MeleeAttackState); 
            _lastPerformedCloseRangeTime = Time.time;
        }
        else if (!_isPlayerInMaxAgroRange)
        {
            _stateMachine.ChangeState(_crabbyEnemy.LookForPlayerState);
        }
        else if (!_isLedgeDetected)
        {
            _entity.Flip();
            _stateMachine.ChangeState(_crabbyEnemy.MoveState);
        }
    }   

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
