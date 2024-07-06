using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/PlayerDetected Data")]

public class D_PlayerDetectedState : ScriptableObject
{
    public float timeBetweenAttacks = 0.5f;
}
