using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class SkillDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();
    public List<SkillEntity> GetAllSkill()
    {
        List<SkillEntity> list = new List<SkillEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Skill";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new SkillEntity
                    {
                        SkillID = dr["Skill_ID"].ToString(),
                        CharacterID = dr["Character_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Chakra = Convert.ToInt32(dr["Chakra"]),
                        Cooldown = (float) dr["Cooldown"],
                        Damage = Convert.ToInt32(dr["Damage"]),
                        Coin = Convert.ToInt32(dr["Coin"]),
                        LevelUnlock = Convert.ToInt32(dr["Level_Unlock"]),
                        Update_Coin = Convert.ToInt32(dr["Update_Coin"]),
                        Description = dr["Description"].ToString(),
                        Link_Image = dr["Link_Image"].ToString(),
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

    public SkillEntity GetSkillbyID(string SkillID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Skill where Skill_ID = '" + SkillID +"'";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    SkillEntity a = new SkillEntity
                    {
                        SkillID = dr["Skill_ID"].ToString(),
                        CharacterID = dr["Character_ID"].ToString(),
                        Name = dr["Name"].ToString(),
                        Chakra = Convert.ToInt32(dr["Chakra"]),
                        Cooldown = (float)dr["Cooldown"],
                        Damage = Convert.ToInt32(dr["Damage"]),
                        Coin = Convert.ToInt32(dr["Coin"]),
                        LevelUnlock = Convert.ToInt32(dr["Level_Unlock"]),
                        Update_Coin = Convert.ToInt32(dr["Update_Coin"]),
                        Description = dr["Description"].ToString(),
                        Link_Image = dr["Link_Image"].ToString(),
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
