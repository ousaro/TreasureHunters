using Osaro.player.constant;
using Osaro.Utilities;
using System.Collections;
using UnityEngine;

namespace Osaro.player
{
    public  class  PlayerAttack : MonoBehaviour
    {
        [SerializeField] private EventManager playerEventManager;
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
        }

    }
   
}
