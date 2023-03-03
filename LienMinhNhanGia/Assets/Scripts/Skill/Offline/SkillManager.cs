using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [Header("Instance")]
    public static SkillManager Instance;

    [SerializeField] GameObject SkillItem;
    [SerializeField] Transform Content;

    [Header("SHOW HOVER SKILL")]
    [SerializeField] Image SkillImage;
    [SerializeField] TMP_Text SkillDescription;
    [SerializeField] GameObject InformationPanel;

    [Header("SKILL UPGRADE")]
    [SerializeField] TMP_Text CurrentLevel;
    [SerializeField] TMP_Text CurrentDamage;
    [SerializeField] TMP_Text CurrentChakra;
    [SerializeField] TMP_Text CurrentCooldown;

    [Header("CAN UPGRADE")]
    [SerializeField] TMP_Text NextLevel;
    [SerializeField] TMP_Text NextDamage;
    [SerializeField] TMP_Text NextChakra;
    [SerializeField] TMP_Text NextCooldown;
    [SerializeField] TMP_Text UpgradeCost;

    [Header("UPGRADE MANAGER")]
    [SerializeField] GameObject CanUpgradePanel;
    [SerializeField] GameObject MaxLevelPanel;

    [Header("SKILL SLOT MANAGER")]
    [SerializeField] List<Skill_Slot> listSkillSlot = new List<Skill_Slot>();

    public SkillEntity SkillSelected;
    int MaxSkillLevel = 3;

    bool StatusEquip;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadAccountSkillList();
        foreach (Skill_Slot skill in listSkillSlot)
        {
            skill.SetUpSlot();
        }
    }

    public void LoadAccountSkillList()
    {
        AccountManager.UpdateListAccountSkill();
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountSkillEntity Skill in AccountManager.ListAccountSkill)
        {
            SkillEntity skillentity = new SkillDAO().GetSkillbyID(Skill.SkillID);

            StatusEquip = Skill.SlotIndex != 0 ? true : false;
            Instantiate(SkillItem, Content).GetComponent<Skill_Item>().SetUp(skillentity, StatusEquip);
        }
    }

    public void LoadAccountSkillSlot()
    {
        foreach (Skill_Slot slot in listSkillSlot)
        {
            slot.SetUpSlot();
        }
    }


    public void ShowInformationSelectedSkill(SkillEntity skill)
    {
        InformationPanel.SetActive(true);
        SkillSelected = skill;
        AccountSkillEntity accountSkillEntity = new Account_SkillDAO().GetAccountSkillbySkillID(AccountManager.AccountID , skill.SkillID);
        SkillImage.sprite = Resources.Load<Sprite>("Skill/" + skill.SkillID);
        SkillDescription.text = skill.Description;
        CurrentDamage.text = (accountSkillEntity.CurrentLevel * skill.Damage).ToString();
        CurrentChakra.text = skill.Chakra.ToString();
        CurrentLevel.text = "Level: " + accountSkillEntity.CurrentLevel.ToString();
        LoadAccountSkillList();
        SetUpStatusForUpgrade(skill);
    }

    public void SetUpStatusForUpgrade(SkillEntity skill)
    {
        AccountSkillEntity accountSkillEntity = new Account_SkillDAO().GetAccountSkillbySkillID(AccountManager.AccountID, skill.SkillID);
        if (accountSkillEntity.CurrentLevel < MaxSkillLevel)
        {
            MaxLevelPanel.SetActive(false);
            CanUpgradePanel.SetActive(true);
            NextLevel.text = "Level: " + (accountSkillEntity.CurrentLevel + 1).ToString();
            NextDamage.text = (skill.Damage + (skill.Damage * 30 / 100)).ToString();
            NextChakra.text = (skill.Chakra - (skill.Chakra * 30 / 100)).ToString();
            NextCooldown.text = (skill.Cooldown - (skill.Cooldown * 30 / 100)).ToString();
        }
        else
        {
            MaxLevelPanel.SetActive(true);
            CanUpgradePanel.SetActive(false);

        }
    }

    /*public void UpgradeSelectedSkill(SkillEntity skill)
    {
        skill.Damage += skill.Damage * 30 / 100;
        skill.Chakra -= (skill.Chakra * 30 / 100);
        skill.Level += 1;

        CurrentLevel.text = "Level " + skill.Level;
        CurrentDamage.text = skill.Damage.ToString();
        CurrentChakra.text = skill.Chakra.ToString();

        SetUpStatusForUpgrade(skill);
        LoadAccountSkillList();
    }*/

    /*public void UpgradeDisplaySkill()
    {
        UpgradeSelectedSkill(SkillSelected);
    }*/

    public void SetUpSelectedSkill(SkillEntity Skill)
    {
        if (Skill != null)
        {
            SkillSelected = Skill;
            LoadAccountSkillList();
        }
    }

}








