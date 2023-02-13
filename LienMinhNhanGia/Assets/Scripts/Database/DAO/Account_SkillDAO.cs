using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using Unity.VisualScripting;

public class Account_SkillDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public List<AccountSkillEntity> GetAllSkillForAccount(int AccountID)
    {
        List<AccountSkillEntity> list = new List<AccountSkillEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Skill where Account_ID = " + AccountID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new AccountSkillEntity
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        SkillID = dr["Skill_ID"].ToString(),
                        CurrentLevel = Convert.ToInt32(dr["Current_Level"]),
                        SlotIndex = Convert.ToInt32(dr["Slot_Index"]),
                        Detele = Convert.ToBoolean(dr["Delete"])

                    });
                }

            }
            finally
            {
                connection.Close();

            }
        }

        return list;
    }

    public AccountSkillEntity GetAccountSkillbySlotIndex(int AccountID, int SlotIndex)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Skill where Account_ID = @accountid and Slot_Index = " + SlotIndex;
                cmd.Parameters.AddWithValue("@accountid", AccountID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountSkillEntity a = new AccountSkillEntity
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        SkillID = dr["Skill_ID"].ToString(),
                        CurrentLevel = Convert.ToInt32(dr["Current_Level"]),
                        SlotIndex = Convert.ToInt32(dr["Slot_Index"]),
                        Detele = Convert.ToBoolean(dr["Delete"])
                    };
                    connection.Close();
                    return a;
                }
            }
            finally
            {
                connection.Close();
            }

        }

        return null;
    }
    public AccountSkillEntity GetAccountSkillbySkillID(int AccountID, string SkillID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Skill where Account_ID = @accountid AND Skill_ID = '" + SkillID +"'";
                cmd.Parameters.AddWithValue("@accountid", AccountID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountSkillEntity a = new AccountSkillEntity
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        SkillID = dr["Skill_ID"].ToString(),
                        CurrentLevel = Convert.ToInt32(dr["Current_Level"]),
                        SlotIndex = Convert.ToInt32(dr["Slot_Index"]),
                        Detele = Convert.ToBoolean(dr["Delete"])
                    };
                    connection.Close();
                    return a;
                }
            }
            finally
            {
                connection.Close();
            }

        }

        return null;
    }

    public void UpdateAccountSkillSlotIndex(int AccountID, string SkillID, int SlotIndex)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update Account_Skill set Slot_Index = @slotindex where Skill_ID = @skillid and Account_ID = " + AccountID;
            cmd.Parameters.AddWithValue("@slotindex", SlotIndex);
            cmd.Parameters.AddWithValue("@skillid", SkillID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
