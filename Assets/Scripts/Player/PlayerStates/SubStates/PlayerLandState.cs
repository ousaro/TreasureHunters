using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolString) : base(player, stateMachine, playerData, animationBoolString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySoundFXClip(_playerData.landingSFX, _player.transform, 1f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_xInput != 0)
        {
            _stateMachine.ChangeState(_player.MoveState);

        }
        else if (_isAnimationFinished)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
    }
}
