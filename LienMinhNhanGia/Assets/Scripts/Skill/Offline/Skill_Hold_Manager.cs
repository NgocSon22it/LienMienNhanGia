﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hold_Manager : MonoBehaviour
{
    public static Skill_Hold_Manager Instance;

    AccountSkillEntity AccountSkill_U;
    AccountSkillEntity AccountSkill_I;
    AccountSkillEntity AccountSkill_O;

    SkillEntity Skill;

    private void Awake()
    {
        Instance = this;
    }   
    private void Update()
    {
        ExecuteSkill();
    }

    public void Skill_WaterBall()
    {
        OfflinePlayer.Instance.animator.SetTrigger("Skill_WaterBall");
    }
    public void Skill_WaterSlash()
    {
        OfflinePlayer.Instance.animator.SetTrigger("Skill_WaterSlash");

    }
    public void Skill_WaterSword()
    {
        Debug.Log("WaterSword");
    }
    public void Skill_WaterDragon()
    {
        Debug.Log("WaterDragon");
    }

    public void Skill_WaterShark()
    {
        Debug.Log("WaterShark");
    }

    public void CallMethodName(string MethodName)
    {
        Invoke(MethodName, 0f);
    }

    public void SetUpSkill(AccountSkillEntity accountSkillEntity ,int slot)
    {
        accountSkillEntity = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, slot);

        switch (slot)
        {
            case 1:
                AccountSkill_U = accountSkillEntity; break;
            case 2:
                AccountSkill_I = accountSkillEntity; break;
            case 3:
                AccountSkill_O = accountSkillEntity; break;
        }
    }


    public void ControlSkill(KeyCode key, AccountSkillEntity accountSkillEntity)
    {
        if (Input.GetKeyDown(key) && accountSkillEntity != null)
        {
            Skill = new SkillDAO().GetSkillbyID(accountSkillEntity.SkillID);
            CallMethodName(Skill.SkillID);

        }
    }

    public void ExecuteSkill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ControlSkill(KeyCode.U, AccountSkill_U);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ControlSkill(KeyCode.I, AccountSkill_I);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ControlSkill(KeyCode.O, AccountSkill_O);
        }
    }


}
