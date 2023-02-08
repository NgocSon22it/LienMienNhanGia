using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class MissionDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public MissionEntity GetMissionbyId(string MissionID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Mission where Mission_ID = '" + MissionID + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    MissionEntity a = new MissionEntity
                    {
                        MissionID = dr["Mission_ID"].ToString(),
                        MapID = dr["Map_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Category = dr["Category"].ToString(),
                        Request = dr["Request"].ToString(),
                        Target = Convert.ToInt32(dr["Target"]),
                        ExperienceBonus = Convert.ToInt32(dr["Experience_Bonus"]),
                        CoinBonus = Convert.ToInt32(dr["Coin_Bonus"]),
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
