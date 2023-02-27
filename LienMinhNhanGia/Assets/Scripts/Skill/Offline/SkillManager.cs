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

    /*[Header("SHOW HOVER SKILL")]
    [SerializeField] Image SkillImage;
    [SerializeField] TMP_Text SkillDescription;

    [Header("SKILL UPGRADE")]
    [SerializeField] TMP_Text CurrentLevel;
    [SerializeField] TMP_Text CurrentDamage;
    [SerializeField] TMP_Text CurrentChakra;

    [Header("CAN UPGRADE")]
    [SerializeField] TMP_Text NextLevel;
    [SerializeField] TMP_Text NextDamage;
    [SerializeField] TMP_Text NextChakra;
    [SerializeField] TMP_Text UpgradeCost;

    [Header("UPGRADE MANAGER")]
    [SerializeField] GameObject CanUpgradePanel;
    [SerializeField] GameObject MaxLevelPanel;
    [SerializeField] GameObject UpgradePetPanel;*/

    [Header("SKILL SLOT MANAGER")]
    List<AccountSkillEntity> ListAccountSkill = new List<AccountSkillEntity>();
    [SerializeField] List<Skill_Slot> listSkillSlot = new List<Skill_Slot>();

    [SerializeField] GameObject SelectedSkillCircle;

    public SkillEntity SkillSelected;

    [Header("DAO Manager")]
    [SerializeField] GameObject DAOManager;

    bool StatusEquip;

    public bool CanUseSkill;

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

    private void Update()
    {
        foreach(Skill_Slot skill in listSkillSlot)
        {
            skill.ExecuteSkill();
        }
    }

    public void LoadAccountSkillList()
    {
        ListAccountSkill = DAOManager.GetComponent<Account_SkillDAO>().GetAllSkillForAccount(AccountManager.AccountID);
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountSkillEntity Skill in ListAccountSkill)
        {
            SkillEntity skillentity = DAOManager.GetComponent<SkillDAO>().GetSkillbyID(Skill.SkillID);

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


    /*public void ShowInformationHoverSkill(SkillEntity skill)
    {
        SkillSelected = skill;
        SkillImage.sprite = skill.SkillImage;
        SkillDescription.text = "- " + skill.Name;
        CurrentDamage.text = skill.Damage.ToString();
        CurrentChakra.text = skill.Chakra.ToString();
        CurrentLevel.text = "Level " + skill.Level.ToString();
        SetUpStatusForUpgrade(skill);
    }
    public void SetUpStatusForUpgrade(SkillEntity skill)
    {
        if (skill.Level < 3)
        {
            MaxLevelPanel.SetActive(false);
            CanUpgradePanel.SetActive(true);
            NextLevel.text = (skill.Level + 1).ToString();
            NextDamage.text = (skill.Damage + (skill.Damage * 30 / 100)).ToString();
            NextChakra.text = (skill.Chakra - (skill.Chakra * 30 / 100)).ToString();
        }
        else
        {
            MaxLevelPanel.SetActive(true);
            CanUpgradePanel.SetActive(false);

        }
    }*/

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

    public void SetUpSelectedSkill(SkillEntity Skill,Vector3 transform)
    {
        if (Skill != null)
        {
            SkillSelected = Skill;
            SelectedSkillCircle.transform.position = transform;
        }
    }

}








