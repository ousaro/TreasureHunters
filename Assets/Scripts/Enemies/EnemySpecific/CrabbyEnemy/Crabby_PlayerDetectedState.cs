using System.Collections;
using System.Collections.Generic;

public class Crabby_PlayerDetectedState : PlayerDetectedState
{
    private CrabbyEnemy _crabbyEnemy;
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

        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_crabbyEnemy.MeleeAttackState);
        }
        else if (!_isPlayerInMaxAgroRange)
        {
            _stateMachine.ChangeState(_crabbyEnemy.LookForPlayerState);
        }
    }   

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
