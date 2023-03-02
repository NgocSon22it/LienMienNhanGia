using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Instance")]
    public static ShopManager Instance;

    [Header("DAOManager")]
    [SerializeField] GameObject DAOManager;


    [SerializeField] GameObject MainItem;
    [SerializeField] Transform Content;

    [Header("Selected Item")]
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text ItemNameTxt;
    [SerializeField] TMP_Text ItemCoinTxt;
    [SerializeField] TMP_Text ItemDescriptionTxt;
    [SerializeField] GameObject CanBuyPanel;
    [SerializeField] GameObject NotBuyPanel;

    [Header("Account Information")]
    [SerializeField] TMP_Text AccountCoinTxt;

    string ItemExtension = "Item/";

    public ItemEntity MainItemSelected;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetUpSelectedMainItem(GetDataManager.ListShopItem[0]);
        LoadShopMainItemList();      
    }

    public void LoadShopMainItemList()
    {
        AccountCoinTxt.text = AccountManager.Account.Coin.ToString();      
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (ItemEntity Item in GetDataManager.ListShopItem)
        {
            Instantiate(MainItem, Content).GetComponent<ShopMainItem>().SetUp(Item);
        }
    }

    public void SetUpSelectedMainItem(ItemEntity mainItem)
    {
        MainItemSelected = mainItem;
        ItemImage.sprite = Resources.Load<Sprite>(ItemExtension + mainItem.ItemID);
        ItemNameTxt.text = mainItem.ItemName;
        ItemCoinTxt.text = mainItem.ItemCoin.ToString();
        ItemDescriptionTxt.text = mainItem.Description;

        if (CheckMainItemOwned())
        {
            CanBuyPanel.gameObject.SetActive(false);
            NotBuyPanel.gameObject.SetActive(true);
        }
        else
        {
            CanBuyPanel.gameObject.SetActive(true);
            NotBuyPanel.gameObject.SetActive(false);
        }
    }

    public bool CheckMainItemOwned()
    {
        foreach (AccountItemEntity itemEntity in AccountManager.ListAccountItem)
        {
            if (itemEntity.ItemID.Equals(MainItemSelected.ItemID))
            {
                return true;
            }
        }
        return false;
    }

    public void BuySelectedMainItem()
    {
        
        DAOManager.GetComponent<ItemDAO>().BuyItem(AccountManager.AccountID, MainItemSelected.ItemID, 1);
        AccountManager.UpdateListAccountItem();
        AccountManager.Account.Coin -= MainItemSelected.ItemCoin;
        LoadShopMainItemList();
        SetUpSelectedMainItem(MainItemSelected);
    }

}
