using UnityEngine;

namespace Osaro.Utilities
{
    public class CharacterMovement : MonoBehaviour
    {
        // Auto-property for Rigidbody2D
        public Rigidbody2D Rigidbody2D { get; set; }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Method to make the character move toward a position
        public void MoveToward(Vector2 newPosition) 
        {
            if (Rigidbody2D == null)
            {
                Debug.LogError("Rigidbody2D is not assigned.");
                return;
            }

            Rigidbody2D.velocity = new Vector2(newPosition.x, newPosition.y);
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
