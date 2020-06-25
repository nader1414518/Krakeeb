using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;

public class ItemSlot : MonoBehaviour
{
    public Item slotItem = new Item();
    //public GameObject itemDetailsPanel;

    bool dashboardItemsFixed = false;
    bool cartItemsFixed = false;

    public static void FixDashboardItems()
    {
        if (UIMan.itemDetailsPanel)
        {
            // Setting the item name 
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[0].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[0].text);
            // Setting the item Category
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[1].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[1].text);
            // Setting the item type
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[2].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[2].text);
            // Setting the item Description 
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[3].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[3].text);
            // Setting the item Price
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[4].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[4].text);
            // Setting the item quantity
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[5].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[5].text);
            // Setting the item seller
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[6].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[6].text);
            // Setting the item status 
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[7].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[7].text);
            // Setting the item post date
            UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[8].text = ArabicFixer.Fix(UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[8].text);
        }
    }

    public static void FixCartItems()
    {
        if (UIMan.cartItemDetailsPanel)
        {
            // Setting the item name 
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[0].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[0].text);
            // Setting the item Category
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[1].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[1].text);
            // Setting the item type
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[2].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[2].text);
            // Setting the item Description 
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[3].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[3].text);
            // Setting the item Price
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[4].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[4].text);
            // Setting the item quantity
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[5].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[5].text);
            // Setting the item seller
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[6].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[6].text);
            // Setting the item status 
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[7].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[7].text);
            // Setting the item post date
            UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[8].text = ArabicFixer.Fix(UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[8].text);
        }
    }

    void OnEnable()
    {
        FixDashboardItems();
        FixCartItems();
    }

    void Awake()
    {
        //itemDetailsPanel = GameObject.FindGameObjectWithTag("ItemDetailsPanel");
    }

    public void ViewCartItemDetails()
    {
        Debug.Log("Viewing item details ... ");
        HelpersMan.ShowPanel(UIMan.cartItemDetailsPanel);
        if (UIMan.cartItemDetailsPanel)
        {
            if (Globals.userLanguage == "AR")
            {
                // Setting the item name 
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[0].text = slotItem.itemName.ToString() + ArabicFixer.Fix("الاسم: ");
                // Setting the item Category
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[1].text = slotItem.itemCategory.ToString() + ArabicFixer.Fix("الفئة: ");
                // Setting the item type
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[2].text = slotItem.itemType.ToString() + ArabicFixer.Fix("النوع: ");
                // Setting the item Description 
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[3].text = slotItem.itemDescription.ToString() + ArabicFixer.Fix("الوصف: ");
                // Setting the item Price
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[4].text = " EGP." + slotItem.itemPrice.ToString() + ArabicFixer.Fix("السعر: ");
                // Setting the item quantity
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[5].text = slotItem.itemQuantity.ToString() + ArabicFixer.Fix("الكمية: ");
                // Setting the item seller
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[6].text = slotItem.itemSeller.ToString() + ArabicFixer.Fix("البائع: ");
                // Setting the item status 
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[7].text = slotItem.itemStatus.ToString() + ArabicFixer.Fix("الحالة: ");
                // Setting the item post date
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[8].text = slotItem.itemPostDate.ToString() + ArabicFixer.Fix("التاريخ: ");
                //FixCartItems();
                UIMan.itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item = slotItem.ShallowCopy();
                UIMan.cartItemDetailsPanel.GetComponent<CartItemDetailsPanelMan>().currentItem = slotItem.ShallowCopy();
            }
            else
            {
                // Setting the item name 
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[0].text = "Name: " + slotItem.itemName.ToString();
                // Setting the item Category
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[1].text = "Category: " + slotItem.itemCategory.ToString();
                // Setting the item type
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[2].text = "Type: " + slotItem.itemType.ToString();
                // Setting the item Description 
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[3].text = "Description: " + slotItem.itemDescription.ToString();
                // Setting the item Price
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[4].text = "Price: " + slotItem.itemPrice.ToString() + " EGP.";
                // Setting the item quantity
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[5].text = "Quantity: " + slotItem.itemQuantity.ToString();
                // Setting the item seller
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[6].text = "Seller: " + slotItem.itemSeller.ToString();
                // Setting the item status 
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[7].text = "Status: " + slotItem.itemStatus.ToString();
                // Setting the item post date
                UIMan.cartItemDetailsPanel.GetComponentsInChildren<Text>()[8].text = "Date: " + slotItem.itemPostDate.ToString();
                UIMan.itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item = slotItem.ShallowCopy();
                UIMan.cartItemDetailsPanel.GetComponent<CartItemDetailsPanelMan>().currentItem = slotItem.ShallowCopy();
            }
        }
    }



    public void ViewItemDetails()
    {
        Debug.Log("Viewing item details ... ");
        HelpersMan.ShowPanel(UIMan.itemDetailsPanel);
        if (UIMan.itemDetailsPanel)
        {
            if (Globals.userLanguage == "AR")
            {
                // Setting the item name 
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[0].text = slotItem.itemName.ToString() + ArabicFixer.Fix("الاسم: ");
                // Setting the item Category
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[1].text = slotItem.itemCategory.ToString() + ArabicFixer.Fix("الفئة: ");
                // Setting the item type
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[2].text = slotItem.itemType.ToString() + ArabicFixer.Fix("النوع: ");
                // Setting the item Description 
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[3].text = slotItem.itemDescription.ToString() + ArabicFixer.Fix("الوصف: ");
                // Setting the item Price
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[4].text = " EGP." + slotItem.itemPrice.ToString() + ArabicFixer.Fix("السعر: ");
                // Setting the item quantity
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[5].text = slotItem.itemQuantity.ToString() + ArabicFixer.Fix("الكمية: ");
                // Setting the item seller
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[6].text = slotItem.itemSeller.ToString() + ArabicFixer.Fix("البائع: ");
                // Setting the item status 
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[7].text = slotItem.itemStatus.ToString() + ArabicFixer.Fix("الحالة: ");
                // Setting the item post date
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[8].text = slotItem.itemPostDate.ToString() + ArabicFixer.Fix("التاريخ: ");
                //FixDashboardItems();
                UIMan.itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item = slotItem;
                UIMan.itemDetailsPanel.GetComponent<ItemDetailsPanelMan>().currentItem = slotItem;
            }
            else
            {
                // Setting the item name 
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[0].text = "Name: " + slotItem.itemName.ToString();
                // Setting the item Category
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[1].text = "Category: " + slotItem.itemCategory.ToString();
                // Setting the item type
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[2].text = "Type: " + slotItem.itemType.ToString();
                // Setting the item Description 
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[3].text = "Description: " + slotItem.itemDescription.ToString();
                // Setting the item Price
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[4].text = "Price: " + slotItem.itemPrice.ToString() + " EGP.";
                // Setting the item quantity
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[5].text = "Quantity: " + slotItem.itemQuantity.ToString();
                // Setting the item seller
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[6].text = "Seller: " + slotItem.itemSeller.ToString();
                // Setting the item status 
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[7].text = "Status: " + slotItem.itemStatus.ToString();
                // Setting the item post date
                UIMan.itemDetailsPanel.GetComponentsInChildren<Text>()[8].text = "Date: " + slotItem.itemPostDate.ToString();
                UIMan.itemSnapshotsPanel.GetComponent<ItemSnapshotPanelMan>().item = slotItem;
                UIMan.itemDetailsPanel.GetComponent<ItemDetailsPanelMan>().currentItem = slotItem;
            }
        }
    }
}
