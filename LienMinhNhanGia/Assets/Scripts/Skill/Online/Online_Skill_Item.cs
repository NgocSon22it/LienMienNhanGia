using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Online_Skill_Item : MonoBehaviour, IPointerClickHandler
{
    SkillEntity Skill;

    [SerializeField] Image SkillImage;
    [SerializeField] GameObject EquipStatusMenu;

    [Header("Extension")]
    string Extension = "Skill/";
    public void OnPointerClick(PointerEventData eventData)
    {
        Online_SkillManager.Instance.SetUpSelectedSkill(Skill, transform.position);
        
    }
    public void SetUp(SkillEntity Skill, bool IsEquiped)
    {
        this.Skill = Skill;

        SkillImage.sprite = Resources.Load<Sprite>(Extension + Skill.SkillID);

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
