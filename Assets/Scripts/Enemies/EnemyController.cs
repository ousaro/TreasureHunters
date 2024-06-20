using Osaro.Enemy.Constrants;
using Osaro.player;
using Osaro.player.constant;
using Osaro.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


namespace Osaro.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        // ScriptableObjects
        [SerializeField] private EnemyStats enemyStats;

        [SerializeField] private AnimationController enemyAnimationController;

        private Rigidbody2D _rigidbody2D;

        [SerializeField] AIPath aiPath;
        // Utilities
        private UtilsClass _osaroRandom = new UtilsClass();

        private float _direction = 1; //facing left;

        private Vector3 _initialPosition;
        Vector3 randomPos ;

        // initialize variables 
        private void Start()
        {
            _initialPosition = transform.position;
            randomPos = GetRandomPosition();
            _rigidbody2D = GetComponent<Rigidbody2D>();
           

        }

        // Update is called once per frame
        void Update()
        {
            ApplyMouvement();
        }


        private void ApplyMouvement()
        {
            HandleMovement();
            HandleAnimation();
            ChangeDirection();
        }

        // Moves the Enemy
        private void HandleMovement()
        {

            _direction = - Mathf.Sign(aiPath.desiredVelocity.x);
          //pathfinder


        }

        private Vector3 GetRandomPosition()
        {
            return _initialPosition  +new Vector3(1,0,1) * UnityEngine.Random.Range(10f, 70f);
        }



        // Changes the enemy's facing direction
        private void ChangeDirection()
        {
             _direction = - Mathf.Sign(aiPath.desiredVelocity.x);
            transform.localScale = new Vector3(_direction, 1, 1);
        }

        // Changes the enemy animation
        private void HandleAnimation()
        {
            string newAnimation = EnemyAnimationString.IDLE;

            if (_rigidbody2D.velocity.x != 0)
            {
                newAnimation = EnemyAnimationString.RUN;
            }
            else
            {
                newAnimation = EnemyAnimationString.IDLE;
            }
            
            

            enemyAnimationController.ChangeCurrentAnimation(newAnimation);
        }


    }
}
