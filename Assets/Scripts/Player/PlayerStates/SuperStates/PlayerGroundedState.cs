using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{

    protected int _xInput;

    private bool _jumpInput;
    private bool _attackInput;
    private bool _isGrounded;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolString) : base(player, stateMachine, playerData, animationBoolString)
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

        _jumpInput = _player.InputHandler.JumpInput;

        _attackInput = _player.InputHandler.AttackInput;


        if (_attackInput)
        {
            _player.InputHandler.UseAttackInput();
            _stateMachine.ChangeState(_player.AttackState);
        }
        else if (_jumpInput)
        {
            _player.InputHandler.UseJumpInput();
            _stateMachine.ChangeState(_player.JumpState);

        }else if (!_isGrounded)
        {
            _stateMachine.ChangeState(_player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
