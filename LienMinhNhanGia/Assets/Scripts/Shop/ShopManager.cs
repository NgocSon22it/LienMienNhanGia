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

    [Header("Item")]
    [SerializeField] GameObject MainItem;
    [SerializeField] Transform Content;

    [Header("Selected Item")]
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text ItemNameTxt;
    [SerializeField] TMP_Text ItemCoinTxt;
    [SerializeField] TMP_Text ItemDescriptionTxt;
    [SerializeField] GameObject ItemCanBuyPanel;
    [SerializeField] GameObject ItemNotBuyPanel;
    [SerializeField] TMP_Text BuyItemErrorTxt;

    [Header("Skill")]
    [SerializeField] GameObject SkillItem;
    [SerializeField] Transform SkillContent;

    [Header("Selected Skill")]
    [SerializeField] Image SkillImage;
    [SerializeField] TMP_Text SkillNameTxt;
    [SerializeField] TMP_Text SkillCoinTxt;
    [SerializeField] TMP_Text SkillDescriptionTxt;
    [SerializeField] GameObject SkillCanBuyPanel;
    [SerializeField] GameObject SkillNotBuyPanel;
    [SerializeField] TMP_Text BuySkillErrorTxt;


    [Header("Account Information")]
    [SerializeField] TMP_Text AccountCoinTxt;

    [Header("All Tab Panel")]
    [SerializeField] List<GameObject> AllTabPanel;

    string ItemExtension = "Item/";
    string SkillExtension = "Skill/";

    public ItemEntity MainItemSelected;
    public SkillEntity SkillSelected;

    private void Awake()
    {
        Instance = this;
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

    public void LoadShopSkillList()
    {
        AccountCoinTxt.text = AccountManager.Account.Coin.ToString();
        foreach (Transform trans in SkillContent)
        {
            Destroy(trans.gameObject);
        }

        foreach (SkillEntity Skill in GetDataManager.ListShopSkill)
        {
            Instantiate(SkillItem, SkillContent).GetComponent<ShopSkill>().SetUp(Skill);
        }
    }

    public void SetUpSelectedMainItem(ItemEntity mainItem)
    {
        MainItemSelected = mainItem;
        ItemImage.sprite = Resources.Load<Sprite>(ItemExtension + mainItem.ItemID);
        ItemNameTxt.text = mainItem.ItemName;
        ItemCoinTxt.text = mainItem.ItemCoin.ToString();
        ItemDescriptionTxt.text = mainItem.Description;
        ResetErrorMessage();
        if (CheckMainItemOwned())
        {
            ItemCanBuyPanel.gameObject.SetActive(false);
            ItemNotBuyPanel.gameObject.SetActive(true);
        }
        else
        {
            ItemCanBuyPanel.gameObject.SetActive(true);
            ItemNotBuyPanel.gameObject.SetActive(false);
        }
    }
    public void SetUpSelectedSkill(SkillEntity skillEntity)
    {
        SkillSelected = skillEntity;
        SkillImage.sprite = Resources.Load<Sprite>(SkillExtension + skillEntity.SkillID);
        SkillNameTxt.text = skillEntity.Name;
        SkillCoinTxt.text = skillEntity.Coin.ToString();
        SkillDescriptionTxt.text = skillEntity.Description;
        ResetErrorMessage();
        if (CheckSkillOwned())
        {
            SkillCanBuyPanel.gameObject.SetActive(false);
            SkillNotBuyPanel.gameObject.SetActive(true);
        }
        else
        {
            SkillCanBuyPanel.gameObject.SetActive(true);
            SkillNotBuyPanel.gameObject.SetActive(false);
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

    public bool CheckSkillOwned()
    {
        foreach (AccountSkillEntity skillEntity in AccountManager.ListAccountSkill)
        {
            if (skillEntity.SkillID.Equals(SkillSelected.SkillID))
            {
                return true;
            }
        }
        return false;
    }

    public void BuySelectedMainItem()
    {
        if (AccountManager.Account.Coin >= SkillSelected.Coin)
        {
            new ItemDAO().BuyItem(AccountManager.AccountID, MainItemSelected.ItemID, 1);
            AccountManager.UpdateListAccountItem();
            AccountManager.Account.Coin -= MainItemSelected.ItemCoin;
            LoadShopMainItemList();
            SetUpSelectedMainItem(MainItemSelected);
            BuyItemErrorTxt.text = "";
        }
        else
        {
            BuyItemErrorTxt.text = "Bạn không đủ xu để mua!";
        }
    }
    public void BuySelectedSkill()
    {
        if (AccountManager.Account.Coin >= SkillSelected.Coin)
        {
            new SkillDAO().BuySkill(AccountManager.AccountID, SkillSelected.SkillID);
            AccountManager.UpdateListAccountSkill();
            AccountManager.Account.Coin -= SkillSelected.Coin;
            LoadShopSkillList();
            SetUpSelectedSkill(SkillSelected);

            BuySkillErrorTxt.text = "";
        }
        else
        {
            BuySkillErrorTxt.text = "Bạn không đủ xu để mua!";
        }



    }
    public void OpenTabPanel(string tabName)
    {
        for (int i = 0; i < AllTabPanel.Count; i++)
        {
            AllTabPanel[i].SetActive(false);
        }

        foreach (GameObject obj in AllTabPanel)
        {
            if (obj.name == tabName)
            {
                obj.gameObject.SetActive(true);
            }
        }

        switch (tabName)
        {
            case "Item":
                UpdateSelected();
                LoadShopMainItemList();
                ResetErrorMessage();
                break;
            case "Skill":
                UpdateSelected();
                LoadShopSkillList();
                ResetErrorMessage();
                break;
        }
    }

    public void UpdateSelected()
    {
        if (GetDataManager.ListShopItem.Count > 0 && GetDataManager.ListShopItem.Count > 0)
        {
            SetUpSelectedMainItem(GetDataManager.ListShopItem[0]);
            SetUpSelectedSkill(GetDataManager.ListShopSkill[0]);
        }
        
    }

    public void ResetErrorMessage()
    {
        BuyItemErrorTxt.text = "";
        BuySkillErrorTxt.text = "";
    }


}
