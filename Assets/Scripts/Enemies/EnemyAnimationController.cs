using Osaro.Enemy.Constrants;
using Osaro.Utilities;
using UnityEngine;

namespace Osaro.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {

        [SerializeField] private AnimationController enemyAnimationController;
        [SerializeField] private EventManager enemyEventManager;



        private string _newAnimation;

        private void OnEnable()
        {
            enemyEventManager.StartListening(EnemyEventString.ON_ATTACK, AttackAnimationHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_MOVE, RunAnimationHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_IDLE, IdleAnimationHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_DEAD, DeathAnimationHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_HEALTH_CHANGE, TakeHitAnimationHandler);

        }

        private void OnDisable()
        {
            enemyEventManager.StopListening(EnemyEventString.ON_ATTACK, AttackAnimationHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_MOVE, RunAnimationHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_IDLE, IdleAnimationHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_DEAD, DeathAnimationHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_HEALTH_CHANGE, TakeHitAnimationHandler);

        }

        private void Update()
        {
            HandleAnimation();
        }

        private void TakeHitAnimationHandler()
        {
            _newAnimation = EnemyAnimationString.HIT;  
        }

        private void DeathAnimationHandler()
        {
            _newAnimation = EnemyAnimationString.DEAD;
        }

        private void IdleAnimationHandler()
        {
            _newAnimation = EnemyAnimationString.IDLE;
        }

        private void RunAnimationHandler()
        {
            _newAnimation = EnemyAnimationString.RUN;
        }

        private void AttackAnimationHandler()
        {
            _newAnimation = EnemyAnimationString.ATTACK;
        }

        private void HandleAnimation()
        {
            enemyAnimationController.ChangeCurrentAnimation(_newAnimation);
        }

       

    }


}
