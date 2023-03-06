using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSkill : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image SkillImage;
    [SerializeField] GameObject SelectedSquare;


    SkillEntity SkillEntity;

    string Extension = "Skill/";

    public void OnPointerDown(PointerEventData eventData)
    {
        ShopManager.Instance.SetUpSelectedSkill(SkillEntity);
        ShopManager.Instance.LoadShopSkillList();
    }

    public void SetUp(SkillEntity skillEntity)
    {
        SkillEntity = skillEntity;
        SkillImage.sprite = Resources.Load<Sprite>(Extension + skillEntity.SkillID);
        SetUpSelected();
    }

    public void SetUpSelected()
    {
        if (ShopManager.Instance.SkillSelected.SkillID.Equals(SkillEntity.SkillID))
        {
            SelectedSquare.SetActive(true);
        }
        else
        {
            SelectedSquare.SetActive(false);
        }
    }
}
