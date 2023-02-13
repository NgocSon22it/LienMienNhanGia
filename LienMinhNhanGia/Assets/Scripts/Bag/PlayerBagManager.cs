using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBagManager : MonoBehaviour
{
    [Header("Instance")]
    public static PlayerBagManager Instance;

    [SerializeField] GameObject MainItem;
    [SerializeField] Transform Content;


    [Header("DAOManager")]
    [SerializeField] GameObject DAOManager;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadAccountItem()
    {     
        AccountManager.ListAccountItem = 
        DAOManager.GetComponent<Account_ItemDAO>().GetAllItemForAccount(AccountManager.AccountID);
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountItemEntity Item in AccountManager.ListAccountItem)
        {
            ItemEntity itemEntity = DAOManager.GetComponent<ItemDAO>().GetItembyId(Item.ItemID);
            Instantiate(MainItem, Content).GetComponent<PlayerBag_MainItem>().SetUp(itemEntity);
        }

    }

    public void InitialManager()
    {
        LoadAccountItem();
    }

}
