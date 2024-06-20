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
        // ScriptableObjects
        [SerializeField] private PlayerStats playerStats;


        [SerializeField] private AnimationController playerAnimationController;
        [SerializeField] private CharacterMovement playerMovement;

        
        private IsOnCollision _playerIsOnCollision;

        private float _direction = 1; //facing right;
        private bool _isGrounded;

        private void Awake()
        {
            _playerIsOnCollision = GetComponent<IsOnCollision>();
        }

        // Update is called once per frame
        void Update()
        {
            ApplyMovement();
            ApplyJump();
        }


        // Handles horizontal movement and animation
        private void ApplyMovement()
        {
            float horizontalInput = Input.GetAxisRaw(PlayerConstantValues.HORIZONTAL);

            if (horizontalInput != 0)
            {
                _direction = Mathf.Sign(horizontalInput);
            }

            Vector2 newPostion = new Vector2(horizontalInput * playerStats.hSpeed, 
                playerMovement.Rigidbody2D.velocity.y);

            HandleMovement(newPostion);
            HandleAnimation(horizontalInput);
            ChangeDirection();
        }



        // Moves the player
        private void HandleMovement(Vector2 newPosition)
        {
            playerMovement.MoveToward(newPosition);
        }

        // Changes the player's facing direction
        private void ChangeDirection()
        {
            transform.localScale = new Vector3(_direction, 1, 1);
        }

        // Handles jumping logic
        private void ApplyJump()
        {
            _isGrounded = IsGrounded();

            if (!CanJump()) { return; }

            playerMovement.Jump(playerStats.jumpForce);


        }
        // Determines if the player can jump
        private bool CanJump()
        {
            return Input.GetButtonDown(PlayerConstantValues.JUMP) && _isGrounded;
        }

        // Checks if the player is grounded
        private bool IsGrounded()
        {
            return _playerIsOnCollision.With(GameConstants.GROUND);
        }


        // Changes the player animation
        private void HandleAnimation(float horizontalInput)
        {
            string newAnimation = PlayerAnimationString.IDLE;
            // Determine animation based on movement and jump status
            if (_isGrounded)
            {
                newAnimation = horizontalInput == 0 ? PlayerAnimationString.IDLE : PlayerAnimationString.RUN;

            }
            else if(!_isGrounded && playerMovement.Rigidbody2D.velocity.y<=0)
            {
                
                newAnimation = PlayerAnimationString.FALL;
            }
            else
            {
                newAnimation = PlayerAnimationString.JUMP;
            }

            playerAnimationController.ChangeCurrentAnimation(newAnimation);
        }


    }
}
