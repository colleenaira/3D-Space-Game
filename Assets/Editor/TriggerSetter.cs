using UnityEngine;
using UnityEditor;

public class TriggerSetter : ScriptableObject
{
    [MenuItem("Tools/Set Triggers for Selected Objects")]
    static void SetTriggers()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }
    }
}
