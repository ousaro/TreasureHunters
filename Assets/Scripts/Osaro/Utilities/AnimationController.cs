using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Osaro.Utilities
{
    public class AnimationController : MonoBehaviour
    {
        private Animator _animator;
        private string _currentAnimation;

        public float AnimationDuration { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChangeCurrentAnimation(string newAnimation)
        {
            if (newAnimation == _currentAnimation) { return; }

            _animator.Play(newAnimation);
            _currentAnimation = newAnimation;
            UpdateAnimationDuration();
        }

        public void InstantiateVFX(GameObject vfxObjext)
        {
            Instantiate(vfxObjext,transform.position,Quaternion.identity);
        }

        private void UpdateAnimationDuration()
        {
            // Get the runtime animator controller
            RuntimeAnimatorController runtimeController = _animator.runtimeAnimatorController;

            // Iterate through all animation clips to find the one matching the current animation
            foreach (AnimationClip clip in runtimeController.animationClips)
            {
                if (clip.name == _currentAnimation)
                {
                    AnimationDuration = clip.length;
                    return;
                }
            }

            // If no match found, set duration to 0
            AnimationDuration = 0f;
        }
    }

  
}
