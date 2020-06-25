using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogMan : MonoBehaviour
{
    public GameObject messageBoxPanel;

    public void OnEnable()
    {
        HelpersMan.HidePanel(messageBoxPanel);
    }

    public void ViewMessageOnScreen(string msg)
    {
        HelpersMan.ShowPanel(messageBoxPanel);
        if (messageBoxPanel)
        {
            messageBoxPanel.GetComponentsInChildren<Text>()[0].text = msg;
        }
    }

    public void CloseMessageBoxBtn()
    {
        HelpersMan.HidePanel(messageBoxPanel);
    }
}
