using UnityEngine;
using Pathfinding;
using Osaro.Enemy.Constrants;
using Osaro.Utilities;
using Osaro.player;
using Osaro.Utilities.Constants;
using System.Collections;
using System;

namespace Osaro.Enemy
{

    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] private GameObject enemyGFX;
        [SerializeField] private EventManager enemyEventManager;

       
        private float _direction = 1; //left
        private Vector3 _initialPosition;
        private Vector3 _roamingPosition;
        private Rigidbody2D _enemyRigidbody2D;


        private void OnEnable()
        {
            enemyEventManager.StartListening(EnemyEventString.ON_ROAM, OnRoamingHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_CHASE, OnChaseHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_BACK_TO_ORIGIN, OnBackToOrigin);
            enemyEventManager.StartListening(EnemyEventString.ON_ATTACK, OnAttackHandler);
            enemyEventManager.StartListening(EnemyEventString.ON_DEAD, StopMovement);
        }

       
        private void OnDisable()
        {
            enemyEventManager.StopListening(EnemyEventString.ON_ROAM, OnRoamingHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_CHASE, OnChaseHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_BACK_TO_ORIGIN, OnBackToOrigin);
            enemyEventManager.StopListening(EnemyEventString.ON_ATTACK, OnAttackHandler);
            enemyEventManager.StopListening(EnemyEventString.ON_DEAD, StopMovement);
        }



        private void Awake()
        {
          
            _enemyRigidbody2D = GetComponent<Rigidbody2D>();    

           
        }

        private void Start()
        {
            _initialPosition = transform.position;
            _roamingPosition = new Vector3(UtilsClass.GetRandomPosition(_initialPosition, 20, 30).x, transform.position.y);
        }


        private void FixedUpdate()
        {
          ChangeDirection();
        }

        private void OnAttackHandler()
        {
            StopMovement();
        }

        private void OnChaseHandler()
        {
            Vector3 playerPosition = PlayerStateHandler.Instance.GetPosition();
            Vector3 newDirection = UtilsClass.CalculateDirection(transform.position, playerPosition);
            Move(newDirection, enemyStats.hSpeed);

        }

        private void OnRoamingHandler()
        {
            
            float distanceToRoamingPos = Vector3.Distance(transform.position, _roamingPosition);

            // Check proximity to roaming position
            if (distanceToRoamingPos < 1f)
            {
                _roamingPosition = new Vector3(_initialPosition.x, transform.position.y);

                float distanceToInitialPos = Vector3.Distance(transform.position, _initialPosition);

                if (distanceToInitialPos < 1f)
                {

                    _roamingPosition = new Vector3(UtilsClass.GetRandomPosition(_initialPosition, 20, 30).x, transform.position.y); ;
                }

            }

            // Calculate direction and move towards newRoamingPos
            Vector3 newDirection = UtilsClass.CalculateDirection(transform.position, _roamingPosition);
            Move(newDirection, enemyStats.hSpeed);

            
        }


        private void OnBackToOrigin()
        {
            
            Vector3 newDirection = UtilsClass.CalculateDirection(transform.position,_initialPosition);
            Move(newDirection, enemyStats.hSpeed);
        }


        public void Move(Vector2 newPosition , float speed)
        {
            _enemyRigidbody2D.velocity = new Vector2(newPosition.x, _enemyRigidbody2D.velocity.y) * speed;
        }

        public void StopMovement()
        {
            _enemyRigidbody2D.velocity = new Vector2(0, _enemyRigidbody2D.velocity.y);
        }


        private void ChangeDirection()
        {
            _direction = -Mathf.Sign(_enemyRigidbody2D.velocity.x);
            enemyGFX.transform.localScale = new Vector3(_direction, 1, 1);
        }
    }
}
