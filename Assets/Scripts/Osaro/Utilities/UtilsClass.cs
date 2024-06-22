using UnityEngine;

namespace Osaro.Utilities
{
    public static class UtilsClass
    {

        // Generate a random position within the defined range
        public static Vector3 GetRandomDirection()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        }

        public static Vector3 GetRandomDirectionX()
        {
            return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, 0).normalized;
        }

        public static Vector3 GetRandomPositionX(Vector3 initialPosition, int minVal, int maxVal)
        {
            return initialPosition + GetRandomDirectionX() * UnityEngine.Random.Range(minVal, maxVal);
        }

        public static Vector3 GetRandomPosition(Vector3 initialPosition, int minVal, int maxVal)
        {
            return initialPosition + GetRandomDirection() * UnityEngine.Random.Range(minVal, maxVal);
        }
    }
    
}
