using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animationBoolString) : base(player, playerStateMachine, playerData, animationBoolString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocityY(_playerData.jumpVelocity);
        _isAbilityDone = true;

        _player.InAirState.SetIsJumping();

        SoundManager.Instance.PlaySoundFXClip(_playerData.jumpSFX, _player.transform, 1f);
    }
}
