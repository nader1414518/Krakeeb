using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Google;
using System.Threading.Tasks;
using System.Linq;
using System;
using Facebook.Unity;


public class Globals : MonoBehaviour
{
    public static string username = "";
    public static string email = "";
    public static string userId = "";
    public static bool isLoggedIn = false;
    public static bool loggedInWithGoogle = false;
    public static bool loggedInWithFaceBook = false;
    public static bool loggedInWithEmail = false;
    public static User currentUser;
    public static string userLanguage;
    public static List<Item> dashboardItems = new List<Item>();
    public static List<Item> cartItems = new List<Item>();
    public static List<Item> searchItems = new List<Item>();
    public static List<GameObject> itemViewSlots = new List<GameObject>();
    public static List<GameObject> cartItemViewSlots = new List<GameObject>();
    public static List<GameObject> snapViewSlots = new List<GameObject>();
    public static List<GameObject> searchViewSlots = new List<GameObject>();
    public static GameObject messageBoxPrefab;
    public static GameObject messagePanel;
    public static GameObject messagePanelAuthMan;
    public static GameObject canvasObject;
    public static string[] userCategories = new string[4];
    //public static Color normalColor = new Color(0.01744394f, 0.2743764f, 0.5283019f, 1.0f);
    //public static Color normalColor = new Color(0.0f, 0.454f, 0.518f, 1.0f);
    public static Color normalColor = Color.white;
    public static Color darkModeColor = new Color(0.1791118f, 0.2176121f, 0.2358491f, 1.0f);
    public static string[] addedItemCategoriesArrayEnglish = new string[2] { "Furniture", "Appliances" };
    public static string[] addedItemCategoriesArrayArabic = new string[2] { "اثاث", "اجهزة منزلية" };
    public static string[] furnitureTypesArrayEnglish = new string[8] { "Cupboard", "Neesh", "Bed", "Table", "Bedroom", "Office Furniture", "Sofa", "Chair" };
    public static string[] furnitureTypesArrayArabic = new string[9] { "خزانة ملابس(دولاب)", "نيش", "سرائر", "طاولات", "غرف نوم", "اثاث مكتبي", "كنب", "كراسي و مقاعد", "ركنة" };
    //public static List<Dropdown.OptionData> furnitureTypesEnglish = new List<Dropdown.OptionData>();
    public static string[] appliancesTypesArrayEnglish = new string[7] { "Refrigerator", "Washer", "Broom", "Oven", "Cooker", "TV", "Fan" };
    public static string[] appliancesTypesArrayArabic = new string[6] { "ثلاجات", "غسالات", "مكانس كهربائية", "افران و بوتاجازات", "تلفزيونات", "مراوح" };
    //public static List<Dropdown.OptionData> appliancesTypesEnglish = new List<Dropdown.OptionData>();
    
    //void Start()
    //{
    //    //messagePanel = GameObject.FindGameObjectWithTag("MessageBox");
    //}

    void Start()
    {
        //for (int i = 0; i < furnitureTypesArrayEnglish.Length; i++)
        //{
        //    Dropdown.OptionData data = new Dropdown.OptionData(furnitureTypesArrayEnglish[i]);
        //    furnitureTypesEnglish.Add(data);
        //}
        //for (int i = 0; i < appliancesTypesArrayEnglish.Length; i++)
        //{
        //    Dropdown.OptionData data = new Dropdown.OptionData(appliancesTypesArrayEnglish[i]);
        //    appliancesTypesEnglish.Add(data);
        //}
    }
}

public class AuthMan : MonoBehaviour
{
    // This the client id you get from the json file you downloaded to the assets folder (type: 3)
    public static string webClientId = "341596242404-5ilrqe93hqis8du9tkvsio21q23caj0n.apps.googleusercontent.com";
    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    public static bool loadDashboard = false; 
    public Text infoTxt;
    float infoTxtTimer = 0;
    


    public static Text logTxt;

    public static bool isDashboardLoaded = false;
    public static bool signedUpSuccessfully = false;

    static float logTxtTimer = 0;

    //public static GameObject messageBoxPanel;

    void OnEnable()
    {
        //messageBoxPanel = GameObject.FindGameObjectWithTag("MessageBox");
        //Globals.messageBoxPrefab = Resources.Load<GameObject>("MessageBox");
        //Globals.canvasObject = GameObject.FindGameObjectWithTag("DashboardPanel");
        //Globals.messagePanelAuthMan = GameObject.FindGameObjectWithTag("AuthManMessageBox");
        
    }

    public static void ViewMessageOnScreen(string msg)
    {
        
        Debug.Log("Showing message from AuthMan ... ");

        // Activate the UI
        MessageBoxMan.Open(msg);
    }

    #region GoogleAndFacebookSignInFunctionality
    private void Awake()
    {
        /* Google Initializations */
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        CheckFirebaseDependencies();        // Don't add this when you are running the game in the editor 
        /* Facebook Initializations */
        if (!(FB.IsInitialized))
        {
            // Initiliaze the facebook sdk
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation event
            FB.ActivateApp();
        }

        Debug.Log(FB.Android.KeyHash);
    }
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation 
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize the Facebook SDK ");
            ViewMessageOnScreen("Failed to initialize the Facebook SDK ");
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!(isGameShown))
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again 
            Time.timeScale = 1;
        }
    }
    public static void SignInWithFacebook()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }
    private static void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details 
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Prints current access token's User ID
            Debug.Log("Access token: " + aToken.UserId.ToString());
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
                //Debug.Log("Access Token String: " + aToken.TokenString);
            }
            Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(aToken.TokenString);
            FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Facebook Login Canceled ... ");
                    ViewMessageOnScreen("Facebook Login Canceled ... ");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("Facebook Login error: " + task.Exception);
                    ViewMessageOnScreen("Facebook Login error: " + task.Exception);
                    return;
                }

                Debug.Log("Signed In Successfully ... ");
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.Log("Welcome: " + newUser.DisplayName);
                Debug.Log("Email: " + newUser.Email);
                Globals.username = newUser.DisplayName.ToString();
                Globals.email = newUser.DisplayName.ToString();
                Globals.userId = newUser.UserId.ToString();
                loadDashboard = true;
                Globals.isLoggedIn = true;
                Globals.loggedInWithFaceBook = true;
            });
        }
        else
        {
            Debug.Log("User cancelled login ... ");
            ViewMessageOnScreen("User Canceled Login ... ");
        }
    }
    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    auth = FirebaseAuth.DefaultInstance;
                }
                else
                {
                    AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
                    ViewMessageOnScreen("Could not resolve all Firebase dependencies: " + task.Result.ToString());
                }
            }
        });
    }
    public void SignInWithGoogle() { OnSignIn();  }
    public void SignOutFromGoogle() { OnSignOut();  }
    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling sign in ... ");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }
    private void OnSignOut()
    {
        AddToInformation("Calling SignOut ... ");
        GoogleSignIn.DefaultInstance.SignOut();
    }
    private void OnDisconnect()
    {
        AddToInformation("Calling Disconnect ... ");
        GoogleSignIn.DefaultInstance.Disconnect();
    }
    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        loadDashboard = false;
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                    ViewMessageOnScreen("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception " + task.Exception);
                    ViewMessageOnScreen("Got Unexpected Exception " + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled ... ");
            ViewMessageOnScreen("Canceled ... ");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email: " + task.Result.Email);
            loadDashboard = true;
            Globals.username = task.Result.DisplayName;
            Globals.email = task.Result.Email;
            Globals.userId = task.Result.UserId;
            Globals.isLoggedIn = true;
            Globals.loggedInWithGoogle = true;
            //AddToInformation("Google Id Token: " + task.Result.IdToken);
            //AddToInformation("Google Id Token: " + task.Result.IdToken);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }
    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                {
                    AddToInformation("\nError code: " + inner.ErrorCode + " Message: " + inner.Message);
                    ViewMessageOnScreen("\nError code: " + inner.ErrorCode + " Message: " + inner.Message);
                }
                else
                {
                    loadDashboard = true;
                    Globals.username = task.Result.DisplayName;
                    Globals.email = task.Result.Email;
                    Globals.userId = task.Result.UserId;
                    Globals.isLoggedIn = true;
                    Globals.loggedInWithGoogle = true;
                    AddToInformation("SignIn Successful ... ");
                }
            }
        });
    }
    public void SignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling Sign In Silently ... ");
        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }
    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;
        AddToInformation("Calling Games Sign IN ");
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }
    private void AddToInformation(string str) { if (infoTxt) { infoTxt.text = str; infoTxtTimer = 5.0f;  } }
    #endregion 

    #region EmailSignInFunctionality
    static bool CheckPassStrength(string password)
    {
        string[] chars = { "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "-", "+", "=", "~", "`", "/", "\\", ">", "<" };
        int length = password.Length;
        bool isAlpha = false;
        for (int i = 0; i < chars.Length; i++)
        {
            if (password.Contains(chars[i]))
            {
                isAlpha = true;
                break;
            }
        }
        if (length >= 8 && isAlpha == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    static void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();
        LogScreenMessages(msg);
        Debug.Log(msg);
    }
    static void LogScreenMessages(string msg)
    {
        if (logTxt)
        {
            logTxt.text = msg;
            logTxtTimer = 5.0f;
        }
    }
    #endregion

    public static bool EmailLogin(string username, string password)
    {
        UIMan.showLoadingPanel = true;
        bool res = false;
        if (username == "" || password == "")
        {
            // Empty fields 
            Debug.Log("Empty Field");
            LogScreenMessages("Empty Field");
            ViewMessageOnScreen("Empty Field");
            res = false;
        }
        else
        {
            FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(username, password).ContinueWith((task =>
            {
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    ViewMessageOnScreen(((AuthError)e.ErrorCode).ToString());
                    res = false;
                    return;
                }
                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    ViewMessageOnScreen(((AuthError)e.ErrorCode).ToString());
                    res = false;
                    return;
                }
                if (task.IsCompleted)
                {
                    Globals.username = username;
                    Globals.email = username;
                    Globals.userId = task.Result.UserId;
                    Globals.isLoggedIn = true;
                    Globals.loggedInWithEmail = true;
                    loadDashboard = true;
                    Debug.Log("Welcome back " + Globals.email);
                    LogScreenMessages("Welcome back " + Globals.email);
                    res = true;
                }
                UIMan.showLoadingPanel = false;
            }));
        }
        return res;
    }

    public static bool RegisterUser(string email, string password)
    {
        UIMan.showLoadingPanel = true;
        bool res = false;
        if (password == "" || email == "")
        {
            // Empty field 
            res = false;
            Debug.Log("Some fields are empty ... ");
            LogScreenMessages("Some fields are empty ... ");
        }
        else if (!(CheckPassStrength(password)))
        {
            // Weak pass
            res = false;
            Debug.Log("Weak password ... ");
            LogScreenMessages("Weak password ... ");
            ViewMessageOnScreen("Weak password ... ");

        }
        else
        {
            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith((task =>
            {
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    ViewMessageOnScreen(((AuthError)e.ErrorCode).ToString());
                    res = false;
                    return;
                }
                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    ViewMessageOnScreen(((AuthError)e.ErrorCode).ToString());
                    res = false;
                    return;
                }
                if (task.IsCompleted)
                {
                    Debug.Log("Signed Up Successfully ... ");
                    signedUpSuccessfully = true;
                    LogScreenMessages("Signed Up Successfully ... ");
                    ViewMessageOnScreen("Signed Up Successfully ... ");
                    res = true;
                }
                UIMan.showLoadingPanel = false;
            }));
        }
        return res;
    }

    public static void LoginAnonymous()
    {

    }

    public static void LogOut()
    {
        if (Globals.loggedInWithEmail)
        {
            Globals.isLoggedIn = false;
            Globals.loggedInWithEmail = false;
            Globals.username = "";
            Globals.email = "";
            Globals.userId = "";
            isDashboardLoaded = false;
            DBMan.isItemsRetrieved = false;
            loadDashboard = false;
            //SceneManager.LoadScene("LoginMenu", LoadSceneMode.Single);
            LogScreenMessages("Logged Out ... ");
            FirebaseAuth.DefaultInstance.SignOut();
        }
        else if (Globals.loggedInWithGoogle)
        {
            Globals.isLoggedIn = false;
            Globals.loggedInWithGoogle = false;
            Globals.username = "";
            Globals.email = "";
            Globals.userId = "";
            DBMan.isItemsRetrieved = false;
            isDashboardLoaded = false;
            loadDashboard = false;
            GoogleSignIn.DefaultInstance.SignOut();
            FirebaseAuth.DefaultInstance.SignOut();
            Debug.Log("Signed out from google ... ");
        }
        else if (Globals.loggedInWithFaceBook)
        { 
            Globals.isLoggedIn = false;
            Globals.loggedInWithFaceBook = false;
            Globals.username = "";
            Globals.email = "";
            Globals.userId = "";
            DBMan.isItemsRetrieved = false;
            isDashboardLoaded = false;
            loadDashboard = false;
            LogScreenMessages("Logged out ... ");
            Debug.Log("Signed out from facebook ... ");
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    void Update()
    {
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

        if (infoTxt)
        {
            if (infoTxtTimer <= 0.0f)
            {
                infoTxt.text = "";
            }
            else
            {
                infoTxtTimer -= Time.deltaTime;
            }
        }
    }
}
