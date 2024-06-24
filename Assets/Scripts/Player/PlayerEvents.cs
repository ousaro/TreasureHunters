using System;
using UnityEngine;

namespace Osaro.player
{
    public static class PlayerEvents
    {
        public static event Action OnAttack;
        public static event Action OnJump;
        public static event Action OnLand;
        public static event Action OnMove;

        public static void TriggerAttack()
        {
            OnAttack?.Invoke();
        }

        public static void TriggerJump()
        {
            OnJump?.Invoke();
        }

        public static void TriggerLand()
        {
            OnLand?.Invoke();
        }

        public static void TriggerMove()
        {
            OnMove?.Invoke();
        }
    }
}
