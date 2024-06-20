using UnityEngine;

namespace Osaro.Utilities
{
    public class IsOnCollision : MonoBehaviour
    {
        private bool _isOnCollision;
        private string _currentCollisionTag;

        public bool With(string tag)
        {
            return _isOnCollision && _currentCollisionTag == tag;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _isOnCollision = true;
            _currentCollisionTag = collision.tag;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _isOnCollision = false;
            _currentCollisionTag = null;
        }
    }

}
