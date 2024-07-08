using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected Transform _attackPosition;

    protected AttackDetails _attackDetails;

    protected bool _canAttack;


    public PlayerAttackState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animationBoolString, Transform attackPosition) : base(player, playerStateMachine, playerData, animationBoolString)
    {
        _attackPosition = attackPosition;
        _canAttack = true;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        _isAbilityDone = true;
        
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        _attackDetails.position = _player.transform.position;
        _attackDetails.damageAmout = _playerData.damageAmount;
        _attackDetails.stunDamageAmout = _playerData.stunDamageAmount;

        Collider2D[] detectedEnemies = Physics2D.OverlapCircleAll(_attackPosition.position, _playerData.attackRadius, _playerData.whatIsDamageable);

        foreach(Collider2D detectedEnemy in detectedEnemies)
        { 
            IDamagable damagable = detectedEnemy.GetComponent<IDamagable>();

            if (damagable != null)
            {
                damagable.Damage(_attackDetails);
            }
           
          
        }
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityX(0);
        SoundManager.Instance.PlaySoundFXClip(_playerData.attackSFX, _player.transform, 1f);

    }

    public override void Exit()
    {
        base.Exit();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.SetVelocityX(0);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
