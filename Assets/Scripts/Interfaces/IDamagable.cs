using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public event Action<float,float> OnHealthChanged;

    public event Action OnDeath;
    public void Damage(AttackDetails attackDetails);
}
