using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using SimpleFileBrowser;
using ArabicSupport;
using System;
//using Main.Assets.Scripts;




public class UIMan : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject signUpPanel;
    public GameObject dashboardPanel;
    public GameObject notificationsPanel;
    public GameObject searchItemPanel;
    public GameObject searchItemInnerPanel;
    public GameObject userProfilePanel;
    public GameObject itemsPanel;
    public GameObject itemsInnerPanel;
    public GameObject addItemPanel;
    public GameObject cartItemsPanel;
    public GameObject cartInnerPanel;
    public static GameObject itemDetailsPanel;
    public static GameObject cartItemDetailsPanel;
    public static GameObject itemSnapshotsPanel;
    public GameObject CheckOutItemPanel;
    //public GameObject messageBoxPrefab;
    public GameObject snapsInnerPanel;
    public GameObject itemSnapPrefab;
    public GameObject browseItemsBtnContainerPanel;
    public GameObject addItemBtnContainerPanel;
    public GameObject viewCartItemsBtnContainerPanel;
    public GameObject searchItemBtnContainerPanel;
    public GameObject profileBtnContainerPanel;
    public GameObject loadingPanel;


    public CategoriesToggleGroupMan categoriesPanelMan;


    public InputField loginEmailIF;
    public InputField loginPasswordIF;
    public InputField signUpEmailIF;
    public InputField signUpPasswordIF;
    public InputField signUpPasswordConfirmIF;
    public InputField addedItemName;
    public InputField addedItemPrice;
    public Dropdown addedItemCategory;
    public InputField addedItemDescription;
    public Dropdown addedItemType;
    public InputField addedItemQuantity;
    public InputField addedItemImagePath;
    public InputField searchItemNameIF;
    public InputField searchItemPriceIF;
    public InputField PurchaseFullNameIF;
    public InputField PurchaseAddressIF;
    public InputField PurchasePhoneNumberIF;

    //public GameObject canvasObject;


    public GameObject dashboardItemPrefab;

    // UI Toggles
    public Toggle darkModeToggle;
    public Toggle testsCategory;

    //public Dropdown userLanguageDD;

    // UI Buttons
    public Button browseItemBtn;
    public Button openCartBtn;
    public Button openSearchBtn;
    public Button logOutBtn;
    public Button refreshBtn;
    public Button searchItemBtn;
    public Button addToCartBtn;
    public Button closeItemDetailsBtn;
    public Button showItemPhotosPanelFromItemDetailsBtn;
    public Button openPurchaseItemPanelBtn;
    public Button openItemPhotosPanelFromItemCartDetailsBtn;
    public Button closeCartItemDetailsBtn;
    public Button proceedWithPurchaseBtn;
    public Button backFromPurchasePanelBtn;
    public Button browseItemImageBtn;
    public Button postItemToDatabaseBtn;
    public Button closeAddItemPanelBtn;
    public Button closeItemSnapPanelBtn;


    public Text userCategoriesLabel;

    public Text logTxt;

    bool arabicFieldsConfigured = false;
    public static bool showLoadingPanel = false;


    float logTxtTimer;
    public float verticalSlotSpacing;


    #region UIHelpers
    void LogMsgs(string msg)
    {
        if (logTxt)
        {
            logTxt.text = "";
            logTxtTimer = 5.0f;
        }
    }
    void HideAllPanel()
    {
        HelpersMan.HidePanel(signUpPanel);
        HelpersMan.HidePanel(loginPanel);
        HelpersMan.HidePanel(dashboardPanel);
        HelpersMan.HidePanel(notificationsPanel);
        HelpersMan.HidePanel(itemsPanel);
        HelpersMan.HidePanel(cartItemsPanel);
        HelpersMan.HidePanel(addItemPanel);
        HelpersMan.HidePanel(itemDetailsPanel);
        HelpersMan.HidePanel(cartItemDetailsPanel);
        HelpersMan.HidePanel(itemSnapshotsPanel);
        HelpersMan.HidePanel(searchItemPanel);
        HelpersMan.HidePanel(userProfilePanel);
        HelpersMan.HidePanel(CheckOutItemPanel);
        HelpersMan.HidePanel(loadingPanel);
        //HelpersMan.HidePanel(Globals.messagePanel);
    }
    void DeHighlightAllBtnContainers()
    {
        HelpersMan.DeHighlightPanel(addItemBtnContainerPanel);
        HelpersMan.DeHighlightPanel(browseItemsBtnContainerPanel);
        HelpersMan.DeHighlightPanel(viewCartItemsBtnContainerPanel);
        HelpersMan.DeHighlightPanel(searchItemBtnContainerPanel);
        HelpersMan.DeHighlightPanel(profileBtnContainerPanel);
    }
    public static void ViewMessageOnScreen(string msg)
    {
        ////Debug.Log("Viewing messages from UIMan ... ");
        ////Globals.messageBoxPrefab = Resources.Load<GameObject>("MessageBox");
        ////Globals.canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        ////if (Globals.messageBoxPrefab)
        ////{
        ////    Globals.messageBox = Instantiate(Globals.messageBoxPrefab);
        ////    if (Globals.canvasObject)
        ////    {
        ////        Globals.messageBox.transform.SetParent(Globals.canvasObject.transform);
        ////        Globals.messageBox.GetComponent<RectTransform>().position = Globals.canvasObject.GetComponent<RectTransform>().position;
        ////    }
        ////    Globals.messageBox.GetComponentsInChildren<Text>()[0].text = msg;
        ////}

        //// Activate the UI 
        //HelpersMan.ShowPanel(Globals.messagePanel);
        //Globals.messagePanel.GetComponentsInChildren<Text>()[0].text = msg;

        MessageBoxMan.Open(msg);
    }
    public static void ViewRestartAppMessageOnScreen(string msg)
    {
        MessageBoxMan.Open(msg, true);
    }
    void CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            if (Globals.userLanguage == "AR")
            {
                ViewMessageOnScreen(ArabicFixer.Fix("فشل الاتصال بالانترنت ... "));
            }
            else
            {
                ViewMessageOnScreen("Check Internet Connection ... ");
            }
        }
    }
    void SwitchColors(string mode)
    {
        if (mode == "Dark")
        {
            if (dashboardPanel)
            {
                dashboardPanel.GetComponent<Image>().color = Globals.darkModeColor;
                Globals.currentUser = new User(Globals.email, Globals.username, Globals.userId, "Dark");
                Globals.currentUser.userColorMode = "Dark";
                //DBMan.UpdateUserProfile(Globals.currentUser, Globals.userId);
                Debug.Log("Switched To Dark Colors ... ");
            }
        }
        else if (mode == "Normal")
        {
            if (dashboardPanel)
            {
                dashboardPanel.GetComponent<Image>().color = Globals.normalColor;
                Globals.currentUser = new User(Globals.email, Globals.username, Globals.userId, "Normal");
                Globals.currentUser.userColorMode = "Normal";
                //DBMan.UpdateUserProfile(Globals.currentUser, Globals.userId);
                Debug.Log("Switched To Normal Colors ... ");
            }
        }
    }
    void PrepareAddedCategoriesDD()
    {
        if (addedItemCategory && addedItemType)
        {
            if (Globals.userLanguage == "AR")
            {
                addedItemCategory.options.Clear();
                addedItemCategory.captionText.text = "";
                addedItemType.captionText.text = "";
                addedItemCategory.itemText.text = ArabicFixer.Fix(addedItemCategory.itemText.text);
                for (int i = 0; i < Globals.addedItemCategoriesArrayArabic.Length; i++)
                {
                    Dropdown.OptionData data = new Dropdown.OptionData(Globals.addedItemCategoriesArrayArabic[i]);
                    addedItemCategory.options.Add(data);
                    // Fix Arabic Characters 
                    addedItemCategory.options[i].text = ArabicFixer.Fix(addedItemCategory.options[i].text);
                }
                addedItemCategory.value = 0;
            }
            else if (Globals.userLanguage == "EN")
            {
                addedItemCategory.options.Clear();
                addedItemCategory.captionText.text = "";
                addedItemType.captionText.text = "";
                for (int i = 0; i < Globals.addedItemCategoriesArrayEnglish.Length; i++)
                {
                    Dropdown.OptionData data = new Dropdown.OptionData(Globals.addedItemCategoriesArrayEnglish[i]);
                    addedItemCategory.options.Add(data);
                }
            }
        }
    }
    void CategoriesAndTypesToggleInteraction()
    {
        if (addedItemCategory && addedItemType)
        {
            if (Globals.userLanguage == "AR")
            {
                addedItemType.itemText.text = ArabicFixer.Fix(addedItemType.itemText.text);
                if (addedItemCategory.options[addedItemCategory.value].text == ArabicFixer.Fix("اثاث"))
                {
                    Debug.Log(addedItemCategory.options[addedItemCategory.value].text);
                    Debug.Log("Choosed Furniture ... ");
                    addedItemType.options.Clear();
                    for (int i = 0; i < Globals.furnitureTypesArrayArabic.Length; i++)
                    {
                        Dropdown.OptionData data = new Dropdown.OptionData(Globals.furnitureTypesArrayArabic[i]);
                        addedItemType.options.Add(data);
                        // Fix Arabic Characters 
                        addedItemType.options[i].text = ArabicFixer.Fix(addedItemType.options[i].text);
                    }
                    addedItemType.captionText.text = addedItemType.options[0].text;
                    //addedItemType.gameObject.GetComponentInChildren<ArabicText>().Text = addedItemType.options[0].text;
                }
                else if (addedItemCategory.options[addedItemCategory.value].text == ArabicFixer.Fix("اجهزة منزلية"))
                {
                    Debug.Log(addedItemCategory.options[addedItemCategory.value].text);
                    Debug.Log("Choosed Appliances ... ");
                    addedItemType.options.Clear();
                    for (int i = 0; i < Globals.appliancesTypesArrayArabic.Length; i++)
                    {
                        Dropdown.OptionData data = new Dropdown.OptionData(Globals.appliancesTypesArrayArabic[i]);
                        addedItemType.options.Add(data);
                        // Fix Arabic Characters 
                        addedItemType.options[i].text = ArabicFixer.Fix(addedItemType.options[i].text);
                    }
                    addedItemType.captionText.text = addedItemType.options[0].text;
                    //addedItemType.gameObject.GetComponentInChildren<ArabicText>().Text = addedItemType.options[0].text;
                }
                else
                {
                    Debug.Log(addedItemCategory.options[addedItemCategory.value].text);
                    Debug.Log("Choosed neither .. ");
                    addedItemType.options.Clear();
                    addedItemType.itemText.text = "";
                    addedItemType.captionText.text = "";
                    //addedItemType.options = new List<Dropdown.OptionData>();
                }
            }
            else if (Globals.userLanguage == "EN")
            {
                if (addedItemCategory.options[addedItemCategory.value].text == "Furniture")
                {
                    Debug.Log(addedItemCategory.options[addedItemCategory.value].text);
                    Debug.Log("Choosed Furniture ... ");
                    addedItemType.options.Clear();
                    for (int i = 0; i < Globals.furnitureTypesArrayEnglish.Length; i++)
                    {
                        Dropdown.OptionData data = new Dropdown.OptionData(Globals.furnitureTypesArrayEnglish[i]);
                        addedItemType.options.Add(data);
                    }
                    addedItemType.captionText.text = addedItemType.options[0].text;
                }
                else if (addedItemCategory.options[addedItemCategory.value].text == "Appliances")
                {
                    Debug.Log(addedItemCategory.options[addedItemCategory.value].text);
                    Debug.Log("Choosed Appliances ... ");
                    addedItemType.options.Clear();
                    for (int i = 0; i < Globals.appliancesTypesArrayEnglish.Length; i++)
                    {
                        Dropdown.OptionData data = new Dropdown.OptionData(Globals.appliancesTypesArrayEnglish[i]);
                        addedItemType.options.Add(data);
                    }
                    addedItemType.captionText.text = addedItemType.options[0].text;
                }
                else
                {
                    Debug.Log(addedItemCategory.options[addedItemCategory.value].text);
                    Debug.Log("Choosed neither .. ");
                    addedItemType.options.Clear();
                    addedItemType.itemText.text = "";
                    addedItemType.captionText.text = "";
                    //addedItemType.options = new List<Dropdown.OptionData>();
                }
            }
        }
    }
    void ChangeAddItemFieldsToArabic()
    {
        if (addedItemName && addedItemDescription && addedItemPrice && addedItemQuantity)
        {
            if (Globals.userLanguage == "AR")
            {
                Debug.Log("Fixing Language ... ");
                addedItemName.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("ادخل اسم السلعة ... ");
                addedItemDescription.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("وصف السلعة (اختياري) ...");
                addedItemPrice.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("سعر السلعة ...");
                addedItemQuantity.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("الكمية ...");
            }
            else
            {
                addedItemName.placeholder.GetComponent<Text>().text = "Item name ... ";
                addedItemDescription.placeholder.GetComponent<Text>().text = "Item description (optional) ... ";
                addedItemPrice.placeholder.GetComponent<Text>().text = "Item price ... ";
                addedItemQuantity.placeholder.GetComponent<Text>().text = "Item Quantity ... ";
            }
        }
    }
    void ChangeProfilePanelFieldsToArabic()
    {
        if (Globals.userLanguage == "AR")
        {
            if (userProfilePanel)
            {
                userProfilePanel.GetComponentsInChildren<Text>()[0].text = ArabicFixer.Fix("اسم المستخدم: " + Globals.username);
                userProfilePanel.GetComponentsInChildren<Text>()[1].text = ArabicFixer.Fix("البريد الالكتروني: " + Globals.username);
            }
        }
        else
        {
            if (userProfilePanel)
            {
                userProfilePanel.GetComponentsInChildren<Text>()[0].text = "Username: " + Globals.username;
                userProfilePanel.GetComponentsInChildren<Text>()[1].text = "Email: " + Globals.email;
            }
        }
    }
    void ChangeHeaderBtnsToArabic()
    {
        if (Globals.userLanguage == "AR")
        {
            if (browseItemBtn)
            {
                browseItemBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("تصفح");
            }
            if (openCartBtn)
            {
                openCartBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("العربة");
            }
            if (openSearchBtn)
            {
                openSearchBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("بحث");
            }
            if (logOutBtn)
            {
                logOutBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("خروج");
            }
            if (refreshBtn)
            {
                refreshBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اعادة تحميل");
            }
            if (searchItemBtn)
            {
                searchItemBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("بحث");
            }
            if (addToCartBtn)
            {
                addToCartBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اضف الى العربة");
            }
            if (closeItemDetailsBtn)
            {
                closeItemDetailsBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اغلاق");
            }
            if (showItemPhotosPanelFromItemDetailsBtn)
            {
                showItemPhotosPanelFromItemDetailsBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("صور");
            }
            if (openPurchaseItemPanelBtn)
            {
                openPurchaseItemPanelBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("شراء");
            }
            if (openItemPhotosPanelFromItemCartDetailsBtn)
            {
                openItemPhotosPanelFromItemCartDetailsBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("صور");
            }
            if (closeCartItemDetailsBtn)
            {
                closeCartItemDetailsBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اغلاق");
            }
            if (proceedWithPurchaseBtn)
            {
                proceedWithPurchaseBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("شراء");
            }
            if (backFromPurchasePanelBtn)
            {
                backFromPurchasePanelBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("رجوع");
            }
            if (browseItemImageBtn)
            {
                browseItemImageBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اضف صورة");
            }
            if (postItemToDatabaseBtn)
            {
                postItemToDatabaseBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("نشر");
            }
            if (closeAddItemPanelBtn)
            {
                closeAddItemPanelBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اغلاق");
            }
            if (closeItemSnapPanelBtn)
            {
                closeItemSnapPanelBtn.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("اغلاق");
            }        
        }
        else
        {
            if (browseItemBtn)
            {
                browseItemBtn.gameObject.GetComponentInChildren<Text>().text = "Browse";
            }
            if (openCartBtn)
            {
                openCartBtn.gameObject.GetComponentInChildren<Text>().text = "Cart";
            }
            if (openSearchBtn)
            {
                openSearchBtn.gameObject.GetComponentInChildren<Text>().text = "Search";
            }
            if (logOutBtn)
            {
                logOutBtn.gameObject.GetComponentInChildren<Text>().text = "LogOut";
            }
            if (refreshBtn)
            {
                refreshBtn.gameObject.GetComponentInChildren<Text>().text = "Refresh";
            }
            if (searchItemBtn)
            {
                searchItemBtn.gameObject.GetComponentInChildren<Text>().text = "Search";
            }
            if (addToCartBtn)
            {
                addToCartBtn.gameObject.GetComponentInChildren<Text>().text = "Add To Cart";
            }
            if (closeItemDetailsBtn)
            {
                closeItemDetailsBtn.gameObject.GetComponentInChildren<Text>().text = "Close";
            }
            if (showItemPhotosPanelFromItemDetailsBtn)
            {
                showItemPhotosPanelFromItemDetailsBtn.gameObject.GetComponentInChildren<Text>().text = "Photos";
            }
            if (openPurchaseItemPanelBtn)
            {
                openPurchaseItemPanelBtn.gameObject.GetComponentInChildren<Text>().text = "Purchase";
            }
            if (openItemPhotosPanelFromItemCartDetailsBtn)
            {
                openItemPhotosPanelFromItemCartDetailsBtn.gameObject.GetComponentInChildren<Text>().text = "Photos";
            }
            if (closeCartItemDetailsBtn)
            {
                closeCartItemDetailsBtn.gameObject.GetComponentInChildren<Text>().text = "Close";
            }
            if (proceedWithPurchaseBtn)
            {
                proceedWithPurchaseBtn.gameObject.GetComponentInChildren<Text>().text = "Proceed";
            }
            if (backFromPurchasePanelBtn)
            {
                backFromPurchasePanelBtn.gameObject.GetComponentInChildren<Text>().text = "Back";
            }
            if (browseItemImageBtn)
            {
                browseItemImageBtn.gameObject.GetComponentInChildren<Text>().text = "Browse";
            }
            if (postItemToDatabaseBtn)
            {
                postItemToDatabaseBtn.gameObject.GetComponentInChildren<Text>().text = "Post";
            }
            if (closeAddItemPanelBtn)
            {
                closeAddItemPanelBtn.gameObject.GetComponentInChildren<Text>().text = "Close";
            }
            if (closeItemSnapPanelBtn)
            {
                closeItemSnapPanelBtn.gameObject.GetComponentInChildren<Text>().text = "Close";
            }
        }
    }
    void ChangeUIBtnsToArabic()
    {
        ChangeHeaderBtnsToArabic();
    }
    void ChangeUITogglesToArabic()
    {
        if (Globals.userLanguage == "AR")
        {
            if (darkModeToggle)
            {
                darkModeToggle.gameObject.GetComponentInChildren<Text>().text = ArabicFixer.Fix("الوضع الخافت");
            }
        }
        else
        {
            if (darkModeToggle)
            {
                darkModeToggle.gameObject.GetComponentInChildren<Text>().text = "Dark Mode";
            }
        }
    }
    void ChangeUITxtsToArabic()
    {
        if (userCategoriesLabel)
        {
            if (Globals.userLanguage == "AR")
            {
                userCategoriesLabel.text = ArabicFixer.Fix("الفئات");
            }
            else
            {
                userCategoriesLabel.text = "Categories";
            }
        }
    }
    void ConfigureInputFieldsToArabic()
    {
        if (addedItemName && addedItemDescription && addedItemPrice && addedItemQuantity)
        {
            addedItemName.onEndEdit.AddListener(delegate { addedItemName.text = ArabicFixer.Fix(addedItemName.text); });
            addedItemDescription.onEndEdit.AddListener(delegate { addedItemDescription.text = ArabicFixer.Fix(addedItemDescription.text); });
            addedItemPrice.onEndEdit.AddListener(delegate { addedItemPrice.text = ArabicFixer.Fix(addedItemPrice.text); });
            addedItemQuantity.onEndEdit.AddListener(delegate { addedItemQuantity.text = ArabicFixer.Fix(addedItemQuantity.text); });
        }
        if (searchItemNameIF && searchItemPriceIF)
        {
            searchItemNameIF.onEndEdit.AddListener(delegate { searchItemNameIF.text = ArabicFixer.Fix(searchItemNameIF.text); });
            searchItemPriceIF.onEndEdit.AddListener(delegate { searchItemPriceIF.text = ArabicFixer.Fix(searchItemPriceIF.text); });
        }
        if (PurchaseFullNameIF && PurchasePhoneNumberIF && PurchaseAddressIF)
        {
            PurchaseFullNameIF.onEndEdit.AddListener(delegate { PurchaseFullNameIF.text = ArabicFixer.Fix(PurchaseFullNameIF.text); });
            PurchaseAddressIF.onEndEdit.AddListener(delegate { PurchaseAddressIF.text = ArabicFixer.Fix(PurchaseAddressIF.text); });
            PurchasePhoneNumberIF.onEndEdit.AddListener(delegate { PurchasePhoneNumberIF.text = ArabicFixer.Fix(PurchasePhoneNumberIF.text); });
        }
    }
    #endregion

    void OnEnable()
    {
        //Globals.messagePanel = GameObject.FindGameObjectWithTag("MessageBox");
        CheckInternet();
        if (SceneManager.GetActiveScene().name == "LoginMenu")
        {
            Globals.isLoggedIn = false;
            Globals.username = "";
            Globals.userId = "";
            HideAllPanel();
            HelpersMan.ShowPanel(loginPanel);
            loginPasswordIF.contentType = InputField.ContentType.Password;
            signUpPasswordIF.contentType = InputField.ContentType.Password;
            signUpPasswordConfirmIF.contentType = InputField.ContentType.Password;
            //ViewMessageOnScreen("Login with your credentials ... ");
            //Globals.messageBoxPrefab = messageBoxPrefab;
            //Globals.canvasObject = canvasObject;
        }
        else if (SceneManager.GetActiveScene().name == "Dashboard")
        {
            //if (userLanguageDD)
            //{
            //    Globals.userLanguage = userLanguageDD.options[userLanguageDD.value].text;
            //}
            DBMan.LoadUserInfo();
            if (!(arabicFieldsConfigured))
            {
                ConfigureInputFieldsToArabic();
                arabicFieldsConfigured = true;
            }
            itemDetailsPanel = GameObject.FindGameObjectWithTag("ItemDetailsPanel");
            cartItemDetailsPanel = GameObject.FindGameObjectWithTag("CartItemDetailsPanel");
            itemSnapshotsPanel = GameObject.FindGameObjectWithTag("ItemSnapshotsPanel");
            DBMan.slotTex = new Texture2D(2, 2);
            HelpersMan.ClearDDOptions(addedItemType);
            HideAllPanel();
            HelpersMan.ShowPanel(dashboardPanel);
            HelpersMan.ShowPanel(itemsPanel);
            DeHighlightAllBtnContainers();
            HelpersMan.HighlightPanel(browseItemsBtnContainerPanel);
            DBMan.RetrieveItemsFromDatabase();
        }
    }

    void Update()
    {
        CheckInternet();
        if (SceneManager.GetActiveScene().name == "LoginMenu")
        {
            if (AuthMan.loadDashboard && Globals.isLoggedIn)
            {
                AuthMan.loadDashboard = false;
                SceneManager.LoadScene("Dashboard", LoadSceneMode.Single);
                DBMan.LoadUserInfo();
            }
            if (AuthMan.signedUpSuccessfully)
            {
                AuthMan.signedUpSuccessfully = false;
                HelpersMan.HidePanel(signUpPanel);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Dashboard")
        {
            if (!(AuthMan.loadDashboard) && !(Globals.isLoggedIn))
            {
                SceneManager.LoadScene("LoginMenu", LoadSceneMode.Single);
            }
            if (DBMan.userInfoLoaded)
            {
                if (darkModeToggle)
                {
                    if (Globals.currentUser.userColorMode == "Normal")
                    {
                        darkModeToggle.isOn = false;
                        SwitchColors("Normal");
                        Debug.Log("Normal Colors Settings ... ");
                    }
                    else if (Globals.currentUser.userColorMode == "Dark")
                    {
                        darkModeToggle.isOn = true;
                        SwitchColors("Dark");
                        Debug.Log("Dark Colors Settings ... ");
                    }
                }
                //if (Globals.userLanguage == "EN")
                //{
                //    if (userLanguageDD)
                //    {
                //        userLanguageDD.value = 0;
                //    }
                //}
                //else if (Globals.userLanguage == "AR")
                //{
                //    if (userLanguageDD)
                //    {
                //        userLanguageDD.value = 1;
                //    }
                //}
                //for (int i = 0; i < )
                ChangeUIBtnsToArabic();
                ChangeUITogglesToArabic();
                ChangeUITxtsToArabic();
                DBMan.userInfoLoaded = false;
            }
            if (DBMan.loadDashboard)
            {
                UpdateDashboardItems();
                DBMan.loadDashboard = false;
                Debug.Log("Updating UI ... ");
            }
            if (DBMan.cartRetrieved)
            {
                UpdateCartItems();
                DBMan.cartRetrieved = false;
                Debug.Log("Updating user cart ... ");
            }
            if (categoriesPanelMan)
            {
                if (categoriesPanelMan.isLanguageChanged)
                {
                    ChangeProfilePanelFieldsToArabic();
                    ChangeUIBtnsToArabic();
                    ChangeUITogglesToArabic();
                    ChangeUITxtsToArabic();
                    BrowseItemsBtnCallback();
                    categoriesPanelMan.isLanguageChanged = false;
                }
            }
            if (DBMan.snapshotsLoaded)
            {
                try
                {
                    Debug.Log("Assigned Byte array ... ");
                    //HelpersMan.ResetTexture2D(ref slotTex);
                    if (DBMan.slotTex != null)
                    {
                        Debug.Log("Loaded image contents ... ");
                        DBMan.slotTex.LoadImage(DBMan.snapBytes);
                        Globals.snapViewSlots[0].GetComponent<RawImage>().texture = DBMan.slotTex;
                    }
                    else
                    {
                        Debug.Log("slotTex object is still null ... ");
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
                DBMan.snapshotsLoaded = false;
            }
            if (showLoadingPanel)
            {
                HelpersMan.ShowPanel(loadingPanel);
            }
            else
            {
                HelpersMan.HidePanel(loadingPanel);
            }
        }
    }

    void PrepareSnapsPanel()
    {
        if (itemSnapPrefab)
        {
            if (snapsInnerPanel)
            {
                //snapsInnerPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.currentResolution.width - 50, Screen.currentResolution.height - 50);
            }
            itemSnapshotsPanel = GameObject.FindGameObjectWithTag("ItemSnapshotsPanel");
            HelpersMan.DestroyGameObjects(Globals.snapViewSlots);
            Debug.Log("Loading item photos ... ");
            // One Photo (for now)
            GameObject snapSlot = Instantiate(itemSnapPrefab);
            snapSlot.transform.SetParent(snapsInnerPanel.transform);
            // Keeping track of added snaps 
            Globals.snapViewSlots.Add(snapSlot);
            // Setting the Image of the slot 
            if (itemSnapshotsPanel)
            {
                Debug.Log("Executing load image function on: " + itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item.itemName.ToString());
                DBMan.LoadItemImage(itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item, snapSlot);
            }
        }
    }

    void UpdateCartItems()
    {
        //DBMan.LoadUserInfo();
        if (dashboardItemPrefab && cartInnerPanel)
        {
            cartInnerPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.currentResolution.width - 70, Screen.currentResolution.height / 5);
            if (Screen.currentResolution.height >= 1080)
            {
                cartInnerPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, -Screen.currentResolution.height / 10);
            }

            HelpersMan.DestroyGameObjects(Globals.cartItemViewSlots);
            Debug.Log("Displaying items for UserID: " + Globals.userId.ToString());
            Debug.Log("Number of items in cart: " + Globals.cartItems.Count);
            for (int i = 0; i < Globals.cartItems.Count; i++)
            {
                if (Globals.cartItems[i].itemStatus != "Sold")
                {
                    GameObject slot = Instantiate(dashboardItemPrefab);
                    slot.transform.SetParent(cartInnerPanel.transform);
                    // Setting the name of the item 
                    slot.GetComponentsInChildren<Text>()[0].text = Globals.cartItems[i].itemName;
                    // Setting the price of the item 
                    slot.GetComponentsInChildren<Text>()[1].text = Globals.cartItems[i].itemPrice.ToString();
                    // Add functions to buttons on slots
                    slot.GetComponent<ItemSlot>().slotItem = Globals.cartItems[i];
                    //if (itemSnapshotsPanel)
                    //{
                    //    itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item = Globals.cartItems[i];
                    //}
                    slot.GetComponent<Button>().onClick.AddListener(slot.GetComponent<ItemSlot>().ViewCartItemDetails);
                    // Keeping track of items in the panel
                    Globals.cartItemViewSlots.Add(slot);
                    Debug.Log("Cart items updated ... ");
                }
            }
        }
    }

    void UpdateSearchItems()
    {
        //DBMan.LoadUserInfo();
        // Prepare UI Elements for displaying search items
        if (dashboardItemPrefab && searchItemInnerPanel)
        {
            searchItemInnerPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.currentResolution.width - 70, Screen.currentResolution.height / 5);
            if (Screen.currentResolution.height >= 1080)
            {
                searchItemInnerPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, -Screen.currentResolution.height / 10);
            }

            HelpersMan.DestroyGameObjects(Globals.searchViewSlots);
            Debug.Log("Displaying " + Globals.searchItems.Count + " items ... ");
            for (int i = 0; i < Globals.searchItems.Count; i++)
            {
                GameObject slot = Instantiate(dashboardItemPrefab);
                slot.transform.SetParent(searchItemInnerPanel.transform);
                // Setting the name of the item
                slot.GetComponentsInChildren<Text>()[0].text = Globals.searchItems[i].itemName;
                // Setting the price of the item
                slot.GetComponentsInChildren<Text>()[1].text = Globals.searchItems[i].itemPrice.ToString();
                // Add functions to buttons on slots
                slot.GetComponent<ItemSlot>().slotItem = Globals.searchItems[i];
                slot.GetComponent<Button>().onClick.AddListener(slot.GetComponent<ItemSlot>().ViewItemDetails);
                // Keeping track of the items in the panel 
                Globals.searchViewSlots.Add(slot);
                Debug.Log("Added search result to UI ... ");
            }
        }
    }

    void UpdateDashboardItems()
    {
        //DBMan.LoadUserInfo();
        if (dashboardItemPrefab && itemsInnerPanel)
        {
            itemsInnerPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.currentResolution.width - 70, Screen.currentResolution.height / 5);
            if (Screen.currentResolution.height >= 1080)
            {
                itemsInnerPanel.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, -Screen.currentResolution.height / 10);
            }


            HelpersMan.DestroyGameObjects(Globals.itemViewSlots);
            //Debug.Log("Displaying " + Globals.dashboardItems.Count + " items");
            for (int i = 0; i <Globals.dashboardItems.Count; i++)
            {
                for (int j = 0; j < Globals.userCategories.Length; j++)
                {
                    if (Globals.dashboardItems[i].itemCategory == Globals.userCategories[j])
                    {
                        GameObject slot = Instantiate(dashboardItemPrefab);
                        slot.transform.SetParent(itemsInnerPanel.transform);
                        // Setting the name of the item 
                        slot.GetComponentsInChildren<Text>()[0].text = Globals.dashboardItems[i].itemName;
                        // Setting the price of the item
                        slot.GetComponentsInChildren<Text>()[1].text = Globals.dashboardItems[i].itemPrice.ToString();
                        // Add functions to buttons on slots 
                        slot.GetComponent<ItemSlot>().slotItem = Globals.dashboardItems[i].ShallowCopy();
                        //if (itemSnapshotsPanel)
                        //{
                        //    itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item = Globals.dashboardItems[i];
                        //}
                        slot.GetComponent<Button>().onClick.AddListener(slot.GetComponent<ItemSlot>().ViewItemDetails);
                        // Keeping tracks of items in the panel
                        Globals.itemViewSlots.Add(slot);
                        //Debug.Log("added item to UI ... ");
                    }
                }
            }
        }
    }

    IEnumerator ShowLoadItemImageCoroutine()
    {
        /* FileBrowser initializations*/
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));     // Set the show all files flag to true and show items of type (.png, .jpg)
        FileBrowser.SetDefaultFilter(".png");           // Default files to be displayed are of type (.png)
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".exe");
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load Image", "Load");
        if (FileBrowser.Success)
        {
            Debug.Log("Image: " + FileBrowser.Result);
            if (FileBrowser.Result != "")
            {
                if (addedItemImagePath)
                {
                    addedItemImagePath.text = FileBrowser.Result;
                }
            }
        }
    }

    #region Callbacks
    public void LoginBtnCallback()
    {
        if (loginEmailIF && loginPasswordIF)
        {
            if (loginPasswordIF.text == "" || loginPasswordIF.text == "")
            {
                Debug.Log("Please fill in all the fields .... ");
                LogMsgs("Please fill in all the fields ... ");
            }
            else
            {
                loginPasswordIF.contentType = InputField.ContentType.Standard;
                string pass = loginPasswordIF.text.ToString();
                loginPasswordIF.contentType = InputField.ContentType.Password;
                AuthMan.EmailLogin(loginEmailIF.text.ToString(), pass);
            }
        }
    }
    public void CreateAccountBtnCallback()
    {
        // Show Sign Up panel
        HelpersMan.ShowPanel(signUpPanel);
    }
    public void SignUpBtnCallback()
    {
        if (signUpEmailIF && signUpPasswordIF && signUpPasswordConfirmIF)
        {
            if (signUpEmailIF.text == "" || signUpPasswordIF.text == "" || signUpPasswordConfirmIF.text == "")
            {
                Debug.Log("Some Fields are empty ... ");
                LogMsgs("Some Fields are empty ... ");
                ViewMessageOnScreen("Some Fields are empty ... ");
            }
            else if (!(signUpEmailIF.text.Contains("@")))
            {
                Debug.Log("Invalid email address (should contain @provider.com) ... ");
                LogMsgs("Invalid email address (should contain @provider.com) ... ");
                ViewMessageOnScreen("Invalid email address(should contain @provider.com)... ");
            }
            else
            {
                signUpPasswordIF.contentType = InputField.ContentType.Standard;
                string pass = signUpPasswordIF.text.ToString();
                signUpPasswordIF.contentType = InputField.ContentType.Password;
                signUpPasswordConfirmIF.contentType = InputField.ContentType.Standard;
                string passConfirm = signUpPasswordConfirmIF.text.ToString();
                signUpPasswordConfirmIF.contentType = InputField.ContentType.Password;
                if (pass != passConfirm)
                {
                    Debug.Log("Passwords don't match ... ");
                    LogMsgs("Passwords don't match ... ");
                    ViewMessageOnScreen("Passwords don't match ... ");
                    return;
                }
                AuthMan.RegisterUser(signUpEmailIF.text.ToString(), pass);
            }
        }
    }
    public void CloseSignUpPanelBtnCallback()
    {
        HelpersMan.HidePanel(signUpPanel);
    }
    public void LogOutBtnCallback()
    {
        AuthMan.LogOut();
    }
    public void ExitAppBtnCallback()
    {
        Application.Quit();

    }
    public void SignInWithFacebook()
    {
        AuthMan.SignInWithFacebook();
    }
    public void AddItemBtnCallback()
    {
        if (addedItemName && addedItemType && addedItemCategory && addedItemDescription && addedItemPrice && addedItemQuantity && addedItemImagePath)
        {
            if (HelpersMan.CheckEmpty(addedItemName) || HelpersMan.CheckEmpty(addedItemPrice) || HelpersMan.CheckEmpty(addedItemCategory) || HelpersMan.CheckEmpty(addedItemType) || HelpersMan.CheckEmpty(addedItemQuantity))
            {
                Debug.Log("Some fields are empty .... ");
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen(ArabicFixer.Fix("لم يتم ادخال البيانات كاملة ..."));
                }
                else
                {
                    ViewMessageOnScreen("Some fields are empty .... ");
                }
                return;
            }

            if (!(HelpersMan.CheckNumeric(HelpersMan.ToEnglishNumber(addedItemPrice.text.ToString()))))
            {
                Debug.Log("The price must be a number ... ");
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen(ArabicFixer.Fix("السعر غير صالح ..."));
                }
                else
                {
                    ViewMessageOnScreen("The price must be a number ... ");
                }
                return;
            }

            if (!(HelpersMan.CheckNumeric(HelpersMan.ToEnglishNumber(addedItemQuantity.text.ToString()))))
            {
                Debug.Log("The quantity must be an integer number ... ");
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen("الكمية غير صالحة ...");
                }
                else
                {
                    ViewMessageOnScreen("The quantity must be an integer number ... ");
                }
                return;
            }

            DBMan.AddItemToDatabase(new Item(
                addedItemName.text.ToString(),
                addedItemType.options[addedItemType.value].text.ToString(),
                addedItemDescription.text.ToString(),
                addedItemCategory.options[addedItemCategory.value].text.ToString(),
                double.Parse(HelpersMan.ToEnglishNumber(addedItemPrice.text.ToString())),
                Globals.userId.Replace(".com", "").ToString(),
                DateTime.Now.ToString(),
                "Available",
                int.Parse(HelpersMan.ToEnglishNumber(addedItemQuantity.text.ToString())),
                addedItemImagePath.text.ToString()
            ));
            HelpersMan.HidePanel(addItemPanel);
            HelpersMan.ShowPanel(itemsPanel);
            DeHighlightAllBtnContainers();
            HelpersMan.HighlightPanel(browseItemsBtnContainerPanel);
        }
    }
    public void CloseAddItemBtnCallback()
    {
        HelpersMan.HidePanel(addItemPanel);
        HelpersMan.ShowPanel(itemsPanel);
        DeHighlightAllBtnContainers();
        HelpersMan.HighlightPanel(browseItemsBtnContainerPanel);
    }
    public void RefreshItemsPanelBtnCallback()
    {
        DBMan.RetrieveItemsFromDatabase();
        HideAllPanel();
        HelpersMan.ShowPanel(dashboardPanel);
        HelpersMan.ShowPanel(itemsPanel);
        DeHighlightAllBtnContainers();
        HelpersMan.HighlightPanel(browseItemsBtnContainerPanel);
    }
    public void CloseItemDetailsPanelBtnCallback()
    {
        HelpersMan.HidePanel(itemDetailsPanel);
    }
    public void AddItemToCartBtnCallback()
    {
        if (Globals.username != "")
        {
            if (itemDetailsPanel)
            {
                //DBMan.AddItemToUserCart(new Item(
                //itemDetailsPanel.GetComponentsInChildren<Text>()[0].text.ToString().Replace("Name: ", ""),
                //itemDetailsPanel.GetComponentsInChildren<Text>()[2].text.ToString().Replace("Type: ", ""),
                //itemDetailsPanel.GetComponentsInChildren<Text>()[3].text.ToString().Replace("Description: ", ""),
                //itemDetailsPanel.GetComponentsInChildren<Text>()[1].text.ToString().Replace("Category: ", ""),
                //double.Parse(itemDetailsPanel.GetComponentsInChildren<Text>()[4].text.ToString().Replace("Price: ", "").Replace(" EGP.", "")),
                //itemDetailsPanel.GetComponentsInChildren<Text>()[6].text.ToString().Replace("Seller: ", ""),
                //itemDetailsPanel.GetComponentsInChildren<Text>()[8].text.ToString().Replace("Date: ", ""),
                //itemDetailsPanel.GetComponentsInChildren<Text>()[7].text.ToString().Replace("Status: ", ""),
                //int.Parse(itemDetailsPanel.GetComponentsInChildren<Text>()[5].text.ToString().Replace("Quantity: ", "")),
                //itemDetailsPanel.GetComponent<ItemDetailsPanelMan>().currentItem.itemImagePath.ToString()
                //    ), Globals.userId.ToString());
                if (itemDetailsPanel.GetComponent<ItemDetailsPanelMan>().currentItem.itemStatus != "Sold")
                {
                    DBMan.AddItemToUserCart(itemDetailsPanel.GetComponent<ItemDetailsPanelMan>().currentItem, Globals.userId.ToString());
                    HideAllPanel();
                    HelpersMan.ShowPanel(dashboardPanel);
                    HelpersMan.ShowPanel(itemsPanel);
                    DeHighlightAllBtnContainers();
                    HelpersMan.HighlightPanel(browseItemsBtnContainerPanel);
                }
                else
                {
                    if (Globals.userLanguage == "AR")
                    {
                        ViewMessageOnScreen(ArabicFixer.Fix("هذه السلعة غير متوفره ... "));
                    }
                    else
                    {
                        ViewMessageOnScreen("This item has been sold ... ");
                    }
                }
            }
        }
    }
    public void OpenAddItemPanelBtnCallback()
    {
        HideAllPanel();
        HelpersMan.ShowPanel(dashboardPanel);
        HelpersMan.ShowPanel(addItemPanel);
        DeHighlightAllBtnContainers();
        HelpersMan.HighlightPanel(addItemBtnContainerPanel);
        PrepareAddedCategoriesDD();
        ChangeAddItemFieldsToArabic();
        if (addedItemCategory)
        {
            //addedItemCategory.value = 0;
            addedItemCategory.value = addedItemCategory.value == 0 ? 1 : 0;
            addedItemCategory.value = addedItemCategory.value == 0 ? 1 : 0;
            //addedItemCategory.itemText.text = addedItemCategory.options[addedItemCategory.value].text;
        }
    }
    public void BrowseItemsBtnCallback()
    {
        HideAllPanel();
        HelpersMan.ShowPanel(dashboardPanel);
        HelpersMan.ShowPanel(itemsPanel);
        DeHighlightAllBtnContainers();
        HelpersMan.HighlightPanel(browseItemsBtnContainerPanel);
        UpdateDashboardItems();
    }
    public void ViewUserCartBtnCallback()
    {
        HideAllPanel();
        HelpersMan.ShowPanel(dashboardPanel);
        HelpersMan.ShowPanel(cartItemsPanel);
        DBMan.RetrieveItemsFromCart(Globals.userId);
        DeHighlightAllBtnContainers();
        HelpersMan.HighlightPanel(viewCartItemsBtnContainerPanel);
    }
    public void CloseCartItemDetailsPanelBtnCallback()
    {
        HelpersMan.HidePanel(cartItemDetailsPanel);
        HelpersMan.ShowPanel(cartItemsPanel);
    }
    public void BrowseForItemImageBtnCallback()
    {
        StartCoroutine(ShowLoadItemImageCoroutine());
    }
    public void OpenItemPhotosBtnCallback()
    {
        HelpersMan.ShowPanel(itemSnapshotsPanel);
        if (itemSnapshotsPanel)
        {
            Debug.Log("Item: " + itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item.itemName);
            //DBMan.LoadItemImage(itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item);
            PrepareSnapsPanel();
        }
    }
    public void CloseItemPhotosBtnCallback()
    {
        HelpersMan.HidePanel(itemSnapshotsPanel);
    }
    public void OpenPurchaseItemPanelBtnCallback()
    {
        if (CheckOutItemPanel)
        {
            HelpersMan.ShowPanel(CheckOutItemPanel);
            if (PurchaseFullNameIF && PurchaseAddressIF && PurchasePhoneNumberIF)
            {
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen(ArabicFixer.Fix("من فضلك ادخل بياناتك ... "));
                    PurchaseFullNameIF.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("الاسم بالكامل ...");
                    PurchaseAddressIF.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("العنوان ...");
                    PurchasePhoneNumberIF.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("رقم الهاتف ... ");
                }
                else
                {
                    ViewMessageOnScreen("Please enter your data to proceed ... ");
                    PurchaseFullNameIF.placeholder.GetComponent<Text>().text = "Full Name ... ";
                    PurchaseAddressIF.placeholder.GetComponent<Text>().text = "Address ... ";
                    PurchasePhoneNumberIF.placeholder.GetComponent<Text>().text = "Phone Number ... ";
                }
            }
            if (cartItemDetailsPanel)
            {
                Debug.Log("Showing controls for Item: " + cartItemDetailsPanel.GetComponent<CartItemDetailsPanelMan>().currentItem.itemName.ToString());
            }
        }
    }
    public void PurchaseItemBtnCallback()
    {
        if (PurchaseFullNameIF && PurchaseAddressIF && PurchasePhoneNumberIF)
        {
            if (HelpersMan.CheckEmpty(PurchaseAddressIF) || HelpersMan.CheckEmpty(PurchaseAddressIF) || HelpersMan.CheckEmpty(PurchasePhoneNumberIF))
            {
                Debug.Log("Some fields are empty ... ");
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen(ArabicFixer.Fix("لم يتم ادخال البيانات كاملة ..."));
                }
                else
                {
                    ViewMessageOnScreen("Some fields are empty ... ");
                }
            }
            else if (!(HelpersMan.CheckNumeric(HelpersMan.ToEnglishNumber(PurchasePhoneNumberIF.text.ToString()))))
            {
                Debug.Log("Phone number should be a number (: ... ");
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen(ArabicFixer.Fix("رقم الهاتف غير صالح ... "));
                }
                else
                {
                    ViewMessageOnScreen("Phone number should be a number (: ... ");
                }
            }
            else
            {
                if (cartItemDetailsPanel)
                {
                    Item item = cartItemDetailsPanel.GetComponent<CartItemDetailsPanelMan>().currentItem;
                    item.itemStatus = "Sold";
                    DBMan.PurchaseItem(item, Globals.userId);
                    if (Globals.userLanguage == "AR")
                    {
                        ViewMessageOnScreen(ArabicFixer.Fix("تمت العملية.\n ستصلك رسالة تأكيد قريبا ... "));
                    }
                    else
                    {
                        ViewMessageOnScreen("Finished check out.\n Confirmation message will be sent to your email soon ... ");
                    }
                    string msgQuery = "Item: " + item.itemName
                        + " \npurchased by: " + PurchaseFullNameIF.text
                        + " \nOn: " + DateTime.Now.ToString()
                        + " \nAddress: " + PurchaseAddressIF.text.ToString()
                        + " \nPhone Number: " + HelpersMan.ToEnglishNumber(PurchasePhoneNumberIF.text.ToString())
                        + " \nItem Category: " + item.itemCategory.ToString()
                        + " \nItem Seller: " + item.itemSeller.ToString()
                        + " \nItem Price: " + item.itemPrice.ToString()
                        + " \nItem PostDate: " + item.itemPostDate
                        + " \nItem Seller Email: " + item.itemSellerEmail
                        + " \nItem Seller Username: " + item.itemSellerUsername;
                    MsgMan.SendMessageToAdmins(msgQuery, item.itemName+" Order");
                    //MsgMan.SendText(PurchasePhoneNumberIF.text, item.itemName + " Order", msgQuery);
                    HelpersMan.HidePanel(CheckOutItemPanel);
                    HelpersMan.HidePanel(cartItemDetailsPanel);
                    UpdateCartItems();
                    UpdateDashboardItems();
                }
            }
        }
    }
    public void ClosePurchaseItemPanelCallback()
    {
        HelpersMan.HidePanel(CheckOutItemPanel);
    }
    public void OpenSearchItemPanelBtnCallback()
    {
        // Prepare the UI 
        HideAllPanel();
        HelpersMan.ShowPanel(dashboardPanel);
        DeHighlightAllBtnContainers();
        HelpersMan.ShowPanel(searchItemPanel);
        HelpersMan.HighlightPanel(searchItemBtnContainerPanel);
        if (searchItemNameIF && searchItemPriceIF)
        {
            if (Globals.userLanguage == "AR")
            {
                searchItemNameIF.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("اسم السلعة ...");
                searchItemPriceIF.placeholder.GetComponent<Text>().text = ArabicFixer.Fix("سعر السلعة ... ");
            }
            else
            {
                searchItemNameIF.placeholder.GetComponent<Text>().text = "Item name ... ";
                searchItemPriceIF.placeholder.GetComponent<Text>().text = "Item price ... ";
            }
        }
    }
    public void SearchItemBtnCallback()
    {
        // Search logic
        HelpersMan.DestroyGameObjects(Globals.searchViewSlots);
        Globals.searchItems.Clear();
        Debug.Log("Searching items .... ");
        if (searchItemNameIF && searchItemPriceIF)
        {
            if (HelpersMan.CheckEmpty(searchItemNameIF) && HelpersMan.CheckEmpty(searchItemPriceIF))
            {
                Debug.Log("Please enter a search term ... ");
                if (Globals.userLanguage == "AR")
                {
                    ViewMessageOnScreen("من فضلك ادخل مصطلح للبحث ... ");
                }
                else
                {
                    ViewMessageOnScreen("Please enter a search term ... ");
                }
            }
            else
            {
                string itemName = searchItemNameIF.text.ToString();
                string itemPrice = "";
                if (Globals.userLanguage == "AR")
                {
                    itemPrice = HelpersMan.ToEnglishNumber(searchItemPriceIF.text.ToString());
                }
                else
                {
                    itemPrice = searchItemPriceIF.text.ToString();
                }
                // in case of empty item name
                if (itemName == "")
                {
                    if (HelpersMan.CheckNumeric(itemPrice))
                    {
                        for (int i = 0; i < Globals.dashboardItems.Count; i++)
                        {
                            if (Mathf.Floor((float)Globals.dashboardItems[i].itemPrice) <= float.Parse(itemPrice) + 10 && Mathf.Floor((float)Globals.dashboardItems[i].itemPrice) >= float.Parse(itemPrice) - 10)
                            {
                                Debug.Log("Found Item: " + Globals.dashboardItems[i].itemName);
                                Globals.searchItems.Add(Globals.dashboardItems[i]);
                            }
                        }
                        UpdateSearchItems();
                        return;
                    }
                    else
                    {
                        Debug.Log("Search Price must be a number .... ");
                        if (Globals.userLanguage == "AR")
                        {
                            ViewMessageOnScreen(ArabicFixer.Fix("السعر غير صالح ... "));
                        }
                        else
                        {
                            ViewMessageOnScreen("Search Price must be a number .... ");
                        }
                        return;
                    }
                }
                // in case of empty item price
                if (itemPrice == "")
                {
                    for (int i = 0; i < Globals.dashboardItems.Count; i++)
                    {
                        if (Globals.dashboardItems[i].itemName.Contains(itemName))
                        {
                            Debug.Log("Found Item: " + Globals.dashboardItems[i].itemName);
                            Globals.searchItems.Add(Globals.dashboardItems[i]);
                        }
                    }
                    UpdateSearchItems();
                    return;
                }
                // in case of both item name and price are filled with data
                for (int i = 0; i < Globals.dashboardItems.Count; i++)
                {
                    if (Globals.dashboardItems[i].itemName.Contains(itemName) || (Mathf.Floor((float)Globals.dashboardItems[i].itemPrice) <= float.Parse(itemPrice) + 10 && Mathf.Floor((float)Globals.dashboardItems[i].itemPrice) >= float.Parse(itemPrice) - 10))
                    {
                        Debug.Log("Found Item: " + Globals.dashboardItems[i].itemName);
                        Globals.searchItems.Add(Globals.dashboardItems[i]);
                    }
                }
                UpdateSearchItems();
                return;
            }
        }
    }
    public void OpenProfilePanelBtnCallback()
    {
        // Prepare the UI
        HideAllPanel();
        HelpersMan.ShowPanel(dashboardPanel);
        DeHighlightAllBtnContainers();
        HelpersMan.ShowPanel(userProfilePanel);
        HelpersMan.HighlightPanel(profileBtnContainerPanel);

        Debug.Log("User profile displayed ... ");
        //DBMan.LoadUserInfo();
        ChangeProfilePanelFieldsToArabic();
        //if (categoriesPanelMan && categoriesPanelMan.isTogglesLoaded)
        //{
        //    categoriesPanelMan.UpdateUserSelectedCategoriesCallback();
        //    categoriesPanelMan.isTogglesLoaded = false;
        //}
        if (categoriesPanelMan)
        {
            Globals.currentUser.userLanguage = categoriesPanelMan.userLanguageDD.options[categoriesPanelMan.userLanguageDD.value].text.ToString();
            Globals.userLanguage = Globals.currentUser.userLanguage;
        }
    }
    public void SwitchColorModesToggleCallback()
    {
        if (darkModeToggle)
        {
            if (darkModeToggle.isOn)
            {
                SwitchColors("Dark");
                //ViewMessageOnScreen("Saved To User Settings ... ");
            }
            else
            {
                SwitchColors("Normal");
                //ViewMessageOnScreen("Saved To User Settings ... ");
            }
        }
    }
    public void OnItemCategoryChangedDropDownCallback()
    {
        CategoriesAndTypesToggleInteraction();
    }

    #endregion
}
