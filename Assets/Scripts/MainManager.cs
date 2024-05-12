using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Networking;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private MainData mainData;

    private LocalStorageManager storageManager;
    private string apiUrl = "https://learn-sql.vercel.app/getMeQuestions";

    private void Awake()
    {
        storageManager = GetComponentInChildren<LocalStorageManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();

        FetchQuestionData("Theory", 1);
    }

    private void Init()
    {
        string data = storageManager.GetMeAllData();

        mainData = JsonUtility.FromJson<MainData>(data);

        // If 2nd Question of each category is not done then deactivate all next
        // If 2nd Done and then 3rd too Done then deactivate rest
        // If 3 Done then allow next one's

        for (int i = 0; i < 3; i++)
        {
            int count = mainData.categories[i].levels[1].prevCorrect <= 0 ? 2 :
                        mainData.categories[i].levels[2].prevCorrect <= 0 ? 3 : 6;

            for (int j = count; j < 6; j++)
            {
                mainData.categories[i].levels[j].isopen = false;
            }
        }

    }

    // This function will fetch question data from server and save it in mainData
    public void FetchQuestionData(string tag, int level)
    {
        Payload payload = new Payload();
        payload.tag = tag;
        payload.level = level;

        string json = JsonUtility.ToJson(payload);  

        StartCoroutine(GetQuestionsAPI(json));
    }

    IEnumerator GetQuestionsAPI(string payload)
    {
        using(UnityWebRequest req = UnityWebRequest.Post(apiUrl, payload, "application/json"))
        {
            yield return req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.ConnectionError)
            {
                string rcv = req.downloadHandler.text;

                // Parsing the Received JSON
                Received received = new Received();
                received = JsonUtility.FromJson<Received>(rcv);

                // Set main data to these received Data
                
                if(received.ststus == "success")
                {
                    if (received.ret.Length > 0)
                    {
                        // Parsing the received Tag
                        Tag st;
                        if (Enum.TryParse(received.ret[0].tag, out st))
                        {
                            int index = received.ret[0].level;
                            mainData.categories[Convert.ToInt32(st)].levels[index].questions = received.ret;

                            // Call function from here to show question for a session
                        }
                    }
                    else
                    {
                        Debug.Log("No Questions Found on Server of Such Type");
                    }
                }
                else
                {
                    Debug.Log("Error Fetching Data");
                }
                


            }
            else
            {
                Debug.Log("Error Connecting Server");
            }

        }
    }

    // To make payload json
    [System.Serializable]
    class Payload
    {
        public string tag;
        public int level;
    }

    [System.Serializable]
    class Received
    {
        public string ststus;
        public QuestionData[] ret;
    }

    // Class To Manager Data after fetching Question
    [System.Serializable]
    class MainData
    {
        public Category[] categories;
    }

    [System.Serializable]
    class Category
    {
        public string tag;
        public Level[] levels;
    }

    [System.Serializable]
    class Level
    {
        public int prevCorrect;
        public bool isopen = true;
        public int pointsPerQue = 0;
        public QuestionData[] questions;
    }

    [System.Serializable]
    public class QuestionData
    {
        public int id;
        public string que;
        public string op1;
        public string op2;
        public string op3;
        public string op4;
        public string correct;

        // To handle the rcv also
        public string tag;
        public int level;
    }

}

[System.Serializable]
public enum Tag
{
    Theory,
    Coding,
    Interview
}