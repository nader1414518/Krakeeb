using System;
using UnityEngine;


[Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public string itemDescription;
    public string itemCategory;
    public double itemPrice;
    public string itemSeller;
    public string itemPostDate;
    public string itemStatus;
    public int itemQuantity;
    public string itemImagePath;
    public string itemImageDownloadUrl;
    public string itemId;
    public string itemSellerEmail;
    public string itemSellerUsername;
    public int itemViews;

    public Item ShallowCopy()
    {
        return (Item)this.MemberwiseClone();
    }

    public Item()
    {
        itemName = "";
        itemType = "";
        itemDescription = "";
        itemCategory = "";
        itemPrice = 0;
        itemSeller = "";
        itemPostDate = "";
        itemStatus = "";
        itemQuantity = 0;
        itemImagePath = "";
        itemSellerEmail = "";
        itemSellerUsername = "";
    }

    public Item(string itemName, string itemType, string itemDescription, string itemCategory, double itemPrice, string itemSeller, string itemPostDate, string itemStatus, int itemQuantity, string itemImagePath)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.itemCategory = itemCategory;
        this.itemPrice = itemPrice;
        this.itemSeller = itemSeller;
        this.itemPostDate = itemPostDate;
        this.itemStatus = itemStatus;
        this.itemQuantity = itemQuantity;
        this.itemImagePath = itemImagePath;
        this.itemId = this.itemName + this.itemPostDate.Replace("/", "").Replace("\\", "").Replace(".", "");
    }

    public Item(string itemName, string itemType, string itemDescription, string itemCategory, double itemPrice, string itemSeller, string itemPostDate, string itemStatus, int itemQuantity, string itemImagePath, string itemSellerEmail)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.itemCategory = itemCategory;
        this.itemPrice = itemPrice;
        this.itemSeller = itemSeller;
        this.itemPostDate = itemPostDate;
        this.itemStatus = itemStatus;
        this.itemQuantity = itemQuantity;
        this.itemImagePath = itemImagePath;
        this.itemId = this.itemName + this.itemPostDate.Replace("/", "").Replace("\\", "").Replace(".", "");
        this.itemSellerEmail = itemSellerEmail;
    }
    public Item(string itemName, string itemType, string itemDescription, string itemCategory, double itemPrice, string itemSeller, string itemPostDate, string itemStatus, int itemQuantity, string itemImagePath, string itemSellerEmail, string itemSellerUsername)
    {
        this.itemName = itemName;
        this.itemType = itemType;
        this.itemDescription = itemDescription;
        this.itemCategory = itemCategory;
        this.itemPrice = itemPrice;
        this.itemSeller = itemSeller;
        this.itemPostDate = itemPostDate;
        this.itemStatus = itemStatus;
        this.itemQuantity = itemQuantity;
        this.itemImagePath = itemImagePath;
        this.itemId = this.itemName + this.itemPostDate.Replace("/", "").Replace("\\", "").Replace(".", "");
        this.itemSellerEmail = itemSellerEmail;
        this.itemSellerUsername = itemSellerUsername;
    }
}
