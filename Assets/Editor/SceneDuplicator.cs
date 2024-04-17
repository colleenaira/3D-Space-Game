using UnityEditor;
using UnityEngine;

public class SceneDuplicator : EditorWindow
{
    [MenuItem("Tools/Duplicate Current Scene")]
    public static void DuplicateCurrentScene()
    {
        // Get the path of the current scene
        string originalScenePath = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
        // Create a new path for the duplicated scene
        string newScenePath = System.IO.Path.GetDirectoryName(originalScenePath) + "/DuplicatedScene.unity";

        // Save the duplicated scene
        UnityEditor.SceneManagement.EditorSceneManager.SaveScene(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene(), newScenePath);
        Debug.Log("Scene duplicated to: " + newScenePath);
    }
}
