using UnityEngine;
using Osaro.player;
using System;
using Osaro.Utilities;

namespace Osaro.Enemy
{
    public class EnemyStateHandler : MonoBehaviour
    {
        public enum State { Patrol, Chasing, BackToStart };

        private State _state;
        private Vector3 _initialPosition;
        private Transform _patrolPosition;
        private Transform _target;
        private Vector2 _followPlayerRange;
        private Rigidbody2D _enemyRigidbody2D;

        public event Action<Vector3> OnNewTarget;

        public void Initialize(Vector3 initialPosition, Transform patrolPosition, Transform target, Vector2 followPlayerRange , Rigidbody2D enemyRigidbody2D)
        {
            _initialPosition = initialPosition;
            _patrolPosition = patrolPosition;
            _target = target;
            _followPlayerRange = followPlayerRange;
            _state = State.Patrol;
            _enemyRigidbody2D = enemyRigidbody2D;
        }

        private void Update()
        {
            EnemyStateLogic();
        }

        private void EnemyStateLogic()
        {
            switch (_state)
            {
                case State.Patrol:
                    PatrolMovementHandler();
                    break;

                case State.Chasing:
                    ChasingMovementHandler();
                    break;

                case State.BackToStart:
                    BackToStartMovementHandler();
                    break;
            }
        }

        private void PatrolMovementHandler()
        {
            SetNewTarget(_patrolPosition.position);
            float distance = Vector3.Distance(_enemyRigidbody2D.position, _patrolPosition.position);
            if (distance < 1f)
            {
                _state = State.BackToStart;
            }
            FindTarget();
        }

        private void ChasingMovementHandler()
        {
            SetNewTarget(_target.position);
            float distancePlayerEnemyX = Mathf.Abs(PlayerController.Instance.transform.position.x - _enemyRigidbody2D.position.x);
            float distancePlayerEnemyY = Mathf.Abs(PlayerController.Instance.transform.position.y - _enemyRigidbody2D.position.y);
            if (distancePlayerEnemyX > _followPlayerRange.x / 2 || distancePlayerEnemyY > _followPlayerRange.y / 2)
            {
                _state = State.BackToStart;
            }
        }

        private void BackToStartMovementHandler()
        {
            SetNewTarget(_initialPosition);
            float distance = Vector3.Distance(_enemyRigidbody2D.position, _initialPosition);
            if (distance < 1f)
            {
                _state = State.Patrol;
            }
            FindTarget();
        }

        private void FindTarget()
        {
            float distancePlayerEnemyX = Mathf.Abs(PlayerController.Instance.transform.position.x - _enemyRigidbody2D.position.x);
            float distancePlayerEnemyY = Mathf.Abs(PlayerController.Instance.transform.position.y - _enemyRigidbody2D.position.y);
            if (distancePlayerEnemyX < _followPlayerRange.x / 2 && distancePlayerEnemyY < _followPlayerRange.y / 2)
            {
                _state = State.Chasing;
            }
        }

        private void SetNewTarget(Vector3 targetPosition)
        {
            OnNewTarget?.Invoke(targetPosition);
        }
    }
}
