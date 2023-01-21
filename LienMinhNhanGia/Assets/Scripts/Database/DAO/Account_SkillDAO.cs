using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class Account_SkillDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public List<AccountSkillEntity> GetAllSkillForAccount()
    {
        List<AccountSkillEntity> list = new List<AccountSkillEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Skill";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new AccountSkillEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CurrentLevel = Convert.ToInt32(dr["CurrentLevel"]),
                        SlotIndex = Convert.ToInt32(dr["SlotIndex"])

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

    public AccountSkillEntity GetAccountSkillbySlotIndex(int SlotIndex)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Skill where SlotIndex = " + SlotIndex;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountSkillEntity a = new AccountSkillEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CurrentLevel = Convert.ToInt32(dr["CurrentLevel"]),
                        SlotIndex = Convert.ToInt32(dr["SlotIndex"])
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
    public AccountSkillEntity GetAccountSkillbyId(int IdSkill)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Skill where Id = " + IdSkill;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountSkillEntity a = new AccountSkillEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        CurrentLevel = Convert.ToInt32(dr["CurrentLevel"]),
                        SlotIndex = Convert.ToInt32(dr["SlotIndex"])
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

    public void UpdateSlotIndex(int IdSkill, int SlotIndex)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update Account_Skill set SlotIndex = @slot where Id = " + IdSkill;
            cmd.Parameters.AddWithValue("@slot", SlotIndex);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
