using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolString) : base(player, stateMachine, playerData, animationBoolString)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        _player.SoundManager.PlayWalkingSound();
           
       

    }

    public override void Exit()
    {
        base.Exit();
        _player.SoundManager.StopWalkingSound();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       
        _player.CheckIfShouldFlip(_xInput);

        _player.SetVelocityX(_playerData.movementVelocity * _xInput);

        if (_xInput == 0)
        {
            _stateMachine.ChangeState(_player.IdleState);
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
