using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Item : MonoBehaviour, IPointerClickHandler
{
    private int Id;
    private Image SkillImage;
    private int Chakra;
    private int Level;
    private int Damage;
    private string Name;


    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.instance.SetUpSelectedCircle(this.gameObject.transform.position);
        SkillEntity skill = new SkillEntity(Id, SkillImage.sprite, Chakra, Name, Level, Damage);
        SkillManager.instance.ShowInformationHoverSkill(skill);
    }
    public void SetUp(int Id, Sprite skillImage, int Chakra, string Name, int Level, int Damage)
    {
        this.Id = Id;
        SkillImage.sprite = skillImage;
        this.Chakra = Chakra;
        this.Name = Name;
        this.Level = Level;
        this.Damage = Damage;
    }



}
