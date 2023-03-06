using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBagManager : MonoBehaviour
{
    [Header("Instance")]
    public static PlayerBagManager Instance;

    [SerializeField] GameObject MainItem;
    [SerializeField] Transform Content;

    [Header("Item Information")]
    [SerializeField] GameObject InformationPanel;
    [SerializeField] TMP_Text ItemNameTxt;
    [SerializeField] TMP_Text ItemDescriptionTxt;
    [SerializeField] Image ItemImage;
    [SerializeField] Scrollbar scrollbar;


    string ItemExtension = "Item/";

    private void Awake()
    {
        Instance = this;
    }

    public void LoadAccountItem()
    {
        AccountManager.UpdateListAccountItem();
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountItemEntity Item in AccountManager.ListAccountItem)
        {
            ItemEntity itemEntity = new ItemDAO().GetItembyId(Item.ItemID);
            Instantiate(MainItem, Content).GetComponent<PlayerBag_MainItem>().SetUp(itemEntity);
        }
    }

    public void InitialManager()
    {
        LoadAccountItem();
    }

    public void ShowSelectedItemInformation(ItemEntity itemEntity)
    {
        InformationPanel.SetActive(true);
        ItemNameTxt.text = itemEntity.ItemName;
        ItemDescriptionTxt.text = itemEntity.Description;
        ItemImage.sprite = Resources.Load<Sprite>(ItemExtension + itemEntity.ItemID);

    }

    public void ResetBagData()
    {
        InformationPanel.SetActive(false);      
    }

    public void ResetItemInformationData()
    {
        scrollbar.value = 1f;
    }
}
