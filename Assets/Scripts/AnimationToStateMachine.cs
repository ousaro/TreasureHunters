using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attacState;
    private void TriggerAttack()
    {
        attacState.TriggerAttack();
    }

    private void TriggerFinishAttack()
    {
        attacState.TriggerFinishAttack();
    }
}
