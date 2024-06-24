using Osaro.Enemy.Constrants;
using Osaro.Utilities;
using UnityEngine;

namespace Osaro.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {

        [SerializeField] private AnimationController enemyAnimationController;
        [SerializeField] private GameObject enemyGFX;

        private Rigidbody2D _enemyRigidbody2d;
        private float _direction = 1; //left

        private void Awake()
        {
            _enemyRigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleAnimation();
            ChangeDirection();
        }
        private void HandleAnimation()
        {
            string newAnimation = EnemyAnimationString.IDLE;

            if (_enemyRigidbody2d.velocity.x != 0)
            {
                newAnimation = EnemyAnimationString.RUN;
            }
            else
            {
                newAnimation = EnemyAnimationString.IDLE;
            }



            enemyAnimationController.ChangeCurrentAnimation(newAnimation);
        }


        private void ChangeDirection()
        {
            _direction = -Mathf.Sign(_enemyRigidbody2d.velocity.x);
            enemyGFX.transform.localScale = new Vector3(_direction, 1, 1);
        }

    }


}
