using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AccountManager
{
    public static int AccountID = 1;

    public static List<AccountItemEntity> ListAccountItem = new Account_ItemDAO().GetAllItemForAccount(AccountID);

    public static List<AccountMissionEntity> ListAccountMission = new Account_MissionDAO().GetAllMissionForAccount(AccountID);

    public static List<AccountSkillEntity> ListAccountSkill = new Account_SkillDAO().GetAllSkillForAccount(AccountID);

    public static AccountEntity Account = new AccountDAO().GetAccountByID(AccountID);

    public static void UpdateListAccountItem()
    {
        ListAccountItem = new Account_ItemDAO().GetAllItemForAccount(AccountID);
    }
    public static void UpdateListAccountSkill()
    {
        ListAccountSkill = new Account_SkillDAO().GetAllSkillForAccount(AccountID);
    }
}
