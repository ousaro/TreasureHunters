using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour, IDamagable
{
    public event Action<float, float> OnHealthChanged;

    public event Action OnDeath;

    private Animator _animator;

    [SerializeField]
    private AudioClip _ShotSFX;
    [SerializeField]
    private AudioClip _DestroySFX;
    [SerializeField]
    private AudioClip _HitSFX;

    [SerializeField]
    private Transform playerCheck;

    [SerializeField]
    private Transform attackPos;
    [SerializeField]
    private GameObject canonBall;

    [SerializeField]
    private float minAgroRange=3f;

    [SerializeField]
    private LayerMask whatIsPlayer;

    [SerializeField]
    private float maxHealth = 30f;

    private float _currentHealth;

    private float _attackStartTime;
    [SerializeField]
    private float timeBetweenAttacks =0.5f;

    private bool _isPlayerInMinAgroRange;



    private enum States { idle , attack, hit, destroy};
    private States _currentState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();      
        _currentHealth = maxHealth;

       
    }

    private void Start()
    {
        _currentState = States.idle;
    }

    private void Update()
    {
        _isPlayerInMinAgroRange = CheckPlayerInMinAgroRange();


        switch (_currentState)
        {
            case States.idle:
                UpdateIdle();
                break;
            case States.attack: 
                UpdateAttack();
                break;
            case States.hit:
                UpdateHit();
                break;
            case States.destroy:     
                UpdateDestroy();
                break;
        }
       

    }


    public void Damage(AttackDetails attackDetails)
    {
        SwitchState(States.hit);
        _currentHealth -= attackDetails.damageAmout;
        OnHealthChanged?.Invoke(_currentHealth, maxHealth);

       

        if (_currentHealth <= 0)
        {
            SwitchState(States.destroy);
            OnDeath?.Invoke();
        }
    }

    private bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, minAgroRange, whatIsPlayer);

    }

  

    // Idle State

    private void EnterIdle()
    {
        _animator.SetBool("idle", true);
    }

    private void ExitIdle()
    {
        _animator.SetBool("idle", false);
    }

    private void UpdateIdle()
    {
        if (_isPlayerInMinAgroRange && Time.time >= _attackStartTime + timeBetweenAttacks)
        {
            SwitchState(States.attack);

        }
       
    }


    // Attack State

    private void EnterAttack()
    {
        _animator.SetBool("fire", true);
        _attackStartTime = Time.time;
        
    }

    private void ExitAttack()
    {
        _animator.SetBool("fire", false);
    }

    private void UpdateAttack()
    {
        if (!_isPlayerInMinAgroRange)
        {
            SwitchState(States.idle);
        }
    }

    private void TriggerAttackAnimation()
    {
        Instantiate(canonBall, attackPos.position, Quaternion.identity);
        SoundManager.Instance.PlaySoundFXClip(_ShotSFX, transform, 0.1f);
    }
    private void TriggerFinishAttackAnimation()
    {
        SwitchState(States.idle);
    }


    // Hit state

    private void EnterHit()
    {
        _animator.SetBool("hit", true);
        SoundManager.Instance.PlaySoundFXClip(_HitSFX, transform, 1f);
    }

    private void ExitHit()
    {
        _animator.SetBool("hit", false);
    }

    private void UpdateHit()
    {

    }

    private void TriggerFinishHitAnimation()
    {
        SwitchState(States.idle);
    }

    // Destroy State

    private void EnterDestroy()
    {
        _animator.SetBool("destroy", true);
        SoundManager.Instance.PlaySoundFXClip(_DestroySFX, transform, 1f);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    private void ExitDestroy()
    {
        _animator.SetBool("destroy", false);
    }

    private void UpdateDestroy()
    {

    }


    // Other function

    private void SwitchState(States state)
    {
        switch (_currentState)
        {
            case States.idle:
                ExitIdle();
                break;
            case States.attack:
                ExitAttack();
                break;
            case States.hit:
                ExitHit();
                break;
            case States.destroy:
                ExitDestroy();
                break;
        }


        switch (state)
        {
            case States.idle:
                EnterIdle();
                break;
            case States.attack:
                EnterAttack();
                break;
            case States.hit:
                EnterHit();
                break;
            case States.destroy:
                EnterDestroy();
                break;
        }

        _currentState = state;

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerCheck.position - transform.right * minAgroRange, 0.2f);
       
    }


}
