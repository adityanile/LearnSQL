using TMPro;
using UnityEngine;

public class ScoreScreenManager : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public GameObject[] stars;

    public void SetScore(int score, int difference)
    {
        if (difference != 0)
        {
            scoreUI.text = "You Got " + score + "/10 questions Correct\n\n\n\n\n" +
                            "You Have Gained " + difference + " Points";
        }
        else
        {
            scoreUI.text = "You Got " + score + "/10 questions Correct";
        }

        ActiveStars(score);
    }

    // Activating Stars based on score
    public void ActiveStars(int score)
    {
        if (score == 10)
        {
            SetStar(3);
        }
        else if (score >= 5 && score < 10)
        {
            SetStar(2);
        }
        else
        {
            SetStar(1);
        }
    }

    private void SetStar(int n)
    {
        stars[n-1].SetActive(true);
    }
}
