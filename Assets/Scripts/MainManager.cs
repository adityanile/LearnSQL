using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class MainManager : MonoBehaviour
{
    public MainData mainData;

    private LocalStorageManager storageManager;
    private string apiUrl = "https://learn-sql.vercel.app/getMeQuestions";
    public SessionManager sessionManager;

    public GameObject loading;
    public GameObject error;

    private void Awake()
    {
        storageManager = GetComponentInChildren<LocalStorageManager>();

        loading.SetActive(false);
        error.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
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

    public void StartSessionManager(Tag tag, int level)
    {
        sessionManager.gameObject.SetActive(true);
        sessionManager.Init(tag, level);
    }

    // Writes Current session info in local Storage
    public void UpdateLocalStorage()
    {
        // Settting isOpen true for next time
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                mainData.categories[i].levels[j].isopen = true;
            }
        }

        string json = JsonUtility.ToJson(mainData,true);
        storageManager.WriteThisSessionData(json);
    }

    public int UpdateCurrentSessionScore(Tag tag, int level, int score)
    {
        int prevscore = mainData.categories[Convert.ToInt32(tag)].levels[level].prevCorrect;
        
        if (score > prevscore)
        {
            mainData.categories[Convert.ToInt32(tag)].levels[level].prevCorrect = score;
            return score - prevscore;
        }
        else
        {
            return 0;
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
        loading.SetActive(true);

        using (UnityWebRequest req = UnityWebRequest.Post(apiUrl, payload, "application/json"))
        {
            yield return req.SendWebRequest();

            loading.SetActive(false);

            if (req.result != UnityWebRequest.Result.ConnectionError)
            {
                string rcv = req.downloadHandler.text;

                // Parsing the Received JSON
                Received received = new Received();
                received = JsonUtility.FromJson<Received>(rcv);

                // Set main data to these received Data
                if (received.ststus == "success")
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
                            sessionManager.GetNextQuestion();
                        }
                    }
                    else
                    {
                        error.SetActive(true);
                        Debug.Log("No Questions Found on Server of Such Type");
                    }
                }
                else
                {
                    error.SetActive(true);
                    Debug.Log("Error Fetching Data");
                }



            }
            else
            {
                Debug.Log("Error Connecting Server");
            }

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
    public class MainData
    {
        public Category[] categories;
    }

    [System.Serializable]
    public class Category
    {
        public string tag;
        public Level[] levels;
    }

    [System.Serializable]
    public class Level
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

[System.Serializable]
public enum Tag
{
    Theory,
    Coding,
    Interview
}