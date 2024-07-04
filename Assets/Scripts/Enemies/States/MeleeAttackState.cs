using TMPro;
using UnityEngine;


public class MeleeAttackState : AttackState
{

    protected D_MeleeAttackState _stateData;

    protected AttackDetails _attackDetails;
    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animationBoolName, attackPosition)
    {
        _stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        _attackDetails.damageAmout = _stateData.attackDamage;
        _attackDetails.position = _entity.AliveGO.transform.position;
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

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_attackPosition.position, _stateData.attackRadius, _stateData.whatIsPlayer);

        foreach(Collider2D collider in detectedObjects)
        {
            //collider.transform.SendMessage("Damage", _attackDetails);
            Debug.Log("player attacked");

        }
    }

    public override void TriggerFinishAttack()
    {
        base.TriggerFinishAttack();
    }
}
