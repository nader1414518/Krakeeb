using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase.Unity.Editor;
using Proyecto26;
using Firebase;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using Firebase.Storage;
using System.Threading.Tasks;
using System;

public class DBMan : MonoBehaviour
{
    public static Text logTxt;

    static float logTxtTimer = 0;

    public static bool isItemsRetrieved;
    public static bool loadDashboard;
    public static bool refreshDashboard;
    public static bool cartRetrieved;
    public static bool snapshotsLoaded;
    public static bool userInfoLoaded;
    public static bool loadUserCategories;

    public const string v = "userCategories";

    //static bool isItemsStored = false;
    public static Texture2D slotTex;
    public static byte[] snapBytes;

    // recursively yield all children of json
    private static IEnumerable<JToken> AllChildren(JToken json)
    {
        foreach (var c in json.Children())
        {
            yield return c;
            foreach (var cc in AllChildren(c))
            {
                yield return cc;
            }
        }
    }

    #region Save 
    public static void SaveItemImage(Item item)
    {
        // Create a reference for the image
        StorageReference imageReference = FirebaseStorage.DefaultInstance.GetReference("images").Child("items").Child(item.itemId);
        // Upload the image to the path (images/items/(item.itemName))
        imageReference.PutFileAsync(item.itemImagePath.ToString()).ContinueWith((Task<StorageMetadata> task) =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                // Error happened 
                Debug.Log(task.Exception.ToString());
                ViewMessageOnScreen(task.Exception.ToString());
            }
            else
            {
                // Metadata contains file metadata such as file size, content type and downloadURL
                StorageMetadata metadata = task.Result;
                Debug.Log("Finished Uploading .... ");
                // Gets the download url
                imageReference.GetDownloadUrlAsync().ContinueWith((url) =>
                {
                    if (url.IsCompleted)
                    {
                        Debug.Log("Image download url: " + url.Result.AbsoluteUri.ToString());
                        FirebaseDatabase.DefaultInstance.GetReference("items").Child(item.itemId).Child("itemImageDownloadUrl").SetValueAsync(url.Result.AbsoluteUri.ToString());
                        //FirebaseDatabase.DefaultInstance.GetReference("users").Child(Globals.userId).Child("cart").Child(item.itemName).Child("itemImageDownloadUrl").SetValueAsync(url.Result.AbsoluteUri.ToString());
                        item.itemImageDownloadUrl = url.Result.AbsoluteUri.ToString();
                        snapshotsLoaded = true;
                        ViewMessageOnScreen("Uploaded Image ... ");
                        //AddItemToUserCart(item, Globals.userId.ToString());
                    }
                    else
                    {
                        Debug.Log("Image not uploaded ... ");
                        ViewMessageOnScreen("Image not uploaded check internet connection ... ");
                    }
                });
            }
        });
    }
    public static void AddItemToDatabase(Item item)
    {
        UIMan.showLoadingPanel = true;
        item.itemSellerEmail = Globals.email;
        item.itemSellerUsername = Globals.username;
        RestClient.Put("https://krakeebv1.firebaseio.com/items/" + item.itemId.ToString() + ".json", item);
        if (item.itemImagePath != "")
        {
            SaveItemImage(item);
        }
        Debug.Log("Added item to database ... ");
        if (Globals.userLanguage == "AR")
        {
            ViewMessageOnScreen(ArabicSupport.ArabicFixer.Fix("تم عرض السلعة للبيع ..."));
        }
        else
        {
            ViewMessageOnScreen("Added item to database ... ");
        }
        UIMan.showLoadingPanel = false;
        // Update the dashboard
        RetrieveItemsFromDatabase();
    }
    public static void AddItemToUserCart(Item item, string userId)
    {
        
        //FirebaseDatabase.DefaultInstance.GetReference("users").Child(Globals.username.Replace(".com", "").ToString()).Child("items").SetRawJsonValueAsync(JsonUtility.ToJson(item).ToString());
        RestClient.Put("https://krakeebv1.firebaseio.com/users/" + userId.Replace(".com", "").ToString() + "/cart/" + item.itemId.ToString() + ".json", item);
        if (Globals.userLanguage == "AR")
        {
            ViewMessageOnScreen(ArabicSupport.ArabicFixer.Fix("تمت اضافة هذه السلعة الى العربة ..."));
        }
        else
        {
            ViewMessageOnScreen("Added to your Cart ... ");
        }
    }
    public static void PurchaseItem(Item item, string userId)
    {
        RestClient.Put("https://krakeebv1.firebaseio.com/users/" + userId.Replace(".com", "").ToString() + "/cart/" + item.itemId.ToString() + ".json", item);
        RestClient.Put("https://krakeebv1.firebaseio.com/items/" + item.itemId.ToString() + ".json", item);
        RemoveItemFromCart(item, userId);
        if (Globals.userLanguage == "AR")
        {
            ViewMessageOnScreen(ArabicSupport.ArabicFixer.Fix("تم تحديث بيانات السلعة ... "));
        }
        else
        {
            ViewMessageOnScreen("Updated Item Details ... ");
        }
    }
    public static void UpdateUserProfile(User user, string userId)
    {
        UIMan.showLoadingPanel = true;
        user.isFirstTime = "no";
        RestClient.Put("https://krakeebv1.firebaseio.com/users/" + userId.Replace(".com", "").ToString() + "/profile.json", user);
        if (Globals.userLanguage == "AR")
        {
            ViewMessageOnScreen(ArabicSupport.ArabicFixer.Fix("تم تحديث بياناتك ...\nمن فضلك انتظر لحظه ..."));
        }
        else
        {
            ViewMessageOnScreen("Updated User Settings ... \nPlease wait a second ... ");
        }
        UIMan.showLoadingPanel = false;
    }
    public static void InitializeUserProfile(User user, string userId)
    {
        user.isFirstTime = "no";
        RestClient.Put("https://krakeebv1.firebaseio.com/users/" + userId.Replace(".com", "").ToString() + "/profile.json", user);
    }
    #endregion

    #region Load
    public static void LoadItemImage(Item item, GameObject slotSnap)
    {
        // Create a reference for the image
        StorageReference imageReference = FirebaseStorage.DefaultInstance.GetReference("images").Child("items").Child(item.itemId);
        // Load the image bytes from url 
        const long maxAllowedSize = 1 * 1024 * 1024;
        imageReference.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("Failed to load images: " + task.Exception.ToString());
                ViewMessageOnScreen("Failed to load images: " + task.Exception.ToString());
            }
            else
            {
                byte[] imageContents = task.Result;
                if (slotSnap)
                {
                    Debug.Log("Retrieved image bytes ... ");
                    Debug.Log(imageContents.Length);
                    //slotTex = slotSnap.GetComponent<RawImage>().texture;
                    snapBytes = imageContents;
                    //slotTex = tex;
                    snapshotsLoaded = true;
                }
            }
        });
    }
    public static void LoadUserInfo()
    {
        UIMan.showLoadingPanel = true;
        if (Globals.userId != "")
        {
            FirebaseDatabase.DefaultInstance.GetReference("users").Child(Globals.userId).Child("profile").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    //foreach (DataSnapshot user in snapshot.Children)
                    //{
                    //    IDictionary dictUser = (IDictionary)user.Value;
                    //    Globals.currentUser = new User(
                    //        dictUser["email"].ToString(),
                    //        dictUser["username"].ToString(),
                    //        dictUser["userId"].ToString(),
                    //        dictUser["userColorMode"].ToString()
                    //    );
                    //    Debug.Log("Retrieved User Info ... ");
                    //}
                    int c = 0;
                    //string[] arr = new string[4];
                    foreach (DataSnapshot child in snapshot.Child("userCategories").Children)
                    {
                        Debug.Log("Retrieved Key: " + child.Key.ToString() + " Value: " + child.Value.ToString());
                        Globals.userCategories[c] = child.Value.ToString();
                        Debug.Log("User Category: " + Globals.userCategories[c]);
                        c++;
                    }
                    IDictionary dictUser = (IDictionary)snapshot.Value;
                    Globals.currentUser = new User(
                        dictUser["email"].ToString(),
                        dictUser["username"].ToString(),
                        dictUser["userId"].ToString(),
                        dictUser["userColorMode"].ToString(),
                        dictUser["userLanguage"].ToString(),
                        Globals.userCategories
                    );
                    //Globals.currentUser.userLanguage = dictUser["userLanguage"].ToString();
                    Debug.Log("User Language: " + Globals.currentUser.userLanguage);
                    Globals.userLanguage = Globals.currentUser.userLanguage;
                    //Globals.currentUser.userCategories = (string[])dictUser["userCategories"];
                    Debug.Log("Retrieved User Info ... ");
                    userInfoLoaded = true;
                    loadDashboard = true;
                    loadUserCategories = true;
                }
                UIMan.showLoadingPanel = false;
            });
        }
    }
    public static void RetrieveItemsFromDatabase()
    {
        UIMan.showLoadingPanel = true;
        Globals.dashboardItems.Clear();
        FirebaseDatabase.DefaultInstance.GetReference("items").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot item in snapshot.Children)
                {
                    IDictionary dictItem = (IDictionary)item.Value;
                Globals.dashboardItems.Add(new Item(dictItem["itemName"].ToString(),
                    dictItem["itemType"].ToString(),
                    dictItem["itemDescription"].ToString(),
                    dictItem["itemCategory"].ToString(),
                    double.Parse(dictItem["itemPrice"].ToString()),
                    dictItem["itemSeller"].ToString(),
                    dictItem["itemPostDate"].ToString(),
                    dictItem["itemStatus"].ToString(),
                    int.Parse(dictItem["itemQuantity"].ToString()),
                    dictItem["itemImagePath"].ToString(),
                    dictItem["itemSellerEmail"].ToString(),
                    dictItem["itemSellerUsername"].ToString()));
                    //Debug.Log("Retrieved Item: " + dictItem["itemName"].ToString());
                }
                loadDashboard = true;
                refreshDashboard = true;
                isItemsRetrieved = true;
            }
            UIMan.showLoadingPanel = false;
        });
    }
    public static void RetrieveItemsFromCart(string userId)
    {
        Globals.cartItems.Clear();
        FirebaseDatabase.DefaultInstance.GetReference("users/"+userId+"/cart").GetValueAsync().ContinueWith(task =>
        {
            //if (task.IsFaulted)
            //{
            //    Debug.Log("Error retrieving data from DB ... ");
            //    LogScreenMessages("Error retrieving data from DB ... ");
            //    //isItemsRetrieved = false;
            //}
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot item in snapshot.Children)
                {
                    IDictionary dictItem = (IDictionary)item.Value;
                    Globals.cartItems.Add(new Item(dictItem["itemName"].ToString(),
                        dictItem["itemType"].ToString(),
                        dictItem["itemDescription"].ToString(),
                        dictItem["itemCategory"].ToString(),
                        double.Parse(dictItem["itemPrice"].ToString()),
                        dictItem["itemSeller"].ToString(),
                        dictItem["itemPostDate"].ToString(),
                        dictItem["itemStatus"].ToString(),
                        int.Parse(dictItem["itemQuantity"].ToString()),
                        dictItem["itemImagePath"].ToString(),
                        dictItem["itemSellerEmail"].ToString(),
                        dictItem["itemSellerUsername"].ToString()));
                }
                cartRetrieved = true;
            }
            UIMan.showLoadingPanel = false;
        });
    }
    public static void RetrieveFromDatabase(User user)
    {
        RestClient.Get<User>("https://recycleprojectv1.firebaseio.com/" + user.email + ".json").Then(response =>
        {
            Globals.currentUser = response;
            //Debug.Log("Username: " + Globals.user.username + "   Score: " + Globals.user.email);
        });
    }
    #endregion

    #region Remove
    public static void RemoveItemFromCart(Item item, string userId)
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").Child(userId.ToString()).Child("cart").Child(item.itemId.ToString()).RemoveValueAsync();
        Debug.Log("Removed Item from cart ... ");
    }
    public static void RemoveItemFromAllCarts(Item item)
    {
        FirebaseDatabase.DefaultInstance.GetReference("cart").Child(item.itemId.ToString()).RemoveValueAsync();
        Debug.Log("Removed from all users' cart ... ");
    }
    #endregion

    #region TestArea
    // For testing 
    public static void PostToDatabase(User user)
    {
        // Make a post request to google firebase database
        //RestClient.Post("https://recycleprojectv1.firebaseio.com/.json", user);
        // Make a put request to google firebase database
        RestClient.Put("https://krakeebv1.firebaseio.com/" + user.email + ".json", user);
    }
    #endregion

    #region MainFunctions
    void Start()
    {
        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://krakeebv1.firebaseio.com/");

        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        //slotTex = new Texture2D(128, 128);
    }
    void Awake()
    {
        //try
        //{
        //    slotTex = new Texture2D(2, 2);
        //}
        //catch (Exception e)
        //{
        //    Debug.Log("In Awake: " + e.ToString());
        //}
    }
    void OnEnable()
    {
        logTxt = GameObject.FindGameObjectWithTag("DBLogTxt").GetComponent<Text>();
    }
    void Update()
    {
        //if (isItemsRetrieved)
        //{
        //    for (int i = 0; i < Globals.dashboardItems.Count; i++)
        //    {
        //        Debug.Log("Retrieved Item: " + Globals.dashboardItems[i].itemName + " For User: " + Globals.dashboardItems[i].email);
        //    }
        //    isItemsRetrieved = false;
        //}

        if (logTxtTimer <= 0.0f)
        {
            logTxtTimer = 0;
            if (logTxt)
            {
                logTxt.text = "";
            }
        }
        else
        {
            logTxtTimer -= Time.deltaTime;
        }
    }
    #endregion

    #region Auxilaries
    static void StoreItems(List<string> usernames, List<string> itemNames, List<string> itemCategories, List<string> itemPrices, List<string> isItemsSold, List<string> itemsDate)
    {
        //for (int i = 0; i < itemNames.Count; i++)
        //{
        //    Item item = new Item(usernames[i], itemNames[i], itemCategories[i], float.Parse(itemPrices[i]), bool.Parse(isItemsSold[i]), itemsDate[i]);
        //    Globals.dashboardItems.Add(item);
        //}
    }
    static void LogScreenMessages(string msg)
    {
        if (logTxt)
        {
            logTxt.text = msg;
            logTxtTimer = 5.0f;
        }
    }
    public static void ViewMessageOnScreen(string msg)
    {
        MessageBoxMan.Open(msg);
    }
    #endregion
}
