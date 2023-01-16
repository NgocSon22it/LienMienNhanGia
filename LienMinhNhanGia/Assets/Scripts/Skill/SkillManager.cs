using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] GameObject UpgradePetPanel;

    [Header("SKILL SLOT MANAGER")]
    [SerializeField] List<Skill_Slot> ListSkillSlot = new List<Skill_Slot>();
    [SerializeField] List<Sprite> ListSkillImage = new List<Sprite>();

    List<SkillEntity> listSkill = new List<SkillEntity>();

    [SerializeField] GameObject SelectedSkillCircle;

    public static SkillEntity SkillSelected;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            SkillEntity Skill = new SkillEntity(i, ListSkillImage[i], (i + 1) * 10, "Skill" + i, i, i * 100);

            listSkill.Add(Skill);
        }

        LoadSkillList();
    }


    public void LoadSkillList()
    {
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (SkillEntity skill in listSkill)
        {
            Instantiate(SkillItem, Content).GetComponent<Skill_Item>().SetUp(skill);
        }
    }


    public void ShowInformationHoverSkill(SkillEntity skill)
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
    }

    public void UpgradeSelectedSkill(SkillEntity skill)
    {
        skill.Damage += skill.Damage * 30 / 100;
        skill.Chakra -= (skill.Chakra * 30 / 100);
        skill.Level += 1;

        CurrentLevel.text = "Level " + skill.Level;
        CurrentDamage.text = skill.Damage.ToString();
        CurrentChakra.text = skill.Chakra.ToString();
        foreach (SkillEntity skillentity in listSkill)
        {
            if (skillentity.Id == skill.Id)
            {
                skillentity.Level = skill.Level;
                skillentity.Chakra = skill.Chakra;
                skillentity.Damage = skill.Damage;
            }
        }

        SetUpStatusForUpgrade(skill);
        LoadSkillList();
    }

    public void UpgradeDisplaySkill()
    {
        UpgradeSelectedSkill(SkillSelected);
    }

    public void SetUpSelectedCircle(Vector3 transform)
    {
        SelectedSkillCircle.transform.position = transform;
    }

    /*public void CheckStatusForEquipSkill(SkillEntity skillEntity)
    {
        foreach (Skill_Slot skill in ListSkillSlot)
        {
            if (skill.Skill != null)
            {
                if (skill.Skill.Id == skillEntity.Id)
                {
                    skill.UnEquipSlot();
                }
            }
        }
    }*/

}








