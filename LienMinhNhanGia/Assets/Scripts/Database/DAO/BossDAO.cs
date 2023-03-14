using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class BossDAO
{

    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public List<BossEntity> GetAllBoss()
    {
        List<BossEntity> list = new List<BossEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Boss order by Health asc";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new BossEntity
                    {
                        BossID = dr["Boss_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Health = Convert.ToInt32(dr["Health"]),
                        Speed = Convert.ToInt32(dr["Speed"]),
                        Coin_Bonus = Convert.ToInt32(dr["Coin_Bonus"]),
                        Experience_Bonus = Convert.ToInt32(dr["Experience_Bonus"]),
                        Point_Skill = Convert.ToInt32(dr["Point_Skill_Bonus"]),
                        Percent_Bonus = Convert.ToInt32(dr["Percent_Bonus"]),
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

    public BossEntity GetBossByID(string BossID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Boss where Boss_ID = @bossid";
                cmd.Parameters.AddWithValue("@bossid", BossID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    BossEntity a = new BossEntity
                    {
                        BossID = dr["Boss_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Health = Convert.ToInt32(dr["Health"]),
                        Speed = Convert.ToInt32(dr["Speed"]),
                        Coin_Bonus = Convert.ToInt32(dr["Coin_Bonus"]),
                        Experience_Bonus = Convert.ToInt32(dr["Experience_Bonus"]),
                        Point_Skill = Convert.ToInt32(dr["Point_Skill_Bonus"]),
                        Percent_Bonus = Convert.ToInt32(dr["Percent_Bonus"]),
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
