public class DeathState : State
{

    protected D_DeathState _stateData;
    public DeathState(Entity entity, FiniteStateMachine stateMachine, string animationBoolName, D_DeathState stateData) : base(entity, stateMachine, animationBoolName)
    {
        _stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        _entity.AnimationToStateMachine.deathState = this;
        base.Enter();
 
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

    public virtual void TriggerAnimation()
    {

    }

    public virtual void TriggerFinishAnimation()
    {
        
    }
}
