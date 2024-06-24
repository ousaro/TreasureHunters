using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osaro.Utilities;
using System;
using Osaro.player.constant;
using Osaro.Utilities.Constants;

namespace Osaro.player
{
    
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [SerializeField] private float attackDuration;
        [SerializeField] private float timeBetweenAttacks;
        public enum PlayerState { Idle, Running, Attacking, Jumping, Falling };
        private PlayerState _playerState;

        private bool _isAlreadyAttacked;

        public bool IsFalling { get; set; }
        public bool IsGrounded { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _playerState = PlayerState.Idle;
            _isAlreadyAttacked = false;
            EventManager.OnAttackEnd += OnAttackEnd;
        }

        private void OnDestroy()
        {
            EventManager.OnAttackEnd -= OnAttackEnd;
        }

        private void Update()
        {
            PlayerStateLogic();
        }

        private void PlayerStateLogic()
        {
            switch (_playerState)
            {
                case PlayerState.Idle:
                    IdleStateHandler();
                    break;
                case PlayerState.Running:
                    RunningStateHandler();
                    break;
                case PlayerState.Jumping:
                    JumpingStateHandler();
                    break;
                case PlayerState.Falling:
                    FallingStateHandler();
                    break;
                case PlayerState.Attacking:
                    AttackingStateHandler();
                    break;
            }
        }

        private void IdleStateHandler()
        {
            if (Input.GetAxisRaw(PlayerConstantValues.HORIZONTAL) != 0)
            {
                _playerState = PlayerState.Running;
                EventManager.TriggerMove();
            }
            else if (Input.GetButtonDown(PlayerConstantValues.FIRE))
            {

                if (_isAlreadyAttacked)
                {
                    return;
                }
                _playerState = PlayerState.Attacking;
                _isAlreadyAttacked = true;

                EventManager.TriggerAttack();
            }
            else if (Input.GetButtonDown(PlayerConstantValues.JUMP))
            {
                _playerState = PlayerState.Jumping;
                EventManager.TriggerJump();
            }
            else
            {
                EventManager.TriggerIdle();
            }
        }

        private void RunningStateHandler()
        {
            if (Input.GetAxisRaw(PlayerConstantValues.HORIZONTAL) == 0)
            {
                _playerState = PlayerState.Idle;
                EventManager.TriggerIdle();
            }
            else if (Input.GetButtonDown(PlayerConstantValues.JUMP))
            {
                _playerState = PlayerState.Jumping;
                EventManager.TriggerJump();
            }
            else if (Input.GetButtonDown(PlayerConstantValues.FIRE))
            {
                if(_isAlreadyAttacked)
                {
                    return;
                }
                _playerState = PlayerState.Attacking;
                _isAlreadyAttacked = true;
                EventManager.TriggerAttack();
            }
        }

        private void JumpingStateHandler()
        {
            if (IsFalling)
            {
                _playerState = PlayerState.Falling;
                EventManager.TriggerFall();
            }
        }

        private void FallingStateHandler()
        {
            if (IsGrounded)
            {
                _playerState = PlayerState.Idle;
                EventManager.TriggerIdle();
            }
        }

        private void AttackingStateHandler()
        {
            // Prevent further state changes while attacking
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            // Wait for the attack duration
            yield return new WaitForSeconds(attackDuration);

            // End attack state and reset flag
            _playerState = PlayerState.Idle;

            yield return new WaitForSeconds(timeBetweenAttacks) ;
            EventManager.TriggerAttackEnd();
        }

        private void OnAttackEnd()
        {
            _isAlreadyAttacked = false;
        }


    }
}
