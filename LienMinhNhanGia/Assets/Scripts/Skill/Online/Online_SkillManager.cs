using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Online_SkillManager : MonoBehaviour
{
    [Header("Instance")]
    public static Online_SkillManager Instance;

    [SerializeField] GameObject SkillItem;
    [SerializeField] Transform Content;

    [Header("SKILL SLOT MANAGER")]
    List<AccountSkillEntity> ListAccountSkill = new List<AccountSkillEntity>();
    [SerializeField] List<Online_SkillSlot> listSkillSlot = new List<Online_SkillSlot>();

    [SerializeField] GameObject SelectedSkillCircle;

    public SkillEntity SkillSelected;

    bool StatusEquip;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LoadAccountSkillList();
        foreach (Online_SkillSlot skill in listSkillSlot)
        {
            skill.SetUpSlot();
        }
    }

    public void LoadAccountSkillList()
    {
        ListAccountSkill = new Account_SkillDAO().GetAllSkillForAccount(AccountManager.AccountID);
        foreach (Transform trans in Content)
        {
            Destroy(trans.gameObject);
        }

        foreach (AccountSkillEntity Skill in ListAccountSkill)
        {
            SkillEntity skillentity = new SkillDAO().GetSkillbyID(Skill.SkillID);

            StatusEquip = Skill.SlotIndex != 0 ? true : false;
            Instantiate(SkillItem, Content).GetComponent<Online_Skill_Item>().SetUp(skillentity, StatusEquip);
        }
    }

    public void LoadAccountSkillSlot()
    {
        foreach (Online_SkillSlot slot in listSkillSlot)
        {
            slot.SetUpSlot();
        }
    }

    public void SetUpSelectedSkill(SkillEntity Skill, Vector3 transform)
    {
        if (Skill != null)
        {
            SkillSelected = Skill;
            SelectedSkillCircle.transform.position = transform;
        }
    }
}
