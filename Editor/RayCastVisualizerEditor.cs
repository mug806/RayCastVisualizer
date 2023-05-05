using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RayCastVisualizer))]
public class RayCastVisualizerEditor : Editor
{
    public override void OnInspectorGUI() {
        RayCastVisualizer visualizer = (RayCastVisualizer)target;
        DrawDefaultInspector();

        if(visualizer.raycastMode == RayCastVisualizer.RaycastMode.Box) {
            visualizer.halfExtents = EditorGUILayout.Vector3Field("Half Extents",visualizer.halfExtents);
        }

        visualizer.autoCast = EditorGUILayout.Toggle("Auto Cast", visualizer.autoCast);

        if(!visualizer.autoCast) {
            if (GUILayout.Button("Cast"))
                visualizer.RayCast();
        }

        GUI.enabled = false;
        if (visualizer.IsValid()) {
            EditorGUILayout.FloatField("Hit Distance", visualizer.GetHitInfo().distance);
            EditorGUILayout.ObjectField("Collider", visualizer.GetHitInfo().collider,typeof(Collider),true);
        } else {
            GUILayout.Label("No hit data");
        }
    }
}
