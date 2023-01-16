using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Slot : MonoBehaviour, IPointerClickHandler
{
    public SkillEntity Skill;
    [SerializeField] Image SkillImage;
    [SerializeField] int slot;
    [SerializeField] KeyCode key;

    [SerializeField] bool IsEquip;

    [Header("EquipSkill")]
    [SerializeField] GameObject EquipText;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SkillManager.SkillSelected != null)
        {
            EquipSlot(SkillManager.SkillSelected);
        }
    }

    private void Update()
    {
        ExecuteSkill();
    }

    public void ExecuteSkill()
    {
        if (Skill == null) return;

        if (Input.GetKeyDown(key))
        {
            Skill_Hold_Manager.Instance.CallMethodFromHold(Skill.Name);
        }
    }

    public void EquipSlot(SkillEntity skill)
    {
        if (IsEquip)
        {
            UnEquipSlot();
        }
        SkillImage.sprite = skill.SkillImage;
        Skill = skill;
    }

    public void UnEquipSlot()
    {
        IsEquip = false;
        Skill = null;
        SkillImage.sprite = null;
    }
}
