using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalStorageManager : MonoBehaviour
{
    private string jsonPath;
    private string initialString = "";

    void Awake()
    {
        jsonPath = Application.persistentDataPath + "/mainData.json";

        if(!File.Exists(jsonPath))
        {
           File.WriteAllText(jsonPath, initialString);
           Debug.Log("File Created");
        }
    }

    public void WriteThisSessionData(string json)
    {
        File.WriteAllText(jsonPath, json);
    }

    public string GetMeAllData()
    {
        return File.ReadAllText(jsonPath);
    }
}
