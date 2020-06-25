using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class CategoriesToggleGroupMan : MonoBehaviour
{
    public Toggle furnitureToggle;
    public Toggle appliancesToggle;
    public Dropdown userLanguageDD;

    public List<Toggle> togglesGroupList = new List<Toggle>();

    public bool isTogglesLoaded = false;
    public bool isLanguageChanged = false;

    public Text label;

    int count = 0;

    public void OnUserLanguageDDValueChangedCallback()
    {
        //Globals.userLanguage = userLanguageDD.options[userLanguageDD.value].text.ToString();
        //Globals.currentUser.userLanguage = Globals.userLanguage;
        //DBMan.UpdateUserProfile(Globals.currentUser, Globals.userId);
        //isLanguageChanged = true;
    }

    public void OnFurnitureToggleValueChangedCallback()
    {
        //if (furnitureToggle)
        //{
        //    if (furnitureToggle.isOn)
        //    {
        //        count++;
        //        if (count > 4)
        //        {
        //            furnitureToggle.isOn = false;
        //            count--;
        //            return;
        //        }
        //        UpdateUserSelectedCategoriesCallback();
        //    }
        //}
        //UpdateUserSelectedCategoriesCallback();
    }

    public void OnAppliancesToggleValueChangedCallback()
    {
        //if (appliancesToggle)
        //{
        //    if (appliancesToggle.isOn)
        //    {
        //        count++;
        //        if (count > 4)
        //        {
        //            appliancesToggle.isOn = false;
        //            count--;
        //            return;
        //        }
        //        UpdateUserSelectedCategoriesCallback();
        //    }
        //}
        //UpdateUserSelectedCategoriesCallback();
    }

    public void UpdateUserSelectedCategoriesCallback()
    {
        //LoadToggleStates();
        //count = 0;
        UIMan.showLoadingPanel = true;
        togglesGroupList.Clear();
        foreach (Toggle toggle in GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                togglesGroupList.Add(toggle);
                //count++;
            }
        }
        Globals.currentUser.userCategories = new string[4];
        for (int i = 0; i < togglesGroupList.Count; i++)
        {
            Globals.currentUser.userCategories[i] = togglesGroupList[i].gameObject.GetComponentInChildren<Text>().text.ToString();
        }
        Globals.userLanguage = userLanguageDD.options[userLanguageDD.value].text.ToString();
        Globals.currentUser.userLanguage = Globals.userLanguage;
        isLanguageChanged = true;
        string json = JsonUtility.ToJson(Globals.currentUser);
        FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(Globals.userId).Child("profile").SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (Globals.userLanguage == "AR")
                {
                    UIMan.ViewMessageOnScreen(ArabicSupport.ArabicFixer.Fix("يرجى اعادة التسجيل ..."));
                    AuthMan.LogOut();
                }
                else
                {
                    UIMan.ViewMessageOnScreen("Please login again ... ");
                    AuthMan.LogOut();
                }
            }
            UIMan.showLoadingPanel = false;
        });
        //DBMan.UpdateUserProfile(Globals.currentUser, Globals.userId);
        //LoadToggleStates();
    }

    void LoadToggleStates()
    {
        //if (furnitureToggle)
        //{
        //    furnitureToggle.isOn = false;
        //}
        //if (appliancesToggle)
        //{
        //    appliancesToggle.isOn = false;
        //}
        Debug.Log("Loading Toggles States ... ");
        for (int i = 0; i < Globals.currentUser.userCategories.Length; i++)
        {
            Debug.Log("Checking Category: " + Globals.currentUser.userCategories[i]);
            if (Globals.userCategories[i] == "Furniture" || Globals.userCategories[i] == ArabicSupport.ArabicFixer.Fix("اثاث"))
            {
                if (furnitureToggle)
                {
                    furnitureToggle.isOn = true;
                    Debug.Log("Furniture Checked ... ");
                    //UpdateUserSelectedCategoriesCallback();
                }
            }
            else if (Globals.userCategories[i] == "Appliances" || Globals.userCategories[i] == ArabicSupport.ArabicFixer.Fix("اجهزة منزلية"))
            {
                if (appliancesToggle)
                {
                    appliancesToggle.isOn = true;
                    Debug.Log("Appliances Checked ... ");
                    //UpdateUserSelectedCategoriesCallback();
                }
            }
        }
        isTogglesLoaded = true;
    }

    void OnEnable()
    {
        if (Globals.userLanguage == "AR")
        {
            if (label)
            {
                label.text = ArabicSupport.ArabicFixer.Fix("اختر 4 فقط");
            }
            if (furnitureToggle)
            {
                furnitureToggle.gameObject.GetComponentInChildren<Text>().text = ArabicSupport.ArabicFixer.Fix("اثاث");
            }
            if (appliancesToggle)
            {
                appliancesToggle.gameObject.GetComponentInChildren<Text>().text = ArabicSupport.ArabicFixer.Fix("اجهزة منزلية");
            }
            if (userLanguageDD)
            {
                userLanguageDD.value = 1; 
            }
        }
        else
        {
            if (label)
            {
                label.text = "Choose 4 only";
            }
            if (furnitureToggle)
            {
                furnitureToggle.gameObject.GetComponentInChildren<Text>().text = "Furniture";
            }
            if (appliancesToggle)
            {
                appliancesToggle.gameObject.GetComponentInChildren<Text>().text = "Appliances";
            }
            if (userLanguageDD)
            {
                userLanguageDD.value = 0;
            }
        }
    }

    void Update()
    {
        if (DBMan.loadUserCategories)
        {
            LoadToggleStates();
            DBMan.loadUserCategories = false;
        }
    }
}
