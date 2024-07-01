using Osaro.Enemy.Constrants;
using Osaro.Enemy;
using Osaro.player.constant;
using Osaro.Utilities;
using System.Collections;
using UnityEngine;
using Osaro.Interface;

namespace Osaro.player
{
    public  class  PlayerAttack : MonoBehaviour
    {
        [SerializeField] private EventManager playerEventManager;
        [SerializeField] private Transform attackCenter;
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask attackableLayerMask;
        [SerializeField] private float damageAmount;



        private void OnEnable()
        {

            playerEventManager.StartListening(PlayerEventsString.ON_ATTACK, ApplyAttack);
        }

        private void OnDisable()
        {
            playerEventManager.StopListening(PlayerEventsString.ON_ATTACK, ApplyAttack);
        }
        public void ApplyAttack()
        {
            //attack logic
            Collider2D[] hits = Physics2D.OverlapCircleAll(attackCenter.position, attackRange, attackableLayerMask);

            foreach (Collider2D hit in hits)
            {
                IDamagable damagable = hit.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(damageAmount);
                }
            }

        }

    }

}
