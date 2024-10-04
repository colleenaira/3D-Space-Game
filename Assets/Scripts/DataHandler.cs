using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;
    private List<SceneData> allSceneData = new List<SceneData>();
    private string masterFilePath = "C:/Users/colle/Desktop/Game Data/MasterData.csv";   // Feel free to change the path to your local computer
    private string participantFilePath = "C:/Users/colle/Desktop/Game Data/P_Data.csv";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddSceneData(SceneData data)
    {
        allSceneData.Add(data);
    }

    public void ExportDataToCSV()
    {
        using (StreamWriter writer = new StreamWriter(participantFilePath))
        {
            writer.WriteLine("Scene Index,Completion Time,Collision Count,Total Collision Time");
            foreach (var data in allSceneData)
            {
                writer.WriteLine($"{data.sceneIndex},{data.completionTime},{data.collisionCount},{data.totalCollisionTime}");
            }
        }
    }
}

/*    public void ExportParticipantDataToCSV(int participantID, List<SceneData> data)
    {
        string filePath = string.Format(participantFilePath, participantID);

        using (StreamWriter writer = new StreamWriter(filePath, false)) // false to overwrite existing data
        {
            writer.WriteLine("Scene Index,Completion Time,Collision Count,Total Collision Time");
            foreach (var scene in data)
            {
                writer.WriteLine($"{scene.sceneIndex},{scene.completionTime},{scene.collisionCount},{scene.totalCollisionTime}");
            }
        }
    }

    public void UpdateMasterDataToCSV(int participantID, List<SceneData> data)
    {
        using (StreamWriter writer = new StreamWriter(masterFilePath, true)) // true to append data
        {
            foreach (var scene in data)
            {
                writer.WriteLine($"{participantID},{scene.sceneIndex},{scene.completionTime},{scene.collisionCount},{scene.totalCollisionTime}");
            }
        }
    }
*/
public class SceneData
    {
        public int sceneIndex;
        public float completionTime;
        public int collisionCount;
        public float totalCollisionTime;
    }


