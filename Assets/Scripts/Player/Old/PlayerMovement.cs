using Osaro.player.constant;
using Osaro.Utilities;
using Osaro.Utilities.Constants;
using UnityEngine;


namespace Osaro.player
{
    public class PlayerMovement : MonoBehaviour
    {


        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private EventManager playerEventManager;
        private Rigidbody2D _playerRigidBody2D;
        private IsOnCollision _playerIsOnCollision;

        private void OnEnable()
        {
           playerEventManager.StartListening(PlayerEventsString.ON_IDLE, StopMovement);
           playerEventManager.StartListening(PlayerEventsString.ON_MOVE, ApplyMovement);
           playerEventManager.StartListening(PlayerEventsString.ON_JUMP, ApplyJump);
           playerEventManager.StartListening(PlayerEventsString.ON_FALL, HandleFall);
           playerEventManager.StartListening(PlayerEventsString.ON_ATTACK, StopMovement);

        }

        private void OnDisable()
        {
            playerEventManager.StopListening(PlayerEventsString.ON_IDLE, StopMovement);
            playerEventManager.StopListening(PlayerEventsString.ON_MOVE, ApplyMovement);
            playerEventManager.StopListening(PlayerEventsString.ON_JUMP, ApplyJump);
            playerEventManager.StopListening(PlayerEventsString.ON_FALL, HandleFall);
            playerEventManager.StopListening(PlayerEventsString.ON_ATTACK, StopMovement);

        }
        private void Awake()
        {
            _playerRigidBody2D = GetComponent<Rigidbody2D>();
            _playerIsOnCollision = GetComponent<IsOnCollision>();
        }

        private void Update()
        {
            PlayerStateHandler.Instance.IsFalling = IsFalling();
            PlayerStateHandler.Instance.IsGrounded = IsGrounded();

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
           
            _playerRigidBody2D.velocity = new Vector2(0, _playerRigidBody2D.velocity.y);
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
            return _playerRigidBody2D.velocity.y <= -1;
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

