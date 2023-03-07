using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Slot : MonoBehaviour, IPointerClickHandler
{
    SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] int SlotIndex;

    [SerializeField] Image ImageUI; 
    [SerializeField] TMP_Text CostUI;

    [Header("Slot Manager")]
    [SerializeField] GameObject Empty;
    [SerializeField] GameObject Full;  

    [Header("Extension")]
    string Extension = "Skill/";
    public void OnPointerClick(PointerEventData eventData)
    {
        if (SkillManager.Instance.SkillSelected != null)
        {
            EquipSlot(SkillManager.Instance.SkillSelected);

            SkillManager.Instance.LoadAccountSkillList();
            SkillManager.Instance.LoadAccountSkillSlot();
        }
    }

    public void EquipSlot(SkillEntity skill)
    {
        if (Skill == null)
        {
           new Account_SkillDAO().UpdateAccountSkillSlotIndex(AccountManager.AccountID, skill.SkillID, SlotIndex);
        }
        else
        {
            AccountSkillEntity CurrentSkill = new Account_SkillDAO().GetAccountSkillbySkillID(AccountManager.AccountID, Skill.SkillID);
            AccountSkillEntity SelectSkill = new Account_SkillDAO().GetAccountSkillbySkillID(AccountManager.AccountID, skill.SkillID);

            int SlotCheck = CurrentSkill.SlotIndex;
            new Account_SkillDAO().UpdateAccountSkillSlotIndex(AccountManager.AccountID, CurrentSkill.SkillID, SelectSkill.SlotIndex);
            new Account_SkillDAO().UpdateAccountSkillSlotIndex(AccountManager.AccountID, SelectSkill.SkillID, SlotCheck);
        }

        SetUpSlot();
    }

    public void UnEquipSlot()
    {
        if (Skill != null)
        {
            new Account_SkillDAO().UpdateAccountSkillSlotIndex(AccountManager.AccountID, Skill.SkillID, 0);
        }

        SetUpSlot();
        SkillManager.Instance.LoadAccountSkillList();
    }

    public void SetUpSlot()
    {
        AccountSkillEntity accountSkillEntity =
        new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, SlotIndex);

        Skill_Hold_Manager.Instance.SetUpSkill(SlotIndex);

        if (accountSkillEntity != null)
        {
            SkillEntity skillEntity = new SkillDAO().GetSkillbyID(accountSkillEntity.SkillID);
            ImageUI.gameObject.SetActive(true);
            CostUI.gameObject.SetActive(true);

            Skill = skillEntity;
            CostUI.text = (Skill.Chakra - accountSkillEntity.CurrentLevel).ToString();
            ImageUI.sprite = Resources.Load<Sprite>(Extension + Skill.SkillID);
            SkillImage.sprite = Resources.Load<Sprite>(Extension + Skill.SkillID);         
            SetUpStatusPanel(false, true);
        }
        else
        {
            Skill = null;
            ImageUI.gameObject.SetActive(false);
            CostUI.gameObject.SetActive(false);
            SkillImage.sprite = null;          
            SetUpStatusPanel(true, false);
        }

    }

    public void SetUpStatusPanel(bool EmptyStatus, bool FullStatus)
    {
        Empty.SetActive(EmptyStatus);
        Full.SetActive(FullStatus);
    }

}
