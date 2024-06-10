using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using osaro.utilities;
using System;
using osaro.player.constant;
using osaro.utilities.Constants;

namespace osaro.player
{
    public class PlayerController : MonoBehaviour
    {

        // scriptableObjects 
        [SerializeField] private PlayerStats playerStats;


        [SerializeField] private AnimationController playerAnimationController;
        [SerializeField] private HorizontalMove playerHorizontalMove;

        private IsOnCollision _playerIsOnCollisionWith;



        private float _direction = 1;

        private bool _isGrounded;

        private void Awake()
        {
            _playerIsOnCollisionWith = GetComponent<IsOnCollision>();
        }

        // Update is called once per frame
        void Update()
        {
            ApplyMovement();
            ApplyJump();
        }

        private void ApplyJump() {

            _isGrounded = _playerIsOnCollisionWith.With(GameConstants.GROUND);

            if(Input.GetButtonDown(PlayerConstantValues.JUMP) && _isGrounded)
            {
                playerHorizontalMove.Jump(playerStats.jumpForce);
            }
        }

        private void ApplyMovement()
        {
            float horizontalInput = Input.GetAxisRaw(PlayerConstantValues.HORIZONTAL);

            if (horizontalInput != 0) _direction = Mathf.Sign(horizontalInput);

            HandleMovement(horizontalInput);
            HandleAnimation(horizontalInput);
            ChangeDirection();
        }

        private void HandleMovement(float horizontalInput)
        {
            playerHorizontalMove.Move(horizontalInput, playerStats.hSeed);
        }

        private void HandleAnimation(float horizontalInput)
        {
            string newAnimation = horizontalInput == 0 ?
                PlayerAnimationStirng.IDLE : PlayerAnimationStirng.RUN;

            playerAnimationController.ChangeCurrentAnimation(newAnimation);

        }
        private void ChangeDirection()
        {
            transform.localScale = new Vector3(_direction, 1, 1);
        }
    }

}
