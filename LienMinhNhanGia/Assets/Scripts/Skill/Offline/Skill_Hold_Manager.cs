using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Hold_Manager : MonoBehaviour
{
    public static Skill_Hold_Manager Instance;

    AccountSkillEntity AccountSkill_U;
    AccountSkillEntity AccountSkill_I;
    AccountSkillEntity AccountSkill_O;

    SkillEntity Skill;
    SkillEntity Skill_U;
    SkillEntity Skill_I;
    SkillEntity Skill_O;

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
        Debug.Log("Bug");
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

    public void SetUpSkill(int slot)
    {
        AccountSkillEntity accountSkillEntity = new Account_SkillDAO().GetAccountSkillbySlotIndex(AccountManager.AccountID, slot);

        switch (slot)
        {
            case 1:
                AccountSkill_U = accountSkillEntity; 
                if (AccountSkill_U != null) { Skill_U = new SkillDAO().GetSkillbyID(AccountSkill_U.SkillID); } 
                else { Skill_U = null; }
                break;
            case 2:
                AccountSkill_I = accountSkillEntity;
                if (AccountSkill_I != null) { Skill_I = new SkillDAO().GetSkillbyID(AccountSkill_I.SkillID); }
                else { Skill_I = null; }
                break;
            case 3:
                AccountSkill_O = accountSkillEntity;
                if (AccountSkill_O != null) { Skill_O = new SkillDAO().GetSkillbyID(AccountSkill_O.SkillID); }
                else { Skill_O = null; }
                break;
        }
    }


    public void ControlSkill(KeyCode key, AccountSkillEntity accountSkillEntity, SkillEntity skillEntity)
    {
        if (Input.GetKeyDown(key) && accountSkillEntity != null && skillEntity != null && OfflinePlayer.Instance.GetCurrentChakra() >= (skillEntity.Chakra + 1 - accountSkillEntity.CurrentLevel))
        {
            Skill = new SkillDAO().GetSkillbyID(accountSkillEntity.SkillID);
            CallMethodName(Skill.SkillID);
            OfflinePlayer.Instance.SetCurrentChakra(OfflinePlayer.Instance.GetCurrentChakra() - (skillEntity.Chakra + 1 - accountSkillEntity.CurrentLevel));
            PlayerUIManager.Instance.UpdatePlayerChakraUI();
        }
    }

    public void ExecuteSkill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ControlSkill(KeyCode.U, AccountSkill_U, Skill_U);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ControlSkill(KeyCode.I, AccountSkill_I, Skill_I);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ControlSkill(KeyCode.O, AccountSkill_O, Skill_O);
        }
    }


}
