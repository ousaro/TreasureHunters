using Osaro.player.constant;
using Osaro.Utilities;
using System.Collections;
using UnityEngine;

namespace Osaro.player
{
    public  class  PlayerAttack : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.OnAttack += ApplyAttack;
        }

        private void OnDisable()
        {
            EventManager.OnAttack -= ApplyAttack;
        }
        public void ApplyAttack()
        {
            //attack logic
        }

    }
   
}
