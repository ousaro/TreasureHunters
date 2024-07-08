using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable
{
    #region Events

    public Action OnDeath;

    #endregion

    #region Components
    public D_Entity entityData;

    public FiniteStateMachine stateMachine;


    public Rigidbody2D Rigidbody2d { get; private set; }

    public Animator Animator { get; private set; }

    public AnimationToStateMachine AnimationToStateMachine { get; private set; }


    #endregion

    #region Transforms variable

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;
    [SerializeField]
    private Transform groundCheck;

    #endregion

    #region Other Variables

    private float _currentHealth;
    private float _currentStunResistance;
    private float _lastDamageTime;

    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int LastDamageDirection { get; private set; }

    private Vector2 _velocityWorkSpace;

    protected bool _isStunned;
    protected bool _isDead;

    #endregion

    #region Unity CallBack Function
    public virtual void Start()
    {
        FacingDirection = -1; // this is depend on the enemy setup in unity 1 right -1 left

        _currentHealth = entityData.maxHealth;
        _currentStunResistance = entityData.stunResistance;

       

       
        Rigidbody2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();
        

        stateMachine = new FiniteStateMachine();
        
    }

    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
        CurrentVelocity = Rigidbody2d.velocity;

        if(Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Functions

    public void SetVelocity(float velocity)
    {
        _velocityWorkSpace.Set(FacingDirection * velocity, CurrentVelocity.y);
        Rigidbody2d.velocity = _velocityWorkSpace;
        CurrentVelocity = _velocityWorkSpace;

    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody2d.velocity = _velocityWorkSpace;
        CurrentVelocity = _velocityWorkSpace;

    }

    #endregion

    #region Check Functions
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, -transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, entityData.minAgroDistance, entityData.whatIsPlayer); // -1 here because the sprite of the enemy is facing left at the beggining
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, -transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }
    #endregion

    #region Other Functions
    public virtual void DamageHop(float velocity)
    {
        _velocityWorkSpace.Set(CurrentVelocity.x, velocity);
        Rigidbody2d.velocity = _velocityWorkSpace;
        CurrentVelocity = _velocityWorkSpace;

    }

    public virtual void ResetStunResistance()
    {
        _isStunned = false;
        _currentStunResistance = entityData.stunResistance;
    }
    public virtual void Damage(AttackDetails attackDetails)
    {
        _lastDamageTime = Time.time;

        _currentHealth -= attackDetails.damageAmout;
        _currentStunResistance -= attackDetails.stunDamageAmout;

        DamageHop(entityData.damageHopSpeed);

        if(attackDetails.position.x > transform.position.x)
        {
            LastDamageDirection = -1;
        }
        else
        {
            LastDamageDirection = 1;
        }

        if (FacingDirection == LastDamageDirection)
        {
            Flip();
        }

        if (_currentStunResistance <= 0)
        {
            _isStunned = true;
        }

        if(_currentHealth <= 0)
        {
            _isDead = true;
        }

    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public virtual void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Draw Gizmos 
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)Vector2.right * FacingDirection * entityData.wallCheckDistance);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)Vector2.down * entityData.ledgeCheckDistance);
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)Vector2.right * FacingDirection * entityData.minAgroDistance);

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)Vector2.right *FacingDirection * entityData.closeRangeActionDistance, 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)Vector2.right * FacingDirection * entityData.minAgroDistance, 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)Vector2.right * FacingDirection * entityData.maxAgroDistance, 0.2f);





    }

    #endregion
}
