using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Online_SkillSlot : MonoBehaviour, IPointerClickHandler
{
    SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] int SlotIndex;
    [SerializeField] KeyCode SlotKey;

    [Header("Slot Manager")]
    [SerializeField] GameObject Empty;
    [SerializeField] GameObject Full;

    [Header("DAO")]
    [SerializeField] GameObject DAOManager;

    [Header("Extension")]
    string Extension = "Skill/";

    private void Start()
    {
        SetUpSlot();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Online_SkillManager.Instance.SkillSelected != null)
        {
            Online_EquipSlot(Online_SkillManager.Instance.SkillSelected);
            Online_SkillManager.Instance.LoadAccountSkillList();
            Online_SkillManager.Instance.LoadAccountSkillSlot();
        }
    }

    public void SetUpSlot()
    {
        AccountSkillEntity accountSkillEntity =
        DAOManager.GetComponent<Account_SkillDAO>().GetAccountSkillbySlotIndex(AccountManager.AccountID, SlotIndex);

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

    public void Online_EquipSlot(SkillEntity skill)
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
        LobbyManager.Instance.SetUpAccountData();
    }

    public void Online_UnEquipSlot()
    {
        if (Skill != null)
        {
            DAOManager.GetComponent<Account_SkillDAO>().UpdateAccountSkillSlotIndex(AccountManager.AccountID, Skill.SkillID, 0);
        }

        SetUpSlot();
        Online_SkillManager.Instance.LoadAccountSkillList();
        LobbyManager.Instance.SetUpAccountData();
    }
}
