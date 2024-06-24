using System;
using UnityEngine;

namespace Osaro.Utilities
{
    public static class EventManager
    {
        public static event Action OnAttack;
        public static event Action OnJump;
        public static event Action OnMove;
        public static event Action OnIdle;
        public static event Action OnFall;
        public static event Action OnAttackEnd;

        public static void TriggerAttackEnd()
        {
            OnAttackEnd?.Invoke();
        }
        public static void TriggerAttack()
        {
            OnAttack?.Invoke();
        }

        public static void TriggerJump()
        {
            OnJump?.Invoke();
        }

        public static void TriggerMove()
        {
            OnMove?.Invoke();
        }

        public static void TriggerIdle()
        {
            OnIdle?.Invoke();
        }

        public static void TriggerFall()
        {
            OnFall?.Invoke();
        }
    }

}
