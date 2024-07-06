using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;

    [Header("In Air State")]
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Player Attack State")]
    public float attackRadius = 1f;
    public float damageAmount = 5f;
    public float stunDamageAmount = 1f;
  

    public LayerMask whatIsEnemy;

    [Header("Player Stun State")]
    public float stunKnockBackSpeed = 10f;
    public Vector2 stunKnockBackAngle;

    [Header("Player health")]
    public float maxHealth = 50f;
    public float damageHopSpeed = 5f;


    [Header("Check Variables")]
    public float gourndCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    [Header("Interactions variables")]
    public float interactionCheckRadius = 1f;

    public LayerMask whatIsInteracteble;
}
