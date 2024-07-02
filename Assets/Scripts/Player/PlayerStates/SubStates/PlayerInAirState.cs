using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int _xInput;
    private bool _isGrounded;
    private bool _jumpInputStop;
    private bool _isJumping;

   
    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animationBoolString) : base(player, playerStateMachine, playerData, animationBoolString)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
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

        _xInput = _player.InputHandler.NormalizedInputX;
        _jumpInputStop = _player.InputHandler.JumpInputStop;

       

        CheckJumpMultiplier();

        if (_isGrounded && _player.CurrentVelocity.y < 0.01f)
        {
            _stateMachine.ChangeState(_player.LandState);
        }
        else
        {
            _player.CheckIfShouldFlip(_xInput);
            _player.SetVelocityX(_playerData.movementVelocity * _xInput);

            _player.Animator.SetFloat("yVelociry", _player.CurrentVelocity.y);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                _player.SetVelocityY(_player.CurrentVelocity.y * _playerData.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetIsJumping() => _isJumping = true;
}
