using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun Data")]

public class D_StunState : ScriptableObject
{
    public float stunTime = 3f;

    public float stunKnockBackTime = 0.2f;
    public float stunKnockBackSpeed = 20f;

    public Vector2 stunKnockBackAngle;
}
