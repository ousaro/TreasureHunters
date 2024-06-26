using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Osaro.Utilities;
using System;
using Osaro.player.constant;
using Osaro.Utilities.Constants;

namespace Osaro.player
{
    
    public class PlayerStateHandler : MonoBehaviour
    {
        public static PlayerStateHandler Instance;

        [SerializeField] private EventManager playerEventManager;

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
                playerEventManager.TriggerEvent(PlayerEventsString.ON_MOVE);
               
            }
            else if (Input.GetButtonDown(PlayerConstantValues.FIRE))
            {

                if (_isAlreadyAttacked)
                {
                    return;
                }
                _playerState = PlayerState.Attacking;
                _isAlreadyAttacked = true;

                playerEventManager.TriggerEvent(PlayerEventsString.ON_ATTACK);
            }
            else if (Input.GetButtonDown(PlayerConstantValues.JUMP))
            {
                _playerState = PlayerState.Jumping;
                playerEventManager.TriggerEvent(PlayerEventsString.ON_JUMP);
            }
            else
            {
                playerEventManager.TriggerEvent(PlayerEventsString.ON_IDLE);
            }
        }

        private void RunningStateHandler()
        {
            if (Input.GetAxisRaw(PlayerConstantValues.HORIZONTAL) == 0)
            {
                _playerState = PlayerState.Idle;
                playerEventManager.TriggerEvent(PlayerEventsString.ON_IDLE);
            }
            else if (Input.GetButtonDown(PlayerConstantValues.JUMP))
            {
                _playerState = PlayerState.Jumping;
                playerEventManager.TriggerEvent(PlayerEventsString.ON_JUMP);
            }
            else if (Input.GetButtonDown(PlayerConstantValues.FIRE))
            {
                if(_isAlreadyAttacked)
                {
                    return;
                }
                _playerState = PlayerState.Attacking;
                _isAlreadyAttacked = true;
                playerEventManager.TriggerEvent(PlayerEventsString.ON_ATTACK);
            }       
        }

        private void JumpingStateHandler()
        {
            if (IsFalling)
            {
                _playerState = PlayerState.Falling;
                playerEventManager.TriggerEvent(PlayerEventsString.ON_FALL);
            }
        }

        private void FallingStateHandler()
        {
            if (IsGrounded)
            {
                _playerState = PlayerState.Idle;
                playerEventManager.TriggerEvent(PlayerEventsString.ON_IDLE);
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
            _isAlreadyAttacked = false;
        }


        public Vector3 GetPosition()
        {
            return transform.position;
        }

    }
}
