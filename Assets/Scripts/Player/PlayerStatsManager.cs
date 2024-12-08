using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }

    public float TimeSinceNewScene { get; set; } = 0f;

       // public Dictionary<string, float> timeRecords = new();
    public List<float> timeRecords = new();

    private void Awake()
    {
        if(Instance != null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadTimeData();
    }

    /// <summary>
    /// Compares time with previous results and overrides if time is less previous record
    /// </summary>
    /// <param name="key">Level name</param>
    /// <param name="time">Level pass time</param>
    //public void TryOverrideTimeData(string key, float time)
    //{
    //    timeRecords[key] = timeRecords.ContainsKey(key) ? Mathf.Min(time, timeRecords[key]) : time;
    //    SaveTimeData();
    //}

    /// <summary>
    /// Compares time with previous results and overrides if time is less previous record
    /// </summary>
    /// <param name="index">Level build index</param>
    /// <param name="time">Level pass time</param>
    public void TryOverrideTimeData(int index, float time)
    {
        if(timeRecords.Count>index)
        {
            timeRecords[index] = Mathf.Min(time, timeRecords[index]);
        }
        else
        {
            timeRecords.Add(time);
        }

        SaveTimeData();
    }


    private void SaveTimeData()
    {
        TimeData data = new TimeData { timeRecords = this.timeRecords };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath+"/timeData.json", json);
    }

    private void LoadTimeData()
    {
        string path = Application.persistentDataPath + "/timeData.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            TimeData data = JsonUtility.FromJson<TimeData>(json);

            timeRecords = data.timeRecords;
        }
    }
}

[System.Serializable]
public class TimeData
{
    public List<float> timeRecords = new();
}

//[System.Serializable]
//public class TimeData
//{
//    public Dictionary<string, float> timeRecords = new();
//}

//[System.Serializable]
//public class TimeData
//{
//    public List<TimeDataInfo> timeRecords = new();
//}

//public struct TimeDataInfo
//{
//    public float Time { get; set; }
//    public string LevelName { get; set; } = "";
//}
