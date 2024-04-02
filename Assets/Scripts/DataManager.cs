using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private List<ParticipantData> allParticipantData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            allParticipantData = new List<ParticipantData>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RecordData(string participantID, string condition, float health, float accuracy, float time)
    {
        // Find the participant data or create a new one
        var participantData = allParticipantData.Find(p => p.ParticipantId == participantID);
        if (participantData == null)
        {
            participantData = new ParticipantData(participantID);
            allParticipantData.Add(participantData);
        }

        // Record the trial data
        participantData.Trials.Add(new TrialData(condition, participantData.Trials.Count + 1, health, accuracy, time));
    }

    public void SaveAllDataToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "participantData.json");
        string jsonData = JsonUtility.ToJson(allParticipantData, true);
        File.WriteAllText(filePath, jsonData);
    }
}

[System.Serializable]
public class ParticipantData
{
    public string ParticipantId;
    public List<TrialData> Trials = new List<TrialData>();

    public ParticipantData(string participantId)
    {
        ParticipantId = participantId;
    }
}

[System.Serializable]
public class TrialData
{
    public string Condition;
    public int TrialNumber;
    public float Health;
    public float Accuracy;
    public float CompletionTime;

    public TrialData(string condition, int trialNumber, float health, float accuracy, float completionTime)
    {
        Condition = condition;
        TrialNumber = trialNumber;
        Health = health;
        Accuracy = accuracy;
        CompletionTime = completionTime;
    }
}
