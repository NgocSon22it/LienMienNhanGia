using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class MonsterDAO
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public List<MonsterEntity> GetAllMonster()
    {
        List<MonsterEntity> list = new List<MonsterEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Monster";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new MonsterEntity
                    {
                        Monster_ID = dr["Monster_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Health = Convert.ToInt32(dr["Health"]),
                        Damage = Convert.ToInt32(dr["Damage"]),
                        Speed = Convert.ToInt32(dr["Speed"]),
                        Coin_Bonus = Convert.ToInt32(dr["Coin_Bonus"]),
                        Description = dr["Description"].ToString(),
                        Link_image = dr["Link_image"].ToString(),
                        Delete = Convert.ToBoolean(dr["Delete"])
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

    public MonsterEntity GetMonsterbyId(string MonsterID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Monster where Monster_ID = @monsterid";
                cmd.Parameters.AddWithValue("@monsterid", MonsterID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    MonsterEntity a = new MonsterEntity
                    {
                        Monster_ID = dr["Monster_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Health = Convert.ToInt32(dr["Health"]),
                        Damage = Convert.ToInt32(dr["Damage"]),
                        Speed = Convert.ToInt32(dr["Speed"]),
                        Coin_Bonus = Convert.ToInt32(dr["Coin_Bonus"]),
                        Description = dr["Description"].ToString(),
                        Link_image = dr["Link_image"].ToString(),
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
