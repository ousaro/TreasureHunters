using UnityEditor;
using UnityEngine;

namespace Osaro.Utilities
{
    [CustomEditor(typeof(DrawGizmos))]
    public class DrawGizmosEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawGizmos drawGizmos = (DrawGizmos)target;

            if (GUILayout.Button("Add Shape"))
            {
                drawGizmos.shapes.Add(new Shape());
            }

            for (int i = 0; i < drawGizmos.shapes.Count; i++)
            {
                var shape = drawGizmos.shapes[i];
                EditorGUILayout.LabelField($"Shape {i + 1}");
                shape.shapeType = (Shape.ShapeType)EditorGUILayout.EnumPopup("Shape Type", shape.shapeType);
                shape.center = (Transform)EditorGUILayout.ObjectField("Center", shape.center, typeof(Transform), true);

                EditorGUILayout.LabelField("Color");
                shape.color.r = EditorGUILayout.Slider("Red", shape.color.r, 0f, 1f);
                shape.color.g = EditorGUILayout.Slider("Green", shape.color.g, 0f, 1f);
                shape.color.b = EditorGUILayout.Slider("Blue", shape.color.b, 0f, 1f);
                shape.color.a = EditorGUILayout.Slider("Alpha", shape.color.a, 0f, 1f);

                switch (shape.shapeType)
                {
                    case Shape.ShapeType.Circle:
                        shape.radius = EditorGUILayout.FloatField("Radius", shape.radius);
                        break;
                    case Shape.ShapeType.Quad:
                        shape.size = EditorGUILayout.Vector3Field("Size", shape.size);
                        break;
                }

                // Add some space before the remove button
                GUILayout.Space(20);


                if (GUILayout.Button("Remove Shape"))
                {
                    drawGizmos.shapes.RemoveAt(i);
                    i--;
                }

                EditorGUILayout.Space();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
