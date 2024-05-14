using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    private int maxQuestions = 10;

    public Transform options;
    public GameObject option;

    public TextMeshProUGUI question;
    private int currentCount;

    // Setting UP tag and Level According to user
    private Tag tag;
    private int level;

    public MainManager mainManager;

    // This list hold the correct Answers of this session
    private int correct;
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

    public GameObject submitBtn;
    public GameObject continueBtn;

    // FInal Score Screen
    public GameObject scoreScreen;
    public GameObject screen1;

    // Run this Method after Starting new Session
    public void Init(Tag tg, int lvl)
    {
        tag = tg;
        level = lvl;

        shownQuestion = new List<int>();
        optPref = new List<GameObject>();

        currentCount = 0;
        
        correct = 0;
        correctUi.text = "Correct - " + correct + "/10";

        scoreScreen.SetActive(false);
    }

    public void OnClickSubmit()
    {
        if(gotCurrentAnswer)
        {
            gotCurrentAnswer = false;

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

        OnClickContinue();
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickContinue()
    {
        Reset();

        if (++currentCount < maxQuestions)
        {
            GetNextQuestion();
        }
        else
        {
            // Now Session is completed, Update current solve score in mainData if it is greated then previous
            int difference = mainManager.UpdateCurrentSessionScore(tag, level, correct);
            
            // Also save this in local Storage for next time if player has earned any points
            if(difference > 0)
            {
                mainManager.UpdateLocalStorage();
            }

            gameObject.SetActive(false);

            // Show the Final Screen For the Session
            scoreScreen.SetActive(true);
            scoreScreen.GetComponent<ScoreScreenManager>().SetScore(correct, difference);
        }
    }

    private void Reset()
    {
        foreach(var opt in optPref)
        {
            Destroy(opt);
        }
        optPref = new List<GameObject>();

        currOptPref = null;

        currentQuestion = null;
        currentOption = "";
        gotCurrentAnswer = false;

        submitBtn.SetActive(true);
        continueBtn.SetActive(false);
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

        submitBtn.SetActive(false);
        continueBtn.SetActive(true);
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
        question.text = "Q" + (currentCount + 1).ToString() + ") " + que.que;
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
