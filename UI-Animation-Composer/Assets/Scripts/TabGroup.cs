using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<GameObject> objectsToSwap;
    private TabButton selectedTab;


    public void Subscribe(TabButton button)
    {
        if (tabButtons == null) 
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab != null || button != selectedTab)
        {
            button.GetComponent<Image>().color = Color.white;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.GetComponent<Image>().color = Color.green;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }
    public void ResetTabs()
    {
        foreach( TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            button.GetComponent<Image>().color = Color.grey;
        }
    }

}
