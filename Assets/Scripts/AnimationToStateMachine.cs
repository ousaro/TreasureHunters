using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attacState;
    public DeathState deathState;
    private void TriggerAttack()
    {
        attacState.TriggerAttack();
       
    }

    private void TriggerFinishAttack()
    {
        attacState.TriggerFinishAttack();
    }

    private void TriggerAnimation()
    {
        deathState.TriggerAnimation();
    }

    private void TriggerFinishAnimation()
    {
        deathState.TriggerFinishAnimation();
    }
}
