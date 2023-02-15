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
    [SerializeField] GameObject SelectedSquare;
    ItemEntity MainItemSelected;
    [SerializeField] GameObject CanBuyPanel;
    [SerializeField] GameObject NotBuyPanel;

    [Header("Account Information")]
    [SerializeField] TMP_Text AccountCoinTxt;

    List<ItemEntity> ListMainItem = new List<ItemEntity>();

    private void Awake()
    {
        Instance = this;
    }

    public void LoadShopMainItemList()
    {
        AccountCoinTxt.text = AccountManager.AccountCoin.ToString();
        ListMainItem = DAOManager.GetComponent<ItemDAO>().GetAllItem();        
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (ItemEntity Item in ListMainItem)
        {
            Instantiate(MainItem, Content).GetComponent<ShopMainItem>().SetUp(Item);
        }

    }

    public void SetUpSelectedMainItem(ItemEntity mainItem)
    {
        MainItemSelected = mainItem;
        ItemImage.sprite = Resources.Load<Sprite>(mainItem.ItemID);
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

    public void SetUpSelectedSquare(Vector3 transform)
    {
        SelectedSquare.transform.position = transform;
    }

    public void BuySelectedMainItem()
    {
        DAOManager.GetComponent<ItemDAO>().BuyItem(AccountManager.AccountID, MainItemSelected.ItemID, 1);
        LoadShopMainItemList();
        SetUpSelectedMainItem(MainItemSelected);

    }

    public void InitialManager()
    {
        LoadShopMainItemList();
        if (ListMainItem.Count > 0)
        {
            SetUpSelectedMainItem(ListMainItem[0]);
        }
    }


}
