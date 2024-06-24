using UnityEngine;
using Pathfinding;
using Osaro.Enemy.Constrants;
using Osaro.Utilities;
using Osaro.player;
using Osaro.Utilities.Constants;

namespace Osaro.Enemy
{
    [RequireComponent(typeof(EnemyStateHandler))]
    [RequireComponent(typeof(EnemyStats))]

    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] private EnemyStateHandler stateHandler;

        private Rigidbody2D _enemyRigidbody2D;

        [SerializeField] private Vector2 followPlayerRange = Vector2.one;
        [SerializeField] private Transform target;
        [SerializeField] private Transform patrolPosition1;
        private Vector2 _velocity;
        private Vector3 _currentTarget;

        [SerializeField] private float nextWayPointDistance = 3f;
        private Path _path;
        private int _currentWayPoint = 0;
        private Seeker _seeker;

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _enemyRigidbody2D = GetComponent<Rigidbody2D>();    

            stateHandler.Initialize(transform.position, patrolPosition1, target, followPlayerRange, _enemyRigidbody2D);
            stateHandler.OnNewTarget += OnNewTarget;

            InvokeRepeating("UpdatePathInvoker", 0f, .5f);
        }

        private void OnNewTarget(Vector3 newTarget)
        {
            _currentTarget = newTarget;
        }

        // Parameterless method for InvokeRepeating
        private void UpdatePathInvoker()
        {
            UpdatePath(_currentTarget);
        }


        private void UpdatePath(Vector3 newTarget)
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_enemyRigidbody2D.position, newTarget, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _currentWayPoint = 0;
            }
        }

        private void FixedUpdate()
        {
            if (_path == null) return;

            if (_currentWayPoint >= _path.vectorPath.Count)
            {
                return;
            }

            ApplyMovement();
        }

        private void ApplyMovement()
        {
            Vector3 newPos = CalculateNewPosition();
            Move(newPos);

            float distanceToNextWayPoint = Vector2.Distance(_enemyRigidbody2D.position, _path.vectorPath[_currentWayPoint]);
            if (distanceToNextWayPoint < nextWayPointDistance)
            {
                _currentWayPoint++;
            }
        }

        public void Move(Vector2 newPosition)
        {
            _enemyRigidbody2D.velocity = new Vector2(newPosition.x, newPosition.y);
        }

        private Vector3 CalculateNewPosition()
        {
            Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - _enemyRigidbody2D.position).normalized;
            _velocity = direction * enemyStats.hSpeed;
            Vector2 newPos = new Vector2(_velocity.x, _enemyRigidbody2D.velocity.y);
            return newPos;
        }
    }
}
