using UnityEngine;


namespace Osaro.Enemy
{
    [CreateAssetMenu(fileName ="EnemyStats", menuName ="ScriptableObjects/EnemyStats")]
    public class EnemyStats: ScriptableObject
    {
        public float hSpeed = 5f;
        public float maxHP = 100;
    }
}
