using Unity.VisualScripting;
using UnityEngine;

public class Crabby_DeathState : DeathState
{
    private CrabbyEnemy _crabbyEnemy;
    public Crabby_DeathState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeathState stateData, CrabbyEnemy crabbyEnemy) : base(entity, stateMachine, animationBoolName, stateData)
    {
        this._crabbyEnemy = crabbyEnemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        _entity.OnDeath?.Invoke();
        _entity.AliveGO.GetComponent<Collider2D>().enabled = false;
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

    public override void TriggerAnimation()
    {
        base.TriggerAnimation();
    }

    public override void TriggerFinishAnimation()
    {
        base.TriggerFinishAnimation();
        _entity.DestroyGameObject();
    }
}