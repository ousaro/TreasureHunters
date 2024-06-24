using UnityEngine;
using System.Collections.Generic;

namespace Osaro.Utilities
{
    public class DrawGizmos : MonoBehaviour
    {
        public List<Shape> shapes = new List<Shape>();

        private void OnDrawGizmos()
        {
            if (shapes == null) return;

            foreach (var shape in shapes)
            {
                if (shape.center == null) continue;  // Skip shapes without a center

                Color color = shape.color.ToColor();

                switch (shape.shapeType)
                {
                    case Shape.ShapeType.Circle:
                        DrawCircle(shape.center.position, shape.radius, color);
                        break;
                    case Shape.ShapeType.Quad:
                        DrawQuad(shape.center.position, shape.size, color);
                        break;
                }
            }
        }

        private void DrawCircle(Vector3 center, float radius, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(center, radius);
        }

        private void DrawQuad(Vector3 center, Vector3 size, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireCube(center, size);
        }
    }
}
