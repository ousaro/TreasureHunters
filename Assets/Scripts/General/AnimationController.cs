using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] private Animator animator;

    private string _currentAnimation;

    public void ChangeCurrentAnimation(string newAnimation)
    {
        if(newAnimation == _currentAnimation) { return; }

        animator.Play(newAnimation);
        _currentAnimation = newAnimation;
    }
}
