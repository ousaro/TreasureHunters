using UnityEditor;
using UnityEngine;

namespace Osaro.Utilities
{
    public class CharacterMovement : MonoBehaviour
    {
        // Auto-property for Rigidbody2D
        public Rigidbody2D Rigidbody2D { get; set; }

        private int _patrolDirection = 1;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Method to make the character move toward a position
        public void Move(Vector2 newPosition) 
        {
            if (Rigidbody2D == null)
            {
                Debug.LogError("Rigidbody2D is not assigned.");
                return;
            }

            Rigidbody2D.velocity = new Vector2(newPosition.x, newPosition.y);
        }

        public void Patrol(Vector3 position1, Vector3 position2, float speed)
        {
            Vector2 target = currentPatrolMvtTarget(position1, position2);

            Vector3 direction = (target - Rigidbody2D.position).normalized;
            Vector3 newPos = direction * speed;

            Move(newPos);

            ChangePatrolDirection(target);




        }

        public void ChangePatrolDirection(Vector2 target)
        {
            float distance = (target - Rigidbody2D.position).magnitude;
            if(distance < 0.1f)
            {
                _patrolDirection *= -1;
            }
        }

        public Vector3 currentPatrolMvtTarget(Vector2 position1, Vector2 position2)
        {
            bool isOnPosition1 = _patrolDirection == 1;
            if(isOnPosition1)
            {
                return position2;

            }
            else
            {
                return position1;
            }
        }

        public void StopMovement()
        {
            Rigidbody2D.velocity = Vector2.zero;
        }

        // Method to make the player jump
        public void Jump(float jumpForce)
        {
            if (Rigidbody2D == null)
            {
                Debug.LogError("Rigidbody2D is not assigned.");
                return;
            }

            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, jumpForce);
        }
    }

}
