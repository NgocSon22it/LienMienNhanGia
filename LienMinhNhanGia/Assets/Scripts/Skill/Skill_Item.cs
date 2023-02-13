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

    [Header("Extension")]
    string Extension = "Skill/";
    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.Instance.SetUpSelectedSkill(Skill, transform.position);
        //SkillManager.Instance.ShowInformationHoverSkill(Skill);
    }
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
    }


}
