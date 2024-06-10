using UnityEngine;

namespace osaro.player
{
    [CreateAssetMenu(fileName ="PlayerStats", menuName ="ScriptableObjects/PlayerStats")]
    public class PlayerStats: ScriptableObject
    {
        public float hSeed = 7;
        public float jumpForce = 5;
        public int maxHp = 100;

    }

}
