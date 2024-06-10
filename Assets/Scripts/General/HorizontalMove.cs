using UnityEngine;

namespace osaro.utilities
{
    public class HorizontalMove : MonoBehaviour
    {

        [SerializeField] private Rigidbody2D rigidbody2d;

       
        public void Move(float horizontalInput, float hSpeed)
        {
            rigidbody2d.velocity = new Vector2(horizontalInput * hSpeed, rigidbody2d.velocity.y);

        }


        public void Jump(float jumpForce)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);

        }
    }

}
