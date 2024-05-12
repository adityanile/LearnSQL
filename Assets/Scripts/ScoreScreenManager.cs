using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScreenManager : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;

    // Also Manage The Stars Animation Here;

    public void SetScore(int score, int difference)
    {
        if(difference != 0)
        {
            scoreUI.text = "You Got " + score + "/10 questions Correct\n\n\n\n\n" +
                            "You Have Gained " + difference + " Points";
        }
        else
        {
            scoreUI.text = "You Got " + score + "/10 questions Correct";
        }
    }
}
