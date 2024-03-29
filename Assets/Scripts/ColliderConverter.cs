using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColliderConverter : EditorWindow
{
    [MenuItem("Tools/Collider Converter")]
    public static void ShowWindow()
    {
        GetWindow<ColliderConverter>("Collider Converter");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Convert BoxColliders to MeshColliders"))
        {
            ConvertColliders();
        }
    }

    private static void ConvertColliders()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            BoxCollider[] boxColliders = obj.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider boxCollider in boxColliders)
            {
                DestroyImmediate(boxCollider);
                MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
                meshCollider.convex = true; // Set to true if you want a convex mesh collider
            }
        }
    }
}

