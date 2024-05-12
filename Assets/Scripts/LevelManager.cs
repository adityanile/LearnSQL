using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI index;
    public TextMeshProUGUI prevPoints;
    public GameObject mainMenu;
    public GameObject screen1;

    [SerializeField]
    private int id;
    private Tag tag;

    private MainManager mainManager;

    private void Awake()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        mainMenu = GameObject.Find("MainMenu");
        screen1 = GameObject.Find("Screen1");
    }

    public void SetLevelData(int i,Tag _tag, int prevPts)
    {
        index.text = "Level - " + (i + 1).ToString();
        prevPoints.text = "(" + prevPts.ToString() + "/10)";

        id = i;
        tag = _tag;
    }

    public void OnClickLevel()
    {
        mainMenu.SetActive(false);
        screen1.SetActive(false);
        
        mainManager.FetchQuestionData(tag.ToString(), id);
        mainManager.StartSessionManager(tag, id);
    }

    public void DeactivateLevel()
    {
        Button btn = GetComponent<Button>();
        Image img = GetComponent<Image>();

        btn.enabled = false;
        img.color = new Color(0.2311024f, 0.2311024f, 0.2578616f);
    }

}
