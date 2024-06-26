using Osaro.player.constant;
using Osaro.Utilities;
using UnityEngine;

namespace Osaro.player
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private AnimationController playerAnimationController;
        [SerializeField] private EventManager playerEventManager;

        private string _newAnimation = PlayerAnimationString.IDLE;

        private void OnEnable()
        {
            playerEventManager.StartListening(PlayerEventsString.ON_IDLE, OnIdle_HandleAnimation);
            playerEventManager.StartListening(PlayerEventsString.ON_MOVE, OnMove_HandleAnimation);
            playerEventManager.StartListening(PlayerEventsString.ON_JUMP, OnJump_HandleAnimation);
            playerEventManager.StartListening(PlayerEventsString.ON_FALL, OnFall_HandleAnimation);
            playerEventManager.StartListening(PlayerEventsString.ON_ATTACK, OnAttack_HandleAnimation);

        }

        private void OnDisable()
        {

            playerEventManager.StopListening(PlayerEventsString.ON_IDLE, OnIdle_HandleAnimation);
            playerEventManager.StopListening(PlayerEventsString.ON_MOVE, OnMove_HandleAnimation);
            playerEventManager.StopListening(PlayerEventsString.ON_JUMP, OnJump_HandleAnimation);
            playerEventManager.StopListening(PlayerEventsString.ON_FALL, OnFall_HandleAnimation);
            playerEventManager.StopListening(PlayerEventsString.ON_ATTACK, OnAttack_HandleAnimation);
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
