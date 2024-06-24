using Osaro.player.constant;
using Osaro.Utilities;
using Osaro.Utilities.Constants;
using UnityEngine;


namespace Osaro.player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerStats playerStats; 
        private Rigidbody2D _playerRigidBody2D;
        private IsOnCollision _playerIsOnCollision;

        private void OnEnable()
        {
            EventManager.OnJump += ApplyJump;
            EventManager.OnMove += ApplyMovement;
            EventManager.OnIdle += StopMovement;
            EventManager.OnFall += HandleFall;
        }

        private void OnDisable()
        {
            EventManager.OnJump -= ApplyJump;
            EventManager.OnMove -= ApplyMovement;
            EventManager.OnIdle -= StopMovement;
            EventManager.OnFall -= HandleFall;
        }
        private void Awake()
        {
            _playerRigidBody2D = GetComponent<Rigidbody2D>();
            _playerIsOnCollision = GetComponent<IsOnCollision>();
        }

        private void Update()
        {
            PlayerController.Instance.IsFalling = IsFalling();
            PlayerController.Instance.IsGrounded = IsGrounded();
        }

        public void ApplyMovement()
        {
            float horizontalInput = Input.GetAxisRaw(PlayerConstantValues.HORIZONTAL);
            Vector2 newPosition = new Vector2(horizontalInput * playerStats.hSpeed , _playerRigidBody2D.velocity.y);
            Move(newPosition);
            

            ChangeDirection(horizontalInput);
        }

        public void StopMovement()
        {
            _playerRigidBody2D.velocity = Vector2.zero;
        }

        public void Move(Vector2 newPosition)
        {
            _playerRigidBody2D.velocity = new Vector2(newPosition.x, newPosition.y);
        }


        public void ApplyJump()
        {
            if (IsGrounded())
            {
                Jump();
            }

        }

        public void Jump()
        {
            _playerRigidBody2D.velocity = new Vector2(_playerRigidBody2D.velocity.x, playerStats.jumpForce);
        }

        public void HandleFall()
        {
            // logic here
        }
        public bool IsFalling()
        {
            return _playerRigidBody2D.velocity.y <= 0;
        }

        private void ChangeDirection(float horizontalInput)
        {
            if (horizontalInput != 0)
            {
                float direction = Mathf.Sign(horizontalInput);
                transform.localScale = new Vector3(direction, 1, 1);
            }
        }


        // Checks if the player is grounded
        private bool IsGrounded()
        {
            return _playerIsOnCollision.With(GameConstants.GROUND);
        }
    }
}

