using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK;
using JMRSDK.InputModule;
using UnityEngine.UI;
using JMRSDK.Toolkit;

public class ScrollButtonHandler : MonoBehaviour, IFocusable
{
    public GameObject[] scrollButtons;

    public void OnFocusEnter()
    {
        if(scrollButtons.Length != 0)
            foreach(GameObject scrollButton in scrollButtons)
                scrollButton.SetActive(true);
    }
    public void OnFocusExit()
    {
        if (scrollButtons.Length != 0)
            foreach (GameObject scrollButton in scrollButtons)
                scrollButton.SetActive(false);
    }
}
