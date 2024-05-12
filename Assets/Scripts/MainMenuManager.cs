using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private Tag tag;

    public TextMeshProUGUI category;
    public TextMeshProUGUI points;

    [SerializeField]
    private MainManager mainManager;

    public GameObject levelPref;
    public Transform levelParent;

    private List<GameObject> levelList;

    public void init(string _tag)
    {
        // Using The Tag That we got from initial Part
        category.text = _tag;
        Enum.TryParse(_tag, out tag);

        levelList = new List<GameObject>();
 
        // Instaiate Levels
        for(int i=0; i < 6; i++)
        {
            int prevPoints = mainManager.mainData.categories[Convert.ToInt32(tag)].levels[i].prevCorrect;
            bool state = mainManager.mainData.categories[Convert.ToInt32(tag)].levels[i].isopen;

            GameObject inst = Instantiate(levelPref, levelParent);
            inst.GetComponent<LevelManager>().SetLevelData(i, tag, prevPoints);

            if (!state)
            {
                inst.GetComponent<LevelManager>().DeactivateLevel();
            }

            levelList.Add(inst);
        }

        // Setting Points Initially
        points.text = "Points : " + CalculatePoints().ToString();
    }

    int CalculatePoints()
    {
        int points = 0;

        foreach(var lvl in mainManager.mainData.categories[Convert.ToInt32(tag)].levels)
        {
            points += lvl.prevCorrect;
        }
        return points;
    }





}
