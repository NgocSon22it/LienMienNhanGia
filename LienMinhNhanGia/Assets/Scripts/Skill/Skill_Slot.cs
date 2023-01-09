using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Slot : MonoBehaviour, IPointerClickHandler
{
    public SkillEntity Skill;
    public Image SkillImage;
    public int slot;

    public bool IsEquip;

    [Header("EquipSkill")]
    public GameObject EquipText;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SkillManager.SkillSelected != null)
        {
            Skill = SkillManager.SkillSelected;
            SetUpSlot(SkillManager.SkillSelected);
        }
    }

     

    public void SetUpSlot(SkillEntity skill)
    {
        SkillImage.sprite = skill.SkillImage;
        Skill.Name = skill.Name;
    }
}
