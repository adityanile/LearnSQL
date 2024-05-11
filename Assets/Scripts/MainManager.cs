using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private MainData mainData;

    private LocalStorageManager storageManager;

    private void Awake()
    {
        storageManager = GetComponentInChildren<LocalStorageManager>();
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


        // Test
        mainData.categories[0].levels[0].questions = new QuestionData[10];

    }

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
    class QuestionData
    {
        public string que;
        public string op1;
        public string op2;
        public string op3;
        public string op4;
        public string correct;
    }

}