using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMainItem : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image ItemImage;

    ItemEntity ItemEntity;

    public void OnPointerDown(PointerEventData eventData)
    {
        ShopManager.Instance.SetUpSelectedMainItem(ItemEntity);
        ShopManager.Instance.SetUpSelectedSquare(transform.position);
    }

    public void SetUp(ItemEntity itemEntity)
    {
        ItemEntity = itemEntity;
        ItemImage.sprite = Resources.Load<Sprite>(itemEntity.ItemID);
    }


}
