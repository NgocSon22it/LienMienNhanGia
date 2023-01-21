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
                        Id = Convert.ToInt32(dr["Id"]),
                        Chakra = Convert.ToInt32(dr["Chakra"]),
                        Level = Convert.ToInt32(dr["Level"]),
                        Damage = Convert.ToInt32(dr["Damage"]),
                        SkillImage = Resources.Load<Sprite>(dr["Image"].ToString()),
                        Name = dr["Name"].ToString(),
                        MethodName = dr["MethodName"].ToString(),


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

    public SkillEntity GetSkillbyID(int IdSkill)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Skill where Id = " + IdSkill;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    SkillEntity a = new SkillEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Chakra = Convert.ToInt32(dr["Chakra"]),
                        Level = Convert.ToInt32(dr["Level"]),
                        Damage = Convert.ToInt32(dr["Damage"]),
                        SkillImage = Resources.Load<Sprite>(dr["Image"].ToString()),
                        Name = dr["Name"].ToString(),
                        MethodName = dr["MethodName"].ToString(),
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
