using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Item : MonoBehaviour , IPointerClickHandler
{
    public SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] GameObject EquipStatusMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.SetUpSelectedCircle(this.gameObject.transform.position);       
        SkillManager.Instance.ShowInformationHoverSkill(Skill);
    }
    public void SetUp(SkillEntity Skill)
    {
        this.Skill = Skill;
        SkillImage.sprite = Skill.SkillImage;
    }

    public void Equiped()
    {
        EquipStatusMenu.SetActive(true);
    }
    public void UnEquiped()
    {
        EquipStatusMenu.SetActive(false);
    }


}
