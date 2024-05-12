using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    private SessionManager sessionManager;
    private Image bg;
    private TextMeshProUGUI text;

    private Image submitBtn;
    private Color original;

    private bool youclicked = false;
    public bool isCorrect = false;

    // TO show Write and Wrong tick
    public GameObject correct;
    public GameObject wrong;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = GameObject.Find("SessionManager").GetComponent<SessionManager>();
        submitBtn = GameObject.Find("Submit").GetComponent<Image>();

        bg = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowCorrectMarking()
    {
        if (isCorrect)
        {
            correct.SetActive(true);
        }
    }
    public void ShowWrongMarking()
    {
        if (!isCorrect)
        {
            wrong.SetActive(true);
        }
    }

    public void OnClick()
    {
        if (!youclicked)
        {
            youclicked = true;

            if (!sessionManager.gotCurrentAnswer)
            {
                bg.color = Color.gray;
                sessionManager.SetOption(text.text);
                sessionManager.gotCurrentAnswer = true;

                // Making submit button glow
                original = submitBtn.color;
                submitBtn.color = Color.yellow;

                // Other than current button disable other buttons
                sessionManager.DisableOtherOptions(this.gameObject);
            }
        }
        else
        {
            youclicked = false;

            sessionManager.gotCurrentAnswer = false;
            bg.color = Color.white;
            sessionManager.SetOption("");

            submitBtn.color = original;

            sessionManager.EnableOtherOptions(this.gameObject);
        }
    }
}
