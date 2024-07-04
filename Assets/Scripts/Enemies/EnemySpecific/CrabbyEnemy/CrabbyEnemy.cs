using UnityEngine;

public class CrabbyEnemy : Entity
{
    public Crabby_IdleState IdleState { get; private set; }

    public Crabby_MoveState MoveState { get; private set; }

    public Crabby_PlayerDetectedState PlayerDetectedState { get; private set; }

    public Crabby_LookForPlayerState LookForPlayerState { get; private set; }

    public Crabby_MeleeAttackState MeleeAttackState { get; private set; }

    public Crabby_StunState StunState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_StunState stunStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        IdleState = new Crabby_IdleState(this, stateMachine, "idle", idleStateData, this);
        MoveState = new Crabby_MoveState(this, stateMachine, "move", moveStateData, this);
        PlayerDetectedState = new Crabby_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        LookForPlayerState = new Crabby_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        MeleeAttackState = new Crabby_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        StunState = new Crabby_StunState(this, stateMachine, "stun", stunStateData, this);

        stateMachine.Initialize(MoveState);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (_isStunned && stateMachine.CurrentState != StunState)
        {
            stateMachine.ChangeState(StunState);
        }
    }
}
