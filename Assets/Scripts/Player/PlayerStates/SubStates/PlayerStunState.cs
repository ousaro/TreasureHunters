using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStunState : PlayerState
{
    public PlayerStunState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animationBoolString) : base(player, playerStateMachine, playerData, animationBoolString)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        _stateMachine.ChangeState(_player.IdleState);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocity(_playerData.stunKnockBackSpeed, _playerData.stunKnockBackAngle, _player.LastDamageDirection);
        SoundManager.Instance.PlaySoundFXClip(_playerData.stunClip, _player.transform, 1f);
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
}
