using UnityEngine;
using UnityEditor;
using System.Linq;

public class TaggingTool : EditorWindow
{
    [MenuItem("Tools/Tagging Tool")]
    public static void ShowWindow()
    {
        GetWindow<TaggingTool>("Tagging Tool");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Tag Walls and Gates"))
        {
            TagWallsAndGates();
        }
    }


    private static void TagWallsAndGates()
    {
        // Find all GameObjects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>(true);

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Wall"))
            {
                Undo.RecordObject(obj, "Tag to Wall");
                obj.tag = "Wall";
            }
            else if (obj.name.Contains("GateCollider"))
            {
                Undo.RecordObject(obj, "Tag to Gate");
                obj.tag = "Gate";
            }
        }
    }
}
