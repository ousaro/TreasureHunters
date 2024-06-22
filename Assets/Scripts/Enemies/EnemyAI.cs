using UnityEngine;
using Pathfinding;
using Osaro.Enemy.Constrants;
using Osaro.Utilities;
using UnityEngine.Diagnostics;
using Osaro.player;
using Osaro.Utilities.Constants;
using System.Collections;

namespace Osaro.Enemy
{
    public class EnemyAI : MonoBehaviour {

        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject enemyGFX;
        [SerializeField] private AnimationController enemyAnimationController;
        [SerializeField] private CharacterMovement enemyMovement;

        [SerializeField] private Vector2 folowPlayerRange = Vector2.one;
        [SerializeField] private float nextWayPointDistance = 3f;

        [SerializeField] private Transform patrolPostion1;

        private enum State {Patrol, Chasing, BackToStart };
        private State state;
        private Path _path;
        private int _currentWayPoint = 0;
        private bool _reackedEndOfPath = false;

        private Vector3 _newTarget;

        private float _direction = 1; //left
        private Vector3 _initialPosition;
        private Seeker _seeker;

        private Vector2 _force;



        private void Awake() {
            _seeker = GetComponent<Seeker>();

            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        private void Start() {
            _initialPosition = transform.position;
            _newTarget = patrolPostion1.position;
            state = State.Patrol;
            _canUpdatePath = false;

        }

        private void UpdatePath() {

            if (_seeker.IsDone()) {
                _seeker.StartPath(enemyMovement.Rigidbody2D.position, _newTarget, OnPathComplete);

            }
        }

        private void EnemyStateHandler() {
            switch (state) {
                case State.Patrol:
                    _newTarget = patrolPostion1.position;
                    float reachedDistance = 1f;
                    float distance = Vector3.Distance(enemyMovement.Rigidbody2D.position, patrolPostion1.position);
                    if (distance < reachedDistance) {

                        state = State.BackToStart;
                    
                    }
                 

                    FindTarget();
                    
                    break;

                case State.Chasing:
                  
                    _newTarget = target.position;

                    float distancePlayerEnemyX = Mathf.Abs(PlayerController.Instance.transform.position.x - enemyMovement.Rigidbody2D.position.x);
                    float distancePlayerEnemyY = Mathf.Abs(PlayerController.Instance.transform.position.y - enemyMovement.Rigidbody2D.position.y);
                    if (distancePlayerEnemyX > folowPlayerRange.x/2 || distancePlayerEnemyY > folowPlayerRange.y/2) {

                        state = State.BackToStart;
                        
                    }
                    break;

                case State.BackToStart:

                    _newTarget = _initialPosition;

                    float reachedDistanceStart = 1f;
                    float distanceStart = Vector3.Distance(enemyMovement.Rigidbody2D.position, _initialPosition);
                    if(distanceStart < reachedDistanceStart)
                    {
               
                        state = State.Patrol;             
                    }
                    FindTarget();

                    break;
                 
            }
        }

        private void FindTarget() {
            float distancePlayerEnemyX = Mathf.Abs(PlayerController.Instance.transform.position.x - enemyMovement.Rigidbody2D.position.x);
            float distancePlayerEnemyY = Mathf.Abs(PlayerController.Instance.transform.position.y - enemyMovement.Rigidbody2D.position.y);

            if (distancePlayerEnemyX < folowPlayerRange.x / 2 && distancePlayerEnemyY < folowPlayerRange.y / 2) {
                state = State.Chasing;
               
            }
        }


        private void OnPathComplete(Path p) {
            if (!p.error) {
                _path = p;
                _currentWayPoint = 0;
            }
        }

        private void Update() {
           
            HandleAnimation();
            ChangeDirection();
 
        }


        private void FixedUpdate()
        {
            EnemyStateHandler();
            if (_path == null) { return; }

            if (_currentWayPoint >= _path.vectorPath.Count)
            {
                _reackedEndOfPath = true;
                
                return;
            }
            else
            {
                _reackedEndOfPath = false;
            }

           
            ApplyMovement();


        }

        private void ApplyMovement()
        {
            
            Vector3 newPos = CalclateNewPosition();

            enemyMovement.Move(newPos);

            float distanceToNextWayPoint = Vector2.Distance(enemyMovement.Rigidbody2D.position, _path.vectorPath[_currentWayPoint]);

            if (distanceToNextWayPoint < nextWayPointDistance)
            {
                _currentWayPoint++;
            }
        }

        private Vector3 CalclateNewPosition(){
            Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - enemyMovement.Rigidbody2D.position).normalized;
            _force = direction * enemyStats.hSpeed;
            Vector2 newPos = new Vector2(_force.x, enemyMovement.Rigidbody2D.velocity.y);
            return newPos;
        }

        private void ChangeDirection()
        {
            _direction = - Mathf.Sign(_force.x);
            enemyGFX.transform.localScale = new Vector3(_direction, 1, 1);
        }


        private void HandleAnimation()
        {
            string newAnimation = EnemyAnimationString.IDLE;

            if (enemyMovement.Rigidbody2D.velocity.x != 0)
            {
                newAnimation = EnemyAnimationString.RUN;
            }
            else
            {
                newAnimation = EnemyAnimationString.IDLE;
            }
            
            

            enemyAnimationController.ChangeCurrentAnimation(newAnimation);
        }

        private void OnDrawGizmos() {
            
            Color color = Color.blue;
            Gizmos.DrawWireCube(transform.position, folowPlayerRange);
        }

    }
}
