using Osaro.player.constant;
using Osaro.Utilities;
using UnityEngine;

namespace Osaro.player
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private AnimationController playerAnimationController;

        private string _newAnimation = PlayerAnimationString.IDLE;

        private void OnEnable()
        {
            EventManager.OnAttack += OnAttack_HandleAnimation;
            EventManager.OnJump += OnJump_HandleAnimation;
            EventManager.OnMove += OnMove_HandleAnimation;
            EventManager.OnIdle += OnIdle_HandleAnimation;
            EventManager.OnFall += OnFall_HandleAnimation;
        }

        private void OnDisable()
        {
            EventManager.OnAttack -= OnAttack_HandleAnimation;
            EventManager.OnJump -= OnJump_HandleAnimation;
            EventManager.OnMove -= OnMove_HandleAnimation;
            EventManager.OnIdle -= OnIdle_HandleAnimation;
            EventManager.OnFall -= OnFall_HandleAnimation;
        }

        private void Update()
        {
            playerAnimationController.ChangeCurrentAnimation(_newAnimation);
        }
        private void OnFall_HandleAnimation()
        {
            _newAnimation = PlayerAnimationString.FALL;
        }
        private void OnAttack_HandleAnimation()
        {
            _newAnimation = PlayerAnimationString.ATTACK;
        }

        private void OnJump_HandleAnimation()
        {
            _newAnimation = PlayerAnimationString.JUMP;
        }

        private void OnMove_HandleAnimation()
        {
            _newAnimation = PlayerAnimationString.RUN;
        }

        private void OnIdle_HandleAnimation()
        {
            _newAnimation = PlayerAnimationString.IDLE;
        }
       
        public float GetAnimationDuration()
        {
            return playerAnimationController.AnimationDuration;
        }
    }

}
