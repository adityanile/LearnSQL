using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{
    public string name;
    private Tag tag;

    public MainMenuManager mainMenuManager;
    private CategoryManager[] obj;

    private void Start()
    {
        Enum.TryParse(name, out tag);
    }
    public void OnClickCategory()
    {
        StartAnimation();
        mainMenuManager.init(tag.ToString());
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
