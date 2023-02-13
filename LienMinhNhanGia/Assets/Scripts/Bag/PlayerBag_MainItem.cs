using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBag_MainItem : MonoBehaviour
{
    [SerializeField] Image ItemImage;
    [SerializeField] TMP_Text ItemAmountTxt;

    ItemEntity ItemEntity;

    public void SetUp(ItemEntity itemEntity)
    {
        ItemEntity = itemEntity;
        ItemImage.sprite = Resources.Load<Sprite>(itemEntity.ItemID);
    }
}
