using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variable
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerLandState LandState { get; private set; }

    public PlayerInAirState InAirState { get; private set; }  

    public PlayerAttackState AttackState { get; private set; }


    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Animator { get; private set; }

    public Rigidbody2D Rigidbody2D { get; private set; }

    public PlayerInputHandler InputHandler { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;

    #endregion

    #region Other Variables

    [SerializeField]
    private Transform attackPosition;
    public Vector2 CurrentVelocity { get; private set; } 

    public int FacingDirection { get; private set; }


    private Vector2 _workSpace;
    #endregion


    #region Unity CallBack Functions
    private void Awake()
    {

        StateMachine = new PlayerStateMachine();
        

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", attackPosition);



    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        FacingDirection = 1; // facing right

        StateMachine.Initialize(IdleState);
        
    }

    private void Update()
    {
        CurrentVelocity = Rigidbody2D.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion


    #region Set Functions
    public void SetVelocityX(float velociry)
    {
        _workSpace.Set(velociry, CurrentVelocity.y);
        Rigidbody2D.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    public void SetVelocityY(float velociry)
    {
        _workSpace.Set(CurrentVelocity.x, velociry);
        Rigidbody2D.velocity = _workSpace;
        CurrentVelocity = _workSpace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.gourndCheckRadius, playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }



    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.localScale= new Vector3(FacingDirection, 1, 1);
    }


    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position, playerData.attackRadius);
        Gizmos.DrawWireSphere(groundCheck.position, playerData.gourndCheckRadius);
        
    }
    #endregion
}
