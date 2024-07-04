using UnityEngine;

public class Crabby_MeleeAttackState : MeleeAttackState
{
    private CrabbyEnemy _crabbyEnemy;
    public Crabby_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData, CrabbyEnemy crabbyEnemy) : base(entity, stateMachine, animationBoolName, attackPosition, stateData)
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

        if (_isAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
            {
                _stateMachine.ChangeState(_crabbyEnemy.PlayerDetectedState);
            }
            else
            {
                _stateMachine.ChangeState(_crabbyEnemy.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }

    public override void TriggerFinishAttack()
    {
        base.TriggerFinishAttack();
    }
}
