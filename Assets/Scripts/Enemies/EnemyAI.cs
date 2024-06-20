using UnityEngine;
using Pathfinding;
using Osaro.Enemy.Constrants;
using Osaro.Utilities;


namespace Osaro.Enemy
{
    public class EnemyAI: MonoBehaviour{

        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject enemyGFX;
        private AnimationController _enemyAnimationController;

        [SerializeField] private float nextWayPointDistance = 3f;
        private Path _path ;
        private int currentWayPoint =0;
        private bool _reackedEndOfPath = false;

        private float _direction = 1; //left
        private Seeker _seeker;
        private Rigidbody2D _enemyRigidBody2D;

        private Vector2 _force;


        private void Awake() {
            _seeker = GetComponent<Seeker>();
            _enemyRigidBody2D = GetComponent<Rigidbody2D>();
            _enemyAnimationController = enemyGFX.GetComponent<AnimationController>();

            InvokeRepeating("UpdatePath",0f,.5f);
        }

        private void UpdatePath(){

            if(_seeker.IsDone()){ 
                _seeker.StartPath(_enemyRigidBody2D.position, target.position, OnPathComplete);

            }
        }
        private void OnPathComplete(Path p){
            if(!p.error){
                _path = p;
                currentWayPoint = 0;
            }
        }

        private void Update() {
            HandleAnimation();
            ChangeDirection();
        }
        private void FixedUpdate() {

            if(_path == null)  {return;}
            
            if(currentWayPoint >=  _path.vectorPath.Count){
                _reackedEndOfPath = true;
                return;
            }else
            {
                _reackedEndOfPath = false;
            }        

            Vector2 direction = ((Vector2)_path.vectorPath[currentWayPoint] - _enemyRigidBody2D.position).normalized;
            _force = direction * enemyStats.hSpeed * Time.deltaTime;

            _enemyRigidBody2D.AddForce(_force);

            float distance = Vector2.Distance(_enemyRigidBody2D.position, _path.vectorPath[currentWayPoint]);

            if(distance< nextWayPointDistance){
                currentWayPoint++;
            }

             

        }


          private void ChangeDirection()
        {
            _direction = - Mathf.Sign(_force.x);
            enemyGFX.transform.localScale = new Vector3(_direction, 1, 1);
        }


        private void HandleAnimation()
        {
            string newAnimation = EnemyAnimationString.IDLE;

            if (_enemyRigidBody2D.velocity.x != 0)
            {
                newAnimation = EnemyAnimationString.RUN;
            }
            else
            {
                newAnimation = EnemyAnimationString.IDLE;
            }
            
            

            _enemyAnimationController.ChangeCurrentAnimation(newAnimation);
        }


    }
}
