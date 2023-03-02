using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMainItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image ItemImage;
    [SerializeField] GameObject SelectedSquare;


    ItemEntity ItemEntity;

    string ItemExtension = "Item/";

    public void OnPointerDown(PointerEventData eventData)
    {
        ShopManager.Instance.SetUpSelectedMainItem(ItemEntity);
        ShopManager.Instance.LoadShopMainItemList();
    }

    public void SetUp(ItemEntity itemEntity)
    {
        ItemEntity = itemEntity;
        ItemImage.sprite = Resources.Load<Sprite>(ItemExtension + itemEntity.ItemID);
        SetUpSelected();
    }

    public void SetUpSelected()
    {
        if (ShopManager.Instance.MainItemSelected.ItemID.Equals(ItemEntity.ItemID))
        {
            SelectedSquare.SetActive(true);
        }
        else
        {
            SelectedSquare.SetActive(false);
        }
    }
}
