using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public string email;
    public string username;
    public string userId;
    public string userColorMode;
    public string isFirstTime;
    public string[] userCategories = new string[4];
    public string userLanguage;

    public User()
    {
        email = "";
    }
    public User(string email)
    {
        this.email = email;
    }
    public User(string email, string username, string userId, string userColorMode)
    {
        this.email = email;
        this.username = username;
        this.userId = userId;
        this.userColorMode = userColorMode;
    }
    public User(string email, string username, string userId, string userColorMode, string isFirstTime)
    {
        this.email = email;
        this.username = username;
        this.userId = userId;
        this.userColorMode = userColorMode;
        this.isFirstTime = isFirstTime;
    }
    public User(string email, string username, string userId, string userColorMode, string userLanguage, string[] userCategories)
    {
        this.email = email;
        this.username = username;
        this.userId = userId;
        this.userColorMode = userColorMode;
        this.userLanguage = userLanguage;
        for (int i = 0; i < userCategories.Length; i++)
        {
            this.userCategories[i] = userCategories[i];
        }
    }
}
