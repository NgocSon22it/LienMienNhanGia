using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Slot : MonoBehaviour, IPointerClickHandler
{
    SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] int SlotIndex;

    [Header("Slot Manager")]
    [SerializeField] GameObject Empty;
    [SerializeField] GameObject Full;  

    [Header("DAO")]
    [SerializeField] GameObject DAOManager;

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
            DAOManager.GetComponent<Account_SkillDAO>().UpdateAccountSkillSlotIndex(AccountManager.AccountID, skill.SkillID, SlotIndex);
        }
        else
        {
            AccountSkillEntity CurrentSkill = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySkillID(AccountManager.AccountID, Skill.SkillID);
            AccountSkillEntity SelectSkill = DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySkillID(AccountManager.AccountID, skill.SkillID);

            int SlotCheck = CurrentSkill.SlotIndex;
            DAOManager.GetComponent<Account_SkillDAO>().UpdateAccountSkillSlotIndex(AccountManager.AccountID, CurrentSkill.SkillID, SelectSkill.SlotIndex);
            DAOManager.GetComponent<Account_SkillDAO>().UpdateAccountSkillSlotIndex(AccountManager.AccountID, SelectSkill.SkillID, SlotCheck);
        }

        SetUpSlot();
    }

    public void UnEquipSlot()
    {
        if (Skill != null)
        {
            DAOManager.GetComponent<Account_SkillDAO>().UpdateAccountSkillSlotIndex(AccountManager.AccountID, Skill.SkillID, 0);
        }

        SetUpSlot();
        SkillManager.Instance.LoadAccountSkillList();
    }

    public void SetUpSlot()
    {
        AccountSkillEntity accountSkillEntity =
        DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, SlotIndex);
        Skill_Hold_Manager.Instance.SetUpSkill(accountSkillEntity, SlotIndex);
        if (accountSkillEntity != null)
        {
            SkillEntity skillEntity = DAOManager.GetComponent<SkillDAO>().GetSkillbyID(accountSkillEntity.SkillID);

            Skill = skillEntity;
            SkillImage.sprite = Resources.Load<Sprite>(Extension + Skill.SkillID);

            SetUpStatusPanel(false, true);
        }
        else
        {
            Skill = null;
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
