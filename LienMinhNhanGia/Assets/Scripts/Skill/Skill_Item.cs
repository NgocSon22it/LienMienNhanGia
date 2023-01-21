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
        SkillManager.Instance.SetUpSelectedCircle(gameObject.transform.position);
        SkillManager.Instance.ShowInformationHoverSkill(Skill);
    }
    public void SetUp(SkillEntity Skill, bool IsEquiped)
    {
        this.Skill = Skill;

        SkillImage.sprite = Skill.SkillImage;

        if (IsEquiped)
        {
            EquipStatusMenu.SetActive(true);
        }
        else
        {
            EquipStatusMenu.SetActive(false);
        }
    }


}
