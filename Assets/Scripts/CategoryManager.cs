using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    public string name;
    private Tag tag;

    public MainMenuManager mainMenuManager;
    private CategoryManager[] obj;

    public bool clicked = false;

    private void Start()
    {
        Enum.TryParse(name, out tag);
    }
    public void OnClickCategory()
    {
        if (!clicked)
        {
            // Set Clicked Active for all Category Objects
            SetClickedActive();

            StartAnimation();
            mainMenuManager.init(tag.ToString());
        }
        else
        {
            // Go Back to Main Menu
            clicked = false;
            SceneManager.LoadScene(0);
        }
    }

    public void SetClickedActive()
    {
        obj = GameObject.FindObjectsOfType<CategoryManager>();

        foreach (var o in obj)
        {
            o.clicked = true;
        }
    }

    void StartAnimation()
    {
        obj = GameObject.FindObjectsOfType<CategoryManager>();
        
        foreach(var o in obj)
        {
            o.gameObject.GetComponent<Animation>().enabled = true;
        }
        mainMenuManager.gameObject.GetComponent<Animation>().enabled = true;
    }
}
