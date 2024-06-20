using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Osaro.Utilities
{
    public class AnimationController : MonoBehaviour
    {

        private Animator _animator;

        private string _currentAnimation;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void ChangeCurrentAnimation(string newAnimation)
        {
            if (newAnimation == _currentAnimation) { return; }

            _animator.Play(newAnimation);
            _currentAnimation = newAnimation;
        }
    }


}
