using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Slot : MonoBehaviour, IPointerClickHandler
{
    SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] int SlotIndex;
    [SerializeField] KeyCode SlotKey;

    [Header("Slot Manager")]
    [SerializeField] GameObject Empty;
    [SerializeField] GameObject Full;

    [Header("DAO")]
    Account_SkillDAO account_SkillDAO;
    SkillDAO skillDAO;

    private void Start()
    {
        account_SkillDAO = GetComponentInParent<Account_SkillDAO>();
        skillDAO = GetComponentInParent<SkillDAO>();
        SetUpSlot();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SkillManager.SkillSelected != null)
        {
            EquipSlot(SkillManager.SkillSelected);

            SkillManager.Instance.LoadAccountSkillList();
            SkillManager.Instance.LoadAccountSkillSlot();
        }
    }

    private void Update()
    {
        ExecuteSkill();
    }

    public void ExecuteSkill()
    {
        if (Skill == null) return;

        if (Input.GetKeyDown(SlotKey))
        {
            Skill_Hold_Manager.Instance.CallMethodFromHold(Skill.MethodName);
        }
    }

    public void EquipSlot(SkillEntity skill)
    {
        if(Skill == null)
        {
            account_SkillDAO.UpdateSlotIndex(skill.Id, SlotIndex);           
        }
        else
        {
            AccountSkillEntity CurrentSkill = account_SkillDAO.GetAccountSkillbyId(Skill.Id);
            AccountSkillEntity SelectSkill = account_SkillDAO.GetAccountSkillbyId(skill.Id);

            int SlotCheck = CurrentSkill.SlotIndex;
            account_SkillDAO.UpdateSlotIndex(CurrentSkill.Id, SelectSkill.SlotIndex);
            account_SkillDAO.UpdateSlotIndex(SelectSkill.Id, SlotCheck);        
        }

        SetUpSlot();
    }

    public void UnEquipSlot()
    {
        if(Skill != null)
        {
            account_SkillDAO.UpdateSlotIndex(Skill.Id, 0);
        }

        SetUpSlot();
        SkillManager.Instance.LoadAccountSkillList();
    }

    public void SetUpSlot()
    {
        AccountSkillEntity accountSkillEntity = account_SkillDAO.GetAccountSkillbySlotIndex(SlotIndex);

        if (accountSkillEntity != null)
        {
            SkillEntity skillEntity = skillDAO.GetSkillbyID(accountSkillEntity.Id);

            Skill = skillEntity;
            SkillImage.sprite = Skill.SkillImage;

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
