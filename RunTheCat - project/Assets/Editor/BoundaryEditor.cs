using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ColliderBoundary))]
public class BoundaryEditor : Editor {

    public override void OnInspectorGUI()
    {
        ColliderBoundary Target = (ColliderBoundary)target;

        Target.size = EditorGUILayout.Vector2Field(
            "Boundary Size", Target.size);

        Target.thickness = EditorGUILayout.FloatField(
            "Boundary Thickness", Target.thickness);

        if (UnityEngine.GUI.changed)
        {
            EditorUtility.SetDirty(Target);
            Target.UpdateLines(Target.thickness, Target.size.x, Target.size.y);
        }
    }
}
