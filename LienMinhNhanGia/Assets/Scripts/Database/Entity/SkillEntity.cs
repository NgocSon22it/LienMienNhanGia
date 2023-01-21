using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEntity
{
    public int Id;
    public Sprite SkillImage;
    public int Chakra;
    public string Name;
    public int Level;
    public int Damage;

    //Logic
    public string MethodName;

    public SkillEntity(int Id, Sprite skillImage, int chakra, string name, int level, int damage)
    {
        this.Id = Id;
        SkillImage = skillImage;
        Chakra = chakra;
        Name = name;
        Level = level;
        Damage = damage;
    }

    public SkillEntity() { }

}
