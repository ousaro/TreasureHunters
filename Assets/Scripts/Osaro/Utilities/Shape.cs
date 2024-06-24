using UnityEngine;

namespace Osaro.Utilities
{
    [System.Serializable]
    public struct ShapeColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public ShapeColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color ToColor()
        {
            return new Color(r, g, b, a);
        }
    }

    [System.Serializable]
    public class Shape
    {
        public enum ShapeType { Circle, Quad }
        public ShapeType shapeType;
        public Transform center;
        public ShapeColor color = new ShapeColor(1f, 1f, 1f, 1f); // Default to white color
        public float radius = 1f;
        public Vector3 size = Vector3.one;
    }
}
