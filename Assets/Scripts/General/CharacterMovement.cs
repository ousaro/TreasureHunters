using UnityEngine;

namespace osaro.utilities
{
    public class CharacterMovement : MonoBehaviour
    {
        // Auto-property for Rigidbody2D
        public Rigidbody2D Rigidbody2D { get; set; }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Method to move the player horizontally
        public void Move(float horizontalInput, float hSpeed)
        {
            if (Rigidbody2D == null)
            {
                Debug.LogError("Rigidbody2D is not assigned.");
                return;
            }

            Rigidbody2D.velocity = new Vector2(horizontalInput * hSpeed, Rigidbody2D.velocity.y);
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
