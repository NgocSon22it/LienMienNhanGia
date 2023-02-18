using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class AccountDAO : MonoBehaviour
{

    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public AccountEntity GetAccountByID(int AccountID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account where Account_ID = " + AccountID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountEntity a = new AccountEntity
                    {
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        Name = dr["Name"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Role = Convert.ToInt32(dr["Role"]),
                        Avatar = dr["Avatar"].ToString(),
                        Coin = Convert.ToInt32(dr["Coin"]),
                        Experience = Convert.ToInt32(dr["Experience"]),
                        Level = Convert.ToInt32(dr["Level"]),
                        CheckPoint = dr["Check_Point"].ToString(),
                        BossKill = Convert.ToInt32(dr["Boss_Kill"]),
                        AmountSlotSkill = Convert.ToInt32(dr["Amount_Slot_Skill"]),
                        PointSkill = Convert.ToInt32(dr["Point_Skill"]),
                        Delete = Convert.ToBoolean(dr["Delete"])

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
}
