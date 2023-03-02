using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerBag_MainItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text ItemAmountTxt;
    [SerializeField] GameObject HoverPanel;

    ItemEntity ItemEntity;
    string ItemExtension = "Item/";

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverPanel.SetActive(false);
    }

    public void SetUp(ItemEntity itemEntity)
    {
        ItemEntity = itemEntity;
        ItemImage.sprite = Resources.Load<Sprite>(ItemExtension + itemEntity.ItemID);
    }

    public void OnClick_ShowInformation()
    {
        PlayerBagManager.Instance.ShowSelectedItemInformation(ItemEntity);
        PlayerBagManager.Instance.ResetItemInformationData();
    }


}
