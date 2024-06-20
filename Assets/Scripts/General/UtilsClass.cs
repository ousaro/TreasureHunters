using UnityEngine;

namespace Osaro.Utilities
{
    public class UtilsClass {

        // Generate a random position within the defined range
        public Vector3 GetRandomDirection()
        {
            return new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

    }

}
