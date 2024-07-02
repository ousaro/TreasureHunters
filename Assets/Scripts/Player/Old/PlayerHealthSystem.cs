using Osaro.Interface;
using Osaro.player.constant;
using Osaro.Utilities;
using UnityEngine;

namespace Osaro.player
{
    public class PlayerHealthSystem : MonoBehaviour, IDamagable
    {

        [SerializeField] PlayerStats playerStats;
        [SerializeField] EventManager playerEventManager;
        private float _currentHealth;


        private void Start()
        {
            _currentHealth = playerStats.maxHp;
        }
        public void TakeDamage(float damageAmount)
        {
            _currentHealth -= damageAmount;
             
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                PlayerDeath();
            }

            playerEventManager.TriggerEvent(PlayerEventsString.ON_HEALTH_CHANGE);

        }

        public void PlayerDeath()
        {
            playerEventManager.TriggerEvent(PlayerEventsString.ON_DEAD);
        }
    }

}
