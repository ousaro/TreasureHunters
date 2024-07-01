using Osaro.Enemy.Constrants;
using Osaro.Interface;
using Osaro.Utilities;
using System;
using UnityEngine;

namespace Osaro.Enemy
{
    public class EnemyHealthSystem : MonoBehaviour, IDamagable
    {

        [SerializeField] EnemyStats enemyStats;
        [SerializeField] EventManager enemyEventManager; 
        private float _currentHealth;

    
        private void Start()
        {
            _currentHealth = enemyStats.maxHP;
        }
        public void TakeDamage(float damageAmount)
        {
            _currentHealth -= damageAmount;
            enemyEventManager.TriggerEvent(EnemyEventString.ON_HEALTH_CHANGE);

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                EnemyDeath();
            }
          
           

        }

        public void EnemyDeath()
        {
            enemyEventManager.TriggerEvent(EnemyEventString.ON_DEAD);
        }
    }


}
