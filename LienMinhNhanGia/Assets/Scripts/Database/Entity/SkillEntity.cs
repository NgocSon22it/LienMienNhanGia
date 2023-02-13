using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEntity
{
    public string SkillID;
    public string CharacterID;
    public string Name;
    public int Chakra;
    public float Cooldown;
    public int Damage;
    public int Coin;
    public int LevelUnlock;
    public int Update_Coin;
    public string Link_Image;
    public string Description;
    public bool Delete;

    //Logic
    public string MethodName;
    public Sprite SkillImage;


    public SkillEntity() { }

}
