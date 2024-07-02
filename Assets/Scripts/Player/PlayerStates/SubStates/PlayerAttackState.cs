using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animationBoolString) : base(player, playerStateMachine, playerData, animationBoolString)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        _isAbilityDone = true;
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityX(0);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.SetVelocityX(0);
    }



}
