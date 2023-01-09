using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public GameObject SkillItem;
    public Transform Content;

    [Header("SHOW HOVER SKILL")]
    [SerializeField] Image SkillImage;
    [SerializeField] TMP_Text SkillDescription;

    [Header("SKILL UPGRADE")]
    public TMP_Text CurrentLevel;
    public TMP_Text CurrentDamage;
    public TMP_Text CurrentChakra;

    [Header("CAN UPGRADE")]
    public TMP_Text NextLevel;
    public TMP_Text NextDamage;
    public TMP_Text NextChakra;
    public TMP_Text UpgradeCost;

    [Header("UPGRADE MANAGER")]
    public GameObject CanUpgradePanel;
    public GameObject MaxLevelPanel;
    public GameObject UpgradePetPanel;

    [Header("SKILL SLOT MANAGER")]
    public List<Skill_Slot> ListSkillSlot = new List<Skill_Slot>();



    public List<Sprite> ListSkillImage = new List<Sprite>();

    static List<SkillEntity> listSkill = new List<SkillEntity>();

    public GameObject SelectedSkillCircle;

    public static SkillEntity SkillSelected;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SkillEntity Skill = new SkillEntity(i,ListSkillImage[i], (i + 1) * 10, "Skill " + i, i, i * 100);

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
            Instantiate(SkillItem, Content).GetComponent<Skill_Item>().SetUp(skill.Id, skill.SkillImage, skill.Chakra, skill.Name, skill.Level, skill.Damage);
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
        SetUpStatusForUpgrade(skill);
        LoadSkillList();
    }

    public void UpgradeDisplaySkill()
    {
        UpgradeSelectedSkill(SkillSelected);
    }

    public void SetUpSelectedCircle (Vector3 transform)
    {
        SelectedSkillCircle.transform.position = transform;
    }

    public void  Skill()
    {
            Debug.Log("Skill");
    }

}








