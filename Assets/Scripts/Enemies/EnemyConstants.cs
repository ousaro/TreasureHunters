using System.Collections;
using UnityEngine;

namespace Osaro.Enemy.Constrants
{
    public static class EnemyAnimationString
    {

        public const string IDLE = "Idle";
        public const string RUN = "Run";
        public const string ATTACK = "Attack";
        public const string DEAD = "Dead";
        public const string HIT = "Hit";

    }

    public static class EnemyEventString
    {

        public const string ON_IDLE = "OnIdle_Enemy";
        public const string ON_MOVE = "OnMove_Enemy";
        public const string ON_ATTACK = "OnAttack_Enemy";
        public const string ON_DEAD = "OnDead_Enemy";
        public const string ON_PATROL = "OnPatrol_Enemy";
        public const string ON_ROAM = "OnRoam_Enemy";
        public const string ON_CHASE = "OnChase_Enemy";
        public const string ON_BACK_TO_ORIGIN = "OnBackToOrigin_Enemy";

    }

}