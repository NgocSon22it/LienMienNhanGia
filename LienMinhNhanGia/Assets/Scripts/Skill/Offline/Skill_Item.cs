using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Skill_Item : MonoBehaviour
{
    SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] GameObject EquipStatusMenu;
    [SerializeField] GameObject SelectedCircle;
    [SerializeField] GameObject HoverMenu;

    [Header("Extension")]
    string Extension = "Skill/";


    public void SetUp(SkillEntity Skill, bool IsEquiped)
    {
        this.Skill = Skill;

        SkillImage.sprite =  Resources.Load<Sprite>(Extension + Skill.SkillID);

        if (IsEquiped)
        {
            EquipStatusMenu.SetActive(true);
        }
        else
        {
            EquipStatusMenu.SetActive(false);
        }
        //SetUpSelected();
    }

    public void SetUpSelected()
    {
        if (SkillManager.Instance.SkillSelected.SkillID.Equals(Skill.SkillID))
        {
            SelectedCircle.SetActive(true);
        }
        else
        {
            SelectedCircle.SetActive(false);
        }
    }

    public void Onlick_SelectedSkill()
    {
        SkillManager.Instance.SetUpSelectedSkill(Skill);
    }

}
