using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HelpersMan : MonoBehaviour
{
    public static void ShowPanel(GameObject panel)
    {
        if (panel)
        {
            panel.SetActive(true);
        }
    }
    public static void HidePanel(GameObject panel)
    {
        if (panel)
        {
            panel.SetActive(false);
        }
    }
    public static bool CheckEmpty(InputField inputField)
    {
        if (inputField)
        {
            if (inputField.text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log("Assign input fields ... ");
            return false;
        }
    }
    public static bool CheckEmpty(Dropdown dropDown)
    {
        if (dropDown)
        {
            if (dropDown.options[dropDown.value].text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public static bool CheckNumeric(string str)
    {
        try
        {
            double.Parse(str);
            return true;
        }
        catch
        {
            Debug.Log("please enter a number ... ");
            return false;
        }
    }
    public static void DestroyGameObjects(List<GameObject> list)
    {
        foreach (GameObject obj in list)
        {
            Destroy(obj);
        }
        list.Clear();
    }
    public static void HighlightPanel(GameObject panel)
    {
        if (panel)
        {
            Color color;
            color = panel.GetComponent<Image>().color;
            color.a = 1.0f;
            panel.GetComponent<Image>().color = color;
        }
    }
    public static void DeHighlightPanel(GameObject panel)
    {
        if (panel)
        {
            Color color;
            color = panel.GetComponent<Image>().color;
            color.a = 0.0f;
            panel.GetComponent<Image>().color = color;
        }
    }
    public static void ResetTexture2D(ref Texture2D tex)
    {
        if (tex == null)
        {
            tex = new Texture2D(2, 2);
            tex.Apply();
        }
    }
    public static void ClearDDOptions(Dropdown dd)
    {
        if (dd)
        {
            dd.options.Clear();
            dd.captionText.text = "";
            dd.itemText.text = "";
        }
    }
    public static string ToEnglishNumber(string input)
    {
        string EnglishNumbers = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsDigit(input[i]))
            {
                EnglishNumbers += char.GetNumericValue(input, i);
            }
            else
            {
                EnglishNumbers += input[i].ToString();
            }
        }
        return EnglishNumbers;
    }
}
