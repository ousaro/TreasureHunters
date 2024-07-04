using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabby_LookForPlayerState : LookForPlayerState
{
    private CrabbyEnemy _crabbyEnemy;
    public Crabby_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_LookForPlayerState stateData, CrabbyEnemy crabbyEnemy) : base(entity, stateMachine, animationBoolName, stateData)
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


        if (_isPlayerInMinAgroRange)
        {
            _stateMachine.ChangeState(_crabbyEnemy.PlayerDetectedState);
        }
        else if (_isAllTurnsTimeDone) {

            _stateMachine.ChangeState(_crabbyEnemy.MoveState);

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
