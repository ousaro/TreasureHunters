using UnityEngine;
using Osaro.player;
using Osaro.Utilities;
using Osaro.player.constant;
using Osaro.Enemy.Constrants;
using System.Collections;

namespace Osaro.Enemy
{
    public class EnemyStateHandler : MonoBehaviour
    {
        [SerializeField] private EventManager enemyEventManager;
        [SerializeField] private Vector2 followPlayerRange = Vector2.one;
        [SerializeField] private Vector2 attackRange = Vector2.one;

        public enum State { Roam, Idle, Chasing, BackToStart, Attack, Hit, Dead }
        private State _state;
        private Vector3 _initialPosition;
        private Rigidbody2D _enemyRigidbody2D;

        private bool _takeHit;
        private bool _isDead;

        private void OnEnable()
        {
            
            enemyEventManager.StartListening(EnemyEventString.ON_HEALTH_CHANGE, OnHealthChange_Handler);
            enemyEventManager.StartListening(EnemyEventString.ON_DEAD, OnDead_Handler);
        }

        private void OnDisable()
        {
            
            enemyEventManager.StopListening(EnemyEventString.ON_HEALTH_CHANGE, OnHealthChange_Handler);
            enemyEventManager.StopListening(EnemyEventString.ON_DEAD, OnDead_Handler);
        }

        private void Awake()
        {
            _enemyRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _initialPosition = transform.position;
            _state = State.Roam;
            enemyEventManager.TriggerEvent(EnemyEventString.ON_MOVE);
            _isDead = false;
            _takeHit = false;
        }

        private void Update()
        {
            EnemyStateLogic();
           
        }

        private void OnDead_Handler()
        {
           
           _isDead=true;
        }

        private void OnHealthChange_Handler()
        {
           _takeHit=true;
        }

        private void EnemyStateLogic()
        {
            switch (_state)
            {
                case State.Roam:
                    RoamStateHandler();
                    break;
                case State.Chasing:
                    ChaseStateHandler();
                    break;
                case State.BackToStart:
                    BackToStartStateHandler();
                    break;
                case State.Attack:
                    AttackStateHandler();
                    break;
                case State.Idle:
                    IdleStateHandler();
                    break;
                case State.Hit:
                    HitStateHandler();
                    
                    break;
                case State.Dead:
                    DeadStateHandler();
                    break;
            }
        }

        private void DeadStateHandler()
        {
            Debug.Log("DeadStateHandler called");

        }

        private void HitStateHandler()
        {

            //StartCoroutine(HitStateCoroutine());
            Debug.Log("hit");
        }

        IEnumerator HitStateCoroutine()
        {
            yield return new WaitForSeconds(5f);
            _state = State.Idle;
        }

        private void IdleStateHandler()
        {
            if (_takeHit)
            {
                _state = State.Hit;
            }
            else if (_isDead)
            {
                _state = State.Dead;
            }
            StartCoroutine(IdleCoroutine(1f));
        }

        IEnumerator IdleCoroutine(float timeBetweenAttacks)
        {
            enemyEventManager.TriggerEvent(EnemyEventString.ON_IDLE);
            yield return new WaitForSeconds(timeBetweenAttacks);
            _state = State.Attack;
            
        }

        private void RoamStateHandler()
        {
            enemyEventManager.TriggerEvent(EnemyEventString.ON_ROAM);
            FindTarget();
        }

        private void ChaseStateHandler()
        {
            enemyEventManager.TriggerEvent(EnemyEventString.ON_CHASE);
            float distancePlayerEnemyX = Mathf.Abs(PlayerStateHandler.Instance.transform.position.x - _enemyRigidbody2D.position.x);
            float distancePlayerEnemyY = Mathf.Abs(PlayerStateHandler.Instance.transform.position.y - _enemyRigidbody2D.position.y);
            if (distancePlayerEnemyX > followPlayerRange.x / 2 || distancePlayerEnemyY > followPlayerRange.y / 2)
            {
                _state = State.BackToStart;
            }
            if (distancePlayerEnemyX < attackRange.x / 2 && distancePlayerEnemyY < attackRange.y / 2)
            {
                _state = State.Attack;
            }
        }

        private void BackToStartStateHandler()
        {
            enemyEventManager.TriggerEvent(EnemyEventString.ON_BACK_TO_ORIGIN);
            float distance = Vector3.Distance(_enemyRigidbody2D.position, _initialPosition);
            if (distance < 1f)
            {
                _state = State.Roam;
            }
            FindTarget();
        }

        private void AttackStateHandler()
        {
            float distancePlayerEnemyX = Mathf.Abs(PlayerStateHandler.Instance.transform.position.x - _enemyRigidbody2D.position.x);
            float distancePlayerEnemyY = Mathf.Abs(PlayerStateHandler.Instance.transform.position.y - _enemyRigidbody2D.position.y);
            if (distancePlayerEnemyX > attackRange.x / 2 || distancePlayerEnemyY > attackRange.y / 2)
            {
                _state = State.Chasing;
                enemyEventManager.TriggerEvent(EnemyEventString.ON_MOVE);
            }
            else
            {
                StartCoroutine(AttackCoroutine(1f));
            }
        }

        IEnumerator AttackCoroutine(float timeToWait)
        {
            enemyEventManager.TriggerEvent(EnemyEventString.ON_ATTACK);
            yield return new WaitForSeconds(timeToWait);
            _state = State.Idle;
        }

        private void FindTarget()
        {
            float distancePlayerEnemyX = Mathf.Abs(PlayerStateHandler.Instance.transform.position.x - _enemyRigidbody2D.position.x);
            float distancePlayerEnemyY = Mathf.Abs(PlayerStateHandler.Instance.transform.position.y - _enemyRigidbody2D.position.y);
            if (distancePlayerEnemyX < followPlayerRange.x / 2 && distancePlayerEnemyY < followPlayerRange.y / 2)
            {
                _state = State.Chasing;
            }
            if (distancePlayerEnemyX < attackRange.x / 2 && distancePlayerEnemyY < attackRange.y / 2)
            {
                _state = State.Attack;
            }
        }
    }
}
