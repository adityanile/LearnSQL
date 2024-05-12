using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    public Transform options;
    public GameObject option;

    public TextMeshProUGUI question;
    private int currentCount = 0;

    // Setting UP tag and Level According to user
    private Tag tag = Tag.Theory;
    private int level = 1;

    public MainManager mainManager;

    // This list hold the correct Answers of this session
    private int correct = 0;
    private List<int> shownQuestion;
    public TextMeshProUGUI correctUi;

    // Current Data
    [SerializeField]
    private QuestionData currentQuestion;
    [SerializeField]
    private string currentOption = "";
    public bool gotCurrentAnswer = false;
    private GameObject currOptPref;

    private List<GameObject> optPref;

    // MainUI Ticks Manager
    public GameObject correctTick;
    public GameObject wrongTick;

    private void Start()
    {
        shownQuestion = new List<int>();
        optPref = new List<GameObject>();
    }

    public void OnClickSubmit()
    {
        if(gotCurrentAnswer)
        {
            if (ValidateAnswer())
            {
                UpdateCorrectUI();
                StartCoroutine(ShowCorrectTick());
            }
            else
            {
                StartCoroutine(ShowWrongTick());
            }
        }
    }
    IEnumerator ShowCorrectTick()
    {
        correctTick.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        correctTick.SetActive(false);

        // Now Show Next Question by resetting all the Necessary Things
    }
    IEnumerator ShowWrongTick()
    {
        wrongTick.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        
        wrongTick.SetActive(false);

        // After Showing wrong tick then show correct Option
        // Logic is selected option is wrong then show wrong tick at its end
        // along with this also show the correct option present
        currOptPref.GetComponent<OptionManager>().ShowWrongMarking();

        foreach (var obj in optPref)
        {
            OptionManager opt = obj.GetComponent<OptionManager>();
            opt.ShowCorrectMarking();
        }
    }

    public void UpdateCorrectUI()
    {
        correct++;
        correctUi.text = "Correct - " + correct + "/10";
    }
    public void GetNextQuestion()
    {
        int index = GetQuestionsIndex();
        QuestionData que = mainManager.mainData.categories[Convert.ToInt32(tag)].levels[level].questions[index];
        currentQuestion = que;
        SetData(que);
    }

    public int GetQuestionsIndex()
    {
        int index = ChooseRandomIndex();

        if (shownQuestion.Count == 0)
        {
            shownQuestion.Add(index);
        }
        else
        {
            while (shownQuestion.IndexOf(index) != -1)
            {
                index = ChooseRandomIndex();
            }
            shownQuestion.Add(index);
        }
        return index;
    }

    bool ValidateAnswer()
    {
        if (currentOption.Equals(currentQuestion.correct))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetOption(string opt)
    {
        currentOption = opt;
    }
    int ChooseRandomIndex()
    {
        return UnityEngine.Random.Range(0, mainManager.mainData.categories[Convert.ToInt32(tag)].levels[level].questions.Length);
    }

    public void SetData(QuestionData que)
    {
        question.text = "Q" + que.id + ") " + que.que;
        string[] opt = { que.op1, que.op2, que.op3, que.op4 };

        for (int i = 0; i < 4; i++)
        {
            GameObject inst = Instantiate(option, options);
            TextMeshProUGUI txt = inst.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = opt[i];

            if(que.correct == opt[i])
            {
                inst.GetComponent<OptionManager>().isCorrect = true;
            }

            optPref.Add(inst);
        }
    }

    public void DisableOtherOptions(GameObject current)
    {
        currOptPref = current;

        foreach(var obj in optPref)
        {
            if(obj != current)
            {
                obj.GetComponent<Button>().enabled = false;
            }
        }
    }
    public void EnableOtherOptions(GameObject current)
    {
        currOptPref = current;

        foreach (var obj in optPref)
        {
            if (obj != current)
            {
                obj.GetComponent<Button>().enabled = true;
            }
        }
    }
}
