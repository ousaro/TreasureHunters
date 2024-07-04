public class Crabby_StunState : StunState
{
    private CrabbyEnemy _crabbyEnemy;
    public Crabby_StunState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_StunState stateData, CrabbyEnemy crabbyEnemy) : base(entity, stateMachine, animationBoolName, stateData)
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

        if (_isStunTimeOver)
        {
            if (_performCloseRangeAction)
            {
                _stateMachine.ChangeState(_crabbyEnemy.MeleeAttackState);
            }
            else if (_isPlayerInMinAgroRange)
            {
                _stateMachine.ChangeState(_crabbyEnemy.PlayerDetectedState);
            }
            else
            {
                _crabbyEnemy.LookForPlayerState.SetTurnImmediately(true);
                _stateMachine.ChangeState(_crabbyEnemy.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
