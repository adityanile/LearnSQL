using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalStorageManager : MonoBehaviour
{
    private string jsonPath;
    private string initialString = "{\r\n  \"categories\": [\r\n    {\r\n      \"tag\": \"Theory\",\r\n      \"levels\": [\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"tag\": \"Coding\",\r\n      \"levels\": [\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"tag\": \"Interview\",\r\n      \"levels\": [\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        },\r\n        {\r\n          \"prevCorrect\": 0,\r\n          \"isopen\": true,\r\n          \"pointsPerQue\": 0,\r\n          \"questions\": []\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}\r\n";

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
